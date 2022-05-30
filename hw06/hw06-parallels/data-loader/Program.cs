using data_generator;
using NDesk.Options;
using repository;
using repository.DAL;
using repository.Factory;
using SharedProject;
using System.Diagnostics;
using System.Text;

const string FUNCTION = "function";
const string TASK = "task";
const string NONE = "none";
const int DEFAULT_NTRIES = 3;

// Оработка входных параметров
Dictionary<string, object>? parameters = null;
try
{
    parameters = ProcessInputParameters(args);
    if (parameters == null) return 1;
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    ShowHelp();
    return 1;
}

if ((bool)parameters.GetValueOrDefault("showHelp", false))
{
    ShowHelp();
    return 0;
}

// 1. Запуск генератора файла через создание процесса, сделать возможность выбора в коде, 
//    как запускать генератор, процессом или через вызов метода. Если вдруг встретится баг с генерацией, 
//    то его нужно исправить и написать об этом при сдаче работы.

// 8. Реализовать 1 пункт задания, сделав в main проекта запуск процесса-генератора файла,
// его нужно будет собрать отдельно и передать в программу путь к .exe файлу,
// также сделать в Main вызов кода генератора из подключенного проекта,
// выбор между процессом или вызовом метода сделать настройкой
// (например аргумент командной строки или файл с настройками) со значением по умолчанию для метода.

string method = (string)parameters.GetValueOrDefault("method", NONE);
// при сдаче задания написать время, за которое был обработан файл и количество потоков.
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
if (method == FUNCTION)
{
    GenerateDataByFunction(parameters);
}
else if(method == TASK)
{
    GenerateDataByTask(parameters);
}
stopwatch.Stop();
double sec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency; //переводим такты в секунды
Console.WriteLine("Data generation execution time: " + sec);

// Распараллеливаем обработку файла по набору диапазонов Id, то есть нужно, чтобы файл разбивался
// на диапазоны по Id и обрабатывался параллельно через Thread, сколько диапазонов столько потоков.
// Хорошо сделать настройку с количеством потоков, чтобы можно было настроить оптимальное количество потоков
// под размер файла с данными. Предусмотреть обработку ошибок в обработчике и перезапуск
// по ошибке с указанием числа попыток. Проверить обработку на файле, в котором 1 млн. записей,
// при сдаче задания написать время, за которое был обработан файл и количество потоков.
stopwatch.Start();
LoadData(parameters);
stopwatch.Stop();

sec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency; //переводим такты в секунды
Console.WriteLine("Execution time: " + sec);
return 0;

/// <summary>
/// Вывод Help
/// </summary>
void ShowHelp()
{
    Console.WriteLine("Usage: data-loader.exe --inrep-path=path --outrep-path=path [--genapp-path=path --method=function|task --quantity=int] [--help] [--ntries=int]");
    Console.WriteLine("An interactive client for loading clients.");
    Console.WriteLine("Options:");
    Console.WriteLine("  inrep-path - path to input csv repository file.");
    Console.WriteLine("  outrep-path - path to output SQLite repository file.");
    Console.WriteLine("  genapp-path - path to generator exe file.");
    Console.WriteLine("  method - method repository generation: by function or by task.");
    Console.WriteLine("  quantity - quantity of records to be generated.");
    Console.WriteLine("  ntries - number of tries of data loading.");
    Console.WriteLine("  help - this help.");
}

Dictionary<string, object> ProcessInputParameters(string[] args)
{
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    var options = new OptionSet()
            .Add("h|?|help|-help", "show help", delegate (string v) { parameters.Add("showHelp", true); })
            .Add("inrep-path=", "path to input csv repository file.", delegate (string v) {
                parameters.Add("inrep-path", v);
            })
            .Add("outrep-path=", "path to output SQLite repository file.", delegate (string v) {
                parameters.Add("outrep-path", v);
            })
            .Add("genapp-path=", "path to generator exe file.", delegate (string v) {
                parameters.Add("genapp-path", v);
            })
            .Add("method=", "method for running data-generator", delegate (string v) {
                parameters.Add("method", v.ToLower());
            })
            .Add("quantity=", "quantity of records.", delegate (int q) {
                parameters.Add("quantity", q);
            })
            .Add("ntries=", "number of tries.", delegate (int nt) {
                parameters.Add("ntries", nt);
            });

    options.Parse(args);
    CheckInputParameters(parameters);
    return parameters;
}

