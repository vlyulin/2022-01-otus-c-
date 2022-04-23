using hw05_delegates_and_events;

//В этом задании требуется реализовать механизмы делегатов и событий для получения практического
//навыка их применения
//Описание/Пошаговая инструкция выполнения домашнего задания:

// 1. Написать обобщённую функцию расширения, находящую и возвращающую максимальный элемент коллекции.
//    Функция должна принимать на вход делегат, преобразующий входной тип в число для возможности
//    поиска максимального значения.
//    public static T GetMax(this IEnumerable e, Func<T, float> getParameter) where T : class;
// 2. Написать класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла;
// 3. Оформить событие и его аргументы с использованием .NET соглашений: public event EventHandler FileFound; FileArgs – будет содержать имя файла и наследоваться от EventArgs
// 4. Добавить возможность отмены дальнейшего поиска из обработчика;
// 5. Вывести в консоль сообщения, возникающие при срабатывании событий и результат поиска максимального элемента.

const int NUMBER_PROCESSED_FILES = 15;

void FileReceiver(object sender, FileArgs file)
{
    Console.WriteLine("Event firing. File: " + file.filePath);
    FileSystemScanner scanner = (FileSystemScanner)sender;
    if (scanner.counter > NUMBER_PROCESSED_FILES)
    {
        // Прерывание обработки
        scanner.StopScanning();
    }
}

string dir = @"C:\tmp";
Console.WriteLine("Scan dir: " + dir);
FileSystemScanner scanner = new FileSystemScanner();
scanner.RaiseFileFoundEvent += FileReceiver;
scanner.Scan(dir);
Console.WriteLine("Finish.");

List<SomeClass> list = new List<SomeClass> {
                new SomeClass(100.1f),
                new SomeClass(450.1f),
                new SomeClass(-24.1f)
            };
Console.Write("\nПоиск максимального элемента из: ");
foreach (SomeClass item in list) Console.Write(item.f + "; ");
var max = list.GetMax<SomeClass>(e => e.f);
Console.WriteLine("\nMax value is: " + max.f);
