using hw04;
using Newtonsoft.Json;
using System.Text;

F f = new F();
Console.WriteLine("CSV serialization: ");
String csvSerializedStr = DurationAnalyser(f, CSVSerialize, 100, true);
Console.WriteLine("\nCSV deserialization: ");
// DeserializationDurationAnalyser<F>(csvSerializedStr, CSVSerializer.Deserialize<F>, 100, true);
CSVDeserializationDurationAnalyser<F>(csvSerializedStr, 100, true);
Console.WriteLine("\nJson serialization: ");
String jsonSerialization = DurationAnalyser(f, JsonSerialize, 100, true);
Console.WriteLine("\nJson deserialization: ");
// DeserializationDurationAnalyser<F>(jsonSerialization, JsonConvert.DeserializeObject<F>, 100, true);
JsonDeserializationDurationAnalyser<F>(jsonSerialization, 100, true);

String DurationAnalyser(Object obj, Func<Object, String>func, int IterationNumber = 0, bool ResultOut = false)
{
    DateTime startTime = DateTime.Now;
    String result = "";
    for (int i = 0; i < IterationNumber; i++)
    {
        result = func(obj);
    }
    DateTime endTime = DateTime.Now;
    TimeSpan span = endTime.Subtract(startTime);
    if (ResultOut) Console.WriteLine("Result: " + result);
    Console.WriteLine("Время выполнения: " + span.Hours + " часов " + span.Minutes + " минут " + 
        span.Seconds + " сек. " + span.Milliseconds + " мс.");
    return result;
}

String CSVSerialize(Object obj)
{
    return CSVSerializer.Serialize(obj);
}

String JsonSerialize(Object obj)
{
    StringBuilder sb = new ();
    StringWriter sw = new (sb);
    var serializer = new Newtonsoft.Json.JsonSerializer();
    serializer.Serialize(sw, obj);
    return sb.ToString();
}

// Непонятные проблемы с десериализацией G класса при вызове CSVSerializer.Deserialize<F>
// Поле G clazz через этот метод не десериализуется в отличие от метода CSVDeserializationDurationAnalyser<T>
void DeserializationDurationAnalyser<T>(String data, Func<String,T?> func, int IterationNumber = 0, bool ResultOut = false)
{
    DateTime startTime = DateTime.Now;
    T? t;
    for (int i = 0; i < IterationNumber; i++)
    {
        t = func(data);
    }
    DateTime endTime = DateTime.Now;
    TimeSpan span = endTime.Subtract(startTime);
    if (f != null && ResultOut) Console.WriteLine("Result: " + f.ToString());
    Console.WriteLine("Время выполнения: " + span.Hours + " часов " + span.Minutes + " минут " +
        span.Seconds + " сек. " + span.Milliseconds + " мс.");
}

void CSVDeserializationDurationAnalyser<T>(String data, int IterationNumber = 0, bool ResultOut = false)
{
    DateTime startTime = DateTime.Now;
    T? f = default(T);
    for (int i = 0; i < IterationNumber; i++)
    {
        f = CSVSerializer.Deserialize<T>(data);
    }
    DateTime endTime = DateTime.Now;
    TimeSpan span = endTime.Subtract(startTime);
    if (f != null && ResultOut) Console.WriteLine("Result: " + f.ToString());
    Console.WriteLine("Время выполнения: " + span.Hours + " часов " + span.Minutes + " минут " +
        span.Seconds + " сек. " + span.Milliseconds + " мс.");
}

void JsonDeserializationDurationAnalyser<T>(String data, int IterationNumber = 0, bool ResultOut = false)
{
    DateTime startTime = DateTime.Now;
    T? t;
    for (int i = 0; i < IterationNumber; i++)
    {
        t = JsonConvert.DeserializeObject<T>(data);
    }
    DateTime endTime = DateTime.Now;
    TimeSpan span = endTime.Subtract(startTime);
    if (f != null && ResultOut) Console.WriteLine("Result: " + f.ToString());
    Console.WriteLine("Время выполнения: " + span.Hours + " часов " + span.Minutes + " минут " +
        span.Seconds + " сек. " + span.Milliseconds + " мс.");
}