/// <summary>
/// Проверка входных параметров 
/// </summary>
/// <param name="parameters">проверяемые входные параметры</param>
/// <exception cref="Exception">ошибка в случае неверно заданных параметров</exception>
void CheckInputParameters(Dictionary<string, object> parameters)
{
    object inRepPath;
    object outRepPath;
    if(!parameters.TryGetValue("inrep-path", out inRepPath)) throw new Exception("The inrep-path param is not defined.");
    if (!parameters.TryGetValue("outrep-path", out outRepPath)) throw new Exception();

    if (String.IsNullOrEmpty((string)inRepPath))
    {
        throw new Exception("Bad path to input csv repository file defined in the inrep-path param.");
    }

    if (String.IsNullOrEmpty((string)outRepPath))
    {
        throw new Exception("Bad path to output SQLite repository file defined in the the outrep-path param.");
    }

    // проверка [--genapp-path=path --method=function|task --quantity=int]
    string[] keys = { "genapp-path", "method", "quantity" };
    if(keys.Any(key => parameters.ContainsKey(key)))
    {
        if(!keys.All(key => parameters.ContainsKey(key)))
        {
            throw new Exception("Not all params present: --genapp-path=path --method=function|task --quantity=int");
        }

        method = (string)parameters["method"];
        if (method != FUNCTION && method != TASK)
        {
            throw new Exception("Bad method: " + method);
        }

        int quantity = (int)parameters["quantity"];
        if (quantity < 1)
        {
            throw new Exception("Quantity not specified or bad value: [" + quantity + "]");
        }

        if (!Utils.CanCreateFile((string)inRepPath))
        {
            throw new Exception("Bad path to input csv repository file: [" + inRepPath + "]");
        }
    }
}

/// <summary>
/// Запуск генератора данных через вызов программы data-generator.exe 
/// с сохранением сгенерированных данных в CSV репозитории
/// </summary>
void GenerateDataByFunction(Dictionary<string, object> parameters)
{
    string generatorPath = (string)parameters["genapp-path"];
    string repositoryPath = (string)parameters["inrep-path"];
    int quantity = (int)parameters["quantity"];

    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("--path ");
    stringBuilder.Append(repositoryPath);
    stringBuilder.Append(" --quantity ");
    stringBuilder.Append(quantity);

    var proc = System.Diagnostics.Process.Start(generatorPath, stringBuilder.ToString());
    proc.WaitForExit();
}

/// <summary>
/// Запуск генератора данных через Task
/// с сохранением сгенерированных данных в CSV репозитории
/// </summary>
void GenerateDataByTask(Dictionary<string, object> parameters)
{
    string generatorPath = (string)parameters["genapp-path"];
    string repositoryPath = (string)parameters["inrep-path"];
    int quantity = (int)parameters["quantity"];

    // генерация клиентов
    ClientGenerator clientGenerator = new();
    List<Client> clients = clientGenerator.Next(quantity);

    // сохранение клиентов в репозитории
    IConfiguration configuration = new CSVFileConfiguration(repositoryPath);
    RepositoryCreator creator = new CSVFileRepositoryCreator();
    IClientRepository? repository = creator.CreateClientRepository(configuration);
    if(repository == null) { throw new Exception("CSVFileRepository is not created."); }
    
    repository.Insert(clients);
    Console.WriteLine("Done.\nCreated " + (int)parameters["quantity"] + " records.");
}

