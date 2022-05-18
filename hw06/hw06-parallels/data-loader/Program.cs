using data_generator;
using NDesk.Options;
using repository;
using repository.DAL;
using System.Text;

const string FUNCTION = "function";
const string TASK = "task";
const string NONE = "none";


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
if (method == FUNCTION)
{
    GenerateDataByFunction(parameters);
}
else if(method == TASK)
{
    GenerateDataByTask(parameters);
}

// Распараллеливаем обработку файла по набору диапазонов Id, то есть нужно, чтобы файл разбивался
// на диапазоны по Id и обрабатывался параллельно через Thread, сколько диапазонов столько потоков.
// Хорошо сделать настройку с количеством потоков, чтобы можно было настроить оптимальное количество потоков
// под размер файла с данными. Предусмотреть обработку ошибок в обработчике и перезапуск
// по ошибке с указанием числа попыток. Проверить обработку на файле, в котором 1 млн. записей,
// при сдаче задания написать время, за которое был обработан файл и количество потоков.
LoadData(parameters);


return 0;

void ShowHelp()
{
    Console.WriteLine("Usage: data-loader.exe --rep-path=path [--gen-path=path --method=function|task --quantity=int] [--help]");
    Console.WriteLine("An interactive client for loading clients.");
    Console.WriteLine("Options:");
    Console.WriteLine("  method - method repository generation: by function or by task.");
    Console.WriteLine("  gen-path - path to generator exe file.");
    Console.WriteLine("  rep-path - path to repository file.");
    Console.WriteLine("  quantity - quantity of records to be generated.");
    Console.WriteLine("  help - this help.");
}

Dictionary<string, object> ProcessInputParameters(string[] args)
{
    Dictionary<string, object> parameters = new Dictionary<string, object>();
    var options = new OptionSet()
            .Add("h|?|help|-help", "show help", delegate (string v) { parameters.Add("showHelp", true); })
            .Add("method=", "method for running data-generator", delegate (string v) {
                parameters.Add("method", v.ToLower());
            })
            .Add("gen-path=", "path to generator exe file.", delegate (string v) {
                parameters.Add("generatorPath", v);
            })
            .Add("rep-path=", "path to repository file.", delegate (string v) {
                parameters.Add("repositoryPath", v);
            })
            .Add("quantity=", "quantity of records.", delegate (int q) {
                parameters.Add("quantity", q);
            });

    options.Parse(args);
    CheckInputParameters(parameters);
    return parameters;
}

// Проверка входных параметров
void CheckInputParameters(Dictionary<string, object> parameters)
{
    string repositoryPath = (string)parameters.GetValueOrDefault("repositoryPath","");
    if(repositoryPath.Length == 0)
    {
        throw new Exception("Bad path to repository file defined in rep-path param.");
    }

    // проверка [--gen-path=path --method=function|task --quantity=int]
    string[] keys = { "generatorPath", "method", "quantity" };
    if(keys.Any(key => parameters.ContainsKey(key)))
    {
        if(!keys.All(key => parameters.ContainsKey(key)))
        {
            throw new Exception("Not all params present: --gen-path=path --method=function|task --quantity=int");
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

        if (!CanCreateFile(repositoryPath))
        {
            throw new Exception("Bad path to repository file: [" + repositoryPath + "]");
        }
    }
    else
    {
        if(!File.Exists(repositoryPath))
        {
            throw new Exception("Bad path to repository file: [" + repositoryPath + "]");
        }
    }

    if (!parameters.ContainsKey("repositoryPath"))
    {
        throw new Exception("Bad path to repository file defined in rep-path param.");
    }
}

/* Проверка возможности создания файла */
bool CanCreateFile(string file)
{
    try
    {
        using (File.Create(file)) { }
        File.Delete(file);
        return true;
    }
    catch
    {
        return false;
    }
}

// Запуск генератора данных через вызов exe
void GenerateDataByFunction(Dictionary<string, object> parameters)
{
    string generatorPath = (string)parameters["generatorPath"];
    string repositoryPath = (string)parameters["repositoryPath"];
    int quantity = (int)parameters["quantity"];

    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("--path ");
    stringBuilder.Append(repositoryPath);
    stringBuilder.Append(" --quantity ");
    stringBuilder.Append(quantity);

    var proc = System.Diagnostics.Process.Start(generatorPath, stringBuilder.ToString());
    proc.WaitForExit();
}

void GenerateDataByTask(Dictionary<string, object> parameters)
{
    string generatorPath = (string)parameters["generatorPath"];
    string repositoryPath = (string)parameters["repositoryPath"];
    int quantity = (int)parameters["quantity"];

    // генерация клиентов
    ClientGenerator clientGenerator = new();
    List<Client> clients = clientGenerator.Next(quantity);

    // сохранение клиентов в репозитории
    ISerializer<Client> serializer = new ClientCSVSerializer();
    IClientContext context = new ClientFileContext(repositoryPath, serializer);
    IClientRepository repository = new ClientRepository(context);
    repository.Insert(clients);
    Console.WriteLine("Done.\nCreated " + (int)parameters["quantity"] + " records.");
}

void LoadData(Dictionary<string, object> parameters)
{
    string repositoryPath = (string)parameters["repositoryPath"];
    ISerializer<Client> serializer = new ClientCSVSerializer();
    IClientContext context = new ClientFileContext(repositoryPath, serializer);
    IClientRepository inRepository = new ClientRepository(context);

    var threads = 2 * Environment.ProcessorCount;
    ThreadPool.SetMaxThreads(threads, threads);

    // delete
    Console.WriteLine("Environment.ProcessorCount = " + Environment.ProcessorCount);
    int workers, ports;
    ThreadPool.GetMinThreads(out workers, out ports);
    Console.WriteLine("Min workers = " + workers + " ports = " + ports);
    ThreadPool.GetMaxThreads(out workers, out ports);
    Console.WriteLine("Max workers = " + workers + " ports = " + ports);

    // определение количества записей в репозитории
    long recordNumber = inRepository.Count();
    long piece = recordNumber / threads;
    Console.WriteLine("recordNumber = " + recordNumber + " piece = " + piece);
    for(long from = 1, to = piece; 
        from < recordNumber && to <= recordNumber; 
        from += piece, to += Math.Min(piece,recordNumber - to)
    )
    {
        /* Console.WriteLine("from = " + from + " to = " + to + " delta = " + 
            (recordNumber - to) + " next = " + (to + Math.Min(piece, recordNumber - to))); */
        Dictionary<string, object> backgroundParams = new Dictionary<string, object>();
        backgroundParams.Add("in-rep", inRepository);
        backgroundParams.Add("from", from);
        backgroundParams.Add("to", to);
        ThreadPool.QueueUserWorkItem(BackgroundDataLoader, backgroundParams);
    }
}

static void BackgroundDataLoader(Object backgroundParams)
{
    Dictionary<string, object> p = (Dictionary<string, object>)backgroundParams;
    long from = (long)p.GetValueOrDefault("from", -1);
    long to = (long)p.GetValueOrDefault("to", -1);
    Console.WriteLine("from = " + from + " to = " + to);
}