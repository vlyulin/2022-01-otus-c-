using data_generator;
using repository;
using repository.DAL;
using NDesk.Options;
using repository.Factory;
using SharedProject;

Dictionary<string, object> parameters = new Dictionary<string, object>();
var options = new OptionSet()
        .Add("h|?|help|-help", "show help", delegate (string v) {
            parameters.Add("showHelp", true);
            //showHelp = true; 
        })
        .Add("path=", "path to repository file.", delegate (string v) {
            //repositoryPath = v;
            parameters.Add("repositoryPath", v);
        })
        .Add("quantity=", "quantity of records.", delegate (int q) {
            //quantity = q;
            parameters.Add("quantity", q);
        });

try
{
    // разбор входных параметров
    options.Parse(args);
    if(!CheckInputParameters(parameters))
    {
        return 1;
    }
}
catch (OptionException e)
{
    Console.WriteLine("Invalid arguments: " + e.Message);
    ShowHelp();
    return 1;
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

// проверка корректности входных параметров
if(!CheckInputParameters(parameters))
{
    // выход при некорректных параметрах
    return 1;
}

if ((bool)parameters.GetValueOrDefault("showHelp",false))
{
    ShowHelp();
    return 0;
}

// Удаление репозитория, если он существует, т.к. при генерации идентификаторы всегда начинаются с 1.
string repositoryPath = (string)parameters["repositoryPath"];
if ( File.Exists(repositoryPath) )
{
    File.Delete(repositoryPath);
}

GenerateData(parameters);
Console.WriteLine("Done.\nCreated " + (int)parameters["quantity"] + " records.");

return 0;

<<<<<<< HEAD
/// <summary>
/// Вывод Help
/// </summary>
=======
// Вывод Help
>>>>>>> main
void ShowHelp()
{
    Console.WriteLine("Usage: data-generator.exe --path=path --quantity=int [--help]");
    Console.WriteLine("An interactive client for loading clients.");
    Console.WriteLine("Options:");
    Console.WriteLine("  path - path to repository file.");
    Console.WriteLine("  quantity - quantity of records to be generated.");
    Console.WriteLine("  help - this help.");
}

<<<<<<< HEAD
/// <summary>
/// Проверка входных параметров 
/// </summary>
/// <param name="parameters">проверяемые входные параметры</param>
/// <returns>true - если параметры заданы правильно</returns>
=======
// Проверка входных параметров 
// Входные параметры:
//   parameters - проверяемые входные параметры
//     1) repositoryPath - путь к файлу пепозиторию
//     2) quantity - количество генерируемых записей
// Возвращаемое значение:
//   true - если параметры заданы правильно
//   false - если параметры заданы некорректно
>>>>>>> main
bool CheckInputParameters(Dictionary<string, object> parameters)
{
    if (!parameters.ContainsKey("repositoryPath"))
    {
        // не указан путь к файлу репозитория
        Console.WriteLine("Bad path to repository file.");
        ShowHelp();
        return false;
    }

    string repositoryPath = (string)parameters["repositoryPath"];
    if(repositoryPath.Length == 0)
    {
        Console.WriteLine("Bad path to repository file.");
        ShowHelp();
        return false;
    }

    if (!Utils.CanCreateFile(repositoryPath))
    {
        Console.WriteLine("Bad path to repository file: [" + repositoryPath + "]");
        return false;
    }

    int quantity = (int)parameters["quantity"];
    if (quantity < 1)
    {
        Console.WriteLine("Quantity not specified or bad value: [" + quantity + "]");
        return false;
    }
    return true;
}

<<<<<<< HEAD
/// <summary>
/// Генерация клиентов в соответствии с заданными параметрами и сохранение их в CSV репозиторий
/// </summary>
/// <param name="parameters">входные параметры для генерации клиентов</param>
=======
// Генерация клиентов в соответствии с заданными параметрами и сохранение их в CSV репозиторий
// Входные параметры:
//   parameters - входные параметры для генерации клиентов:
//     1) repositoryPath - путь к файлу пепозиторию
//     2) quantity - количество генерируемых записей
// Exceptions:
//     Exception - ошибка
>>>>>>> main
static void GenerateData(Dictionary<string, object> parameters)
{
    int quantity = (int)parameters["quantity"];
    string repositoryPath = (string)parameters["repositoryPath"];

    // генерация клиентов
    ClientGenerator clientGenerator = new();
    List<Client> clients = clientGenerator.Next(quantity);

    // сохранение клиентов в репозитории
    IConfiguration configuration = new CSVFileConfiguration(repositoryPath);
    RepositoryCreator creator = new CSVFileRepositoryCreator();
    IClientRepository? repository = creator.CreateClientRepository(configuration);
    if(repository == null)
    {
        throw new Exception("Repository is not created.");
    }

    repository.Insert(clients);
<<<<<<< HEAD
}
=======
}
>>>>>>> main