/// <summary>
/// Загрузка данных в SQLite репозиторий в параллельном режиме
/// </summary>
void LoadData(Dictionary<string, object> parameters)
{
    string inRepositoryPath = (string)parameters["inrep-path"];

    IConfiguration configuration = new CSVFileConfiguration(inRepositoryPath);
    RepositoryCreator creator = new CSVFileRepositoryCreator();
    IClientRepository? inRepository = creator.CreateClientRepository(configuration);
    if (inRepository == null) { throw new Exception("CSVFileRepository is not created."); }

    var threads = 2 * Environment.ProcessorCount;
    ThreadPool.SetMaxThreads(threads, threads);

    Console.WriteLine("Yardware info:");
    Console.WriteLine("Processor count = " + Environment.ProcessorCount);
    int workers, ports;
    ThreadPool.GetMinThreads(out workers, out ports);
    Console.WriteLine("Min workers = " + workers + " ports = " + ports);
    ThreadPool.GetMaxThreads(out workers, out ports);
    Console.WriteLine("Max workers = " + workers + " ports = " + ports);
    
    // определение количества записей в репозитории
    long recordNumber = inRepository.Count();
    if (recordNumber == 0 )
    {
        Console.WriteLine("Nothing to load.");
        return;
    }

    // Вычисление количества записей, которые будут загружены одним потоком
    long piece = recordNumber / threads;
    Console.WriteLine("Records for loading: " + recordNumber);
    Console.WriteLine("Number of threads: " + threads + ". Records for each thread: " + piece);

    int niterations = (int)parameters.GetValueOrDefault("ntries", DEFAULT_NTRIES);
    for (int i = 1; i <= niterations; i++)
    {
        // флаг ошибочного завершения загрузки данных в SQLite
        bool errorHappened = false;

        var events = new List<ManualResetEvent>();
        // Распараллеливаем обработку файла по набору диапазонов Id, то есть нужно, чтобы файл разбивался на диапазоны по Id
        // и обрабатывался параллельно через Thread, сколько диапазонов столько потоков.
        // Хорошо сделать настройку с количеством потоков, чтобы можно было настроить оптимальное количество потоков под размер файла с данными.
        // Предусмотреть обработку ошибок в обработчике и перезапуск по ошибке с указанием числа попыток.
        // Проверить обработку на файле, в котором 1 млн.записей, при сдаче задания написать время, за которое был обработан файл и количество потоков.
        for (long from = 1, to = piece;
            from < recordNumber && to <= recordNumber;
            from += piece, to += Math.Min(piece, recordNumber - to)
        )
        {
            // Прерывание работы, если что-то пошло не так
            if (errorHappened) break;

            var resetEvent = new ManualResetEvent(false);
            Dictionary<string, object> backgroundParams = new Dictionary<string, object>();
            backgroundParams.Add("inrep-path", inRepositoryPath);
            backgroundParams.Add("outrep-path", (string)parameters["outrep-path"]);
            backgroundParams.Add("from", from);
            backgroundParams.Add("to", to);
            backgroundParams.Add("resetEvent", resetEvent);
            backgroundParams.Add("errorHappened", errorHappened);
            
            // Вместо создания потоков через new Thread() использовать ThreadPool
            ThreadPool.QueueUserWorkItem(callback => BackgroundDataLoader(backgroundParams));
            events.Add(resetEvent);
        }

        if (events.Count() > 0)
        {
            WaitHandle.WaitAll(events.ToArray());
        }

        // Проверка корректного выполнения
        if (errorHappened)
        {
            Console.WriteLine("Error processing. Iteration number: " + i);
        }
        else
        {
            Console.WriteLine("Successfull loading");
            break;
        }
    }
}

