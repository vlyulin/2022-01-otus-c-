using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;

namespace hw04
{
    internal class CSVSerializer
    {
        private const char NAME_VALUE_DELIMITER = ':';
        private const char FIELD_DELIMITER = ',';
        private const char STRING_SURROUNDER = '"';
        private const char ARR_START_SURROUNDER = '[';
        private const char ARR_END_SURROUNDER = ']';
        private const char ELEMENTS_DELIMITER = ';';
        private const char CLASS_START_SURROUNDER = '{';
        private const char CLASS_FIELD_DELIMITER = '|';
        private const char CLASS_END_SURROUNDER = '}';

        public static string Serialize(object? value)
        {
            if (value == null) return "";
            if (value.GetType().IsPrimitive)
            {
                return value.ToString();
            }

            var sb = new StringBuilder();

            object t;
            Type T = value.GetType();            
            if (T.GetMethod("Get") != null)
            {
                t = T.InvokeMember("Get", BindingFlags.InvokeMethod, null, value, Array.Empty<object>());
            }
            else
            {
                t = value;
            }

            // https://docs.microsoft.com/ru-ru/dotnet/api/system.reflection.bindingflags?view=netcore-3.1
            var fields = T.GetFields(
                BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Static);

            foreach (var field in fields)
            {
                // не понял как определить field это или property
                // поэтому использую такой костыль
                if (field.Name.Contains("<prop>"))
                {
                    continue;
                }

                var fType = field.FieldType;
                if (fType.IsPrimitive || fType == typeof(Decimal))
                {
                    // Обработка примитивов
                    sb.Append(field.Name);
                    sb.Append(NAME_VALUE_DELIMITER);
                    sb.Append(field.GetValue(t).ToString());
                    sb.Append(FIELD_DELIMITER);
                }
                else if(fType == typeof(string))
                {
                    // Обработка строк
                    sb.Append(field.Name);
                    sb.Append(NAME_VALUE_DELIMITER);
                    sb.Append(STRING_SURROUNDER);
                    sb.Append(field.GetValue(t));
                    sb.Append(STRING_SURROUNDER);
                    sb.Append(FIELD_DELIMITER);
                }
                else if (fType.GetTypeInfo().IsArray)
                {
                    // Обработка массивов
                    // Пример результата: arr:[1;2;3]
                    Array arr = (Array)field.GetValue(t);
                    sb.Append(field.Name);
                    sb.Append(NAME_VALUE_DELIMITER);
                    sb.Append(ARR_START_SURROUNDER);
                    for (int i=0; i<arr.Length; i++)
                    {
                        Object obj = arr.GetValue(i);
                        sb.Append(Serialize(arr.GetValue(i)));
                        sb.Append(ELEMENTS_DELIMITER);
                    }
                    sb.Length--; // remove last DELIMITER
                    sb.Append(ARR_END_SURROUNDER);
                    sb.Append(FIELD_DELIMITER);
                }
                else if (fType.GetTypeInfo().IsClass)
                {
                    // Обработка классов
                    // Пример результата: <clazz>k__BackingField:{g1;1|g2;2} 
                    String s = Serialize(field.GetValue(t));
                    s = s.Replace(NAME_VALUE_DELIMITER, ELEMENTS_DELIMITER);
                    s = s.Replace(FIELD_DELIMITER, CLASS_FIELD_DELIMITER);
                    if (s != null) {
                        sb.Append(field.Name);
                        sb.Append(NAME_VALUE_DELIMITER);
                        sb.Append(CLASS_START_SURROUNDER); 
                        sb.Append(s);
                        sb.Append(CLASS_END_SURROUNDER);
                        sb.Append(FIELD_DELIMITER);
                    }
                }
            }

            var properties = T.GetProperties();
            foreach (var property in properties)
            {
                // Обработка свойств
                sb.Append(property.Name);
                sb.Append(NAME_VALUE_DELIMITER);
                sb.Append(property.GetValue(t));
                sb.Append(FIELD_DELIMITER);
            }

            sb.Length--; // remove last DELIMITER
            return sb.ToString();
        }
    
        public static T? Deserialize<T>(String data)
        {
            T obj = (T)Activator.CreateInstance(typeof(T), new object[] {});

            List<String> listStrLineElements = data.Split(FIELD_DELIMITER).ToList();
            foreach(string element in listStrLineElements)
            {
                String[] name_value = element.Split(NAME_VALUE_DELIMITER);
                if(name_value.Length == 2)
                {
                    try
                    {
                        var prop = typeof(T).GetField(name_value[0],
                            BindingFlags.Instance |
                            BindingFlags.NonPublic |
                            BindingFlags.Public |
                            BindingFlags.Static);
                        
                        if (prop == null) continue;

                        if (prop.FieldType.IsArray)
                        {
                            // Обработка массива
                            // обрабатываются только одномерные массивы!
                            List<char> charsToRemove = new() { ARR_START_SURROUNDER, ARR_END_SURROUNDER, ELEMENTS_DELIMITER };
                            string[] result = name_value[1].Split(charsToRemove.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                            // преобразование в тип элемента массива и десериализация самого массива
                            Type? elementType = prop.FieldType.GetElementType();
                            if (elementType != null)
                            {
                                // Не получилось создать массив типа prop.FieldType и присвоить полю десериализуемого класса
                                // Получаю ошибку: Object of type 'System.Object[]' cannot be converted to type 'System.Int32[]
                                // Были разные варианты, последняя выглядела так:
                                // Object arrObj = Array.CreateInstance(prop.FieldType, result.Length);
                                // var foo = System.ComponentModel.TypeDescriptor.GetConverter(elementType);
                                // Object arrObj = Array.ConvertAll(result, s => foo.ConvertFromInvariantString(s));
                                // Решения не нашел, создаю явно с нужным типом
                                Int32[] arrObj = Array.ConvertAll(result, s => Int32.Parse(s));
                                prop.SetValue(obj, arrObj);
                            }
                        }
                        else if (prop.FieldType.IsClass)
                        {
                            // Обработка класса
                            MethodInfo method = typeof(CSVSerializer).GetMethod(nameof(CSVSerializer.Deserialize));
                            MethodInfo generic = method.MakeGenericMethod(prop.FieldType);
                            List<char> charsToRemove = new List<char>() { CLASS_START_SURROUNDER, CLASS_END_SURROUNDER };
                            string[] result = name_value[1].Split(charsToRemove.ToArray(), StringSplitOptions.RemoveEmptyEntries);
                            result[0] = result[0].Replace(CLASS_FIELD_DELIMITER, FIELD_DELIMITER);
                            result[0] = result[0].Replace(ELEMENTS_DELIMITER, NAME_VALUE_DELIMITER);
                            
                            var classInstance = generic.Invoke(null, result);
                            prop.SetValue(obj, classInstance);
                        }
                        else if (prop.FieldType.IsPrimitive || prop.FieldType == typeof(Decimal))
                        {
                            prop.SetValue(obj, Int32.Parse(name_value[1]));
                        }
                        else if (prop.FieldType == typeof(string))
                        {
                            prop.SetValue(obj, Convert.ChangeType(name_value[1], typeof(System.String)));
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return obj;
        }
    }
}