/// <summary>
/// Проверка входных параметров для загрузчика данных
/// </summary>
/// <param name="p">проверяемые входные параметры</param>
/// <exception cref="Exception">ошибка в случае неверно заданных параметров</exception>
static void CheckBackgroundDataLoaderParams(Dictionary<string, object> p)
{
    long from = (long) p.GetValueOrDefault("from", -1);
    long to = (long) p.GetValueOrDefault("to", -1);
    string inRepositoryPath = (string)p.GetValueOrDefault("inrep-path","");
    string outRepositoryPath = (string)p.GetValueOrDefault("outrep-path","");

    if(from == -1 || to == -1)
    {
        throw new Exception("Load range (from and to) is not defined.");
    }
    if(inRepositoryPath == null)
    {
        throw new Exception("Input repository (in-rep) is not defined.");
    }
    if (outRepositoryPath == null)
    {
        throw new Exception("Out repository (out-rep) is not defined.");
    }
    Object resetEvent;
    if(!p.TryGetValue("resetEvent", out resetEvent))
    {
        throw new Exception("ResetEvent is not defined.");
    }
    if(resetEvent == null)
    {
        throw new Exception("ResetEvent is not defined.");
    }
    if (!p.ContainsKey("errorHappened"))
    {
        throw new Exception("Doesn't have errorHappened key.");
    }
}

/// <summary>
/// Загрузчик данных из CSV репозитория в SQLite репозиторий
/// </summary>
/// <param name="backgroundParams">входные параметры определяющие загружаемые данные</param>
/// <remarks>
/// <param name="resetEvent">параметр типа ManualResetEvent для синхронизации потоков</param>
/// <param name="from">начальный идентификатор диапазона клиентов для загрузки</param>
/// <param name="to">конечный идентификатор диапазона клиентов для загрузки</param>
/// <param name="inrep-path">путь к CSV репозиторию</param>
/// <param name="outrep-path">путь к SQLite репозиторию</param>
/// </remarks>
static void BackgroundDataLoader(Object backgroundParams)
{
    Dictionary<string, object> p = (Dictionary<string, object>)backgroundParams;
    CheckBackgroundDataLoaderParams(p);
    ManualResetEvent resentEvent = (ManualResetEvent)p.GetValueOrDefault("resetEvent", null);
    if (resentEvent == null) return;
    
    IClientRepository outRepository;
    try
    {
        long from = (long)p.GetValueOrDefault("from", -1);
        long to = (long)p.GetValueOrDefault("to", -1);
        Thread.CurrentThread.Name = "range " + from + " to " + to;
        Console.WriteLine("Thread: " + Thread.CurrentThread.Name + ". Records chunk: from " + from + " to " + to + ".");

        bool errorHappened = (bool)p.GetValueOrDefault("errorHappened", false);
        if (errorHappened)
        {
            object rf = p.GetValueOrDefault("errorHappened", false);
            Console.WriteLine("Thread: " + Thread.CurrentThread.Name + ". Soft abort execution.");
            return;
        }

        string inRepositoryPath = (string)p.GetValueOrDefault("inrep-path", "");
        string outRepositoryPath = (string)p.GetValueOrDefault("outrep-path", "");

        // Создание экземпляра CSV репозитория
        IConfiguration inConfiguration = new CSVFileConfiguration(inRepositoryPath);
        RepositoryCreator inRepCreator = new CSVFileRepositoryCreator();
        IClientRepository? inRepository = inRepCreator.CreateClientRepository(inConfiguration);
        if (inRepository == null) { throw new Exception("Input CSV repository is not created."); }

        // Создание экземпляра SQLite репозитория
        IConfiguration outConfiguration = new SQLiteConfiguration(outRepositoryPath);
        RepositoryCreator outRepCreator = new SQLiteRepositoryCreator();
        outRepository = outRepCreator.CreateClientRepository(outConfiguration);
        if (outRepository == null) { throw new Exception("Output SQL repository is not created."); }

        // Получение списка клиентов из CSV в диапазоне идентификаторов from, to для загрузки в SQLite
        IClientSpecification clientCpecification = new ClientFileSpecification(from, to);
        IEnumerable<Client> clients = inRepository.Get(clientCpecification);

        outRepository.Open();
        outRepository.BeginTransaction();
        if (clients.Count() > 0)
        {
            outRepository.Insert(clients);
        }
        outRepository.CommitTransaction();
    }
    catch (Exception ex) { 
        
        // if(outRepository != null) outRepository.RollbackTransaction();
        Console.WriteLine(ex.Message);
        p["errorHappened"] = true;
    }
    finally
    {
        resentEvent.Set();
    }
}
