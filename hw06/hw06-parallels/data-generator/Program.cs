using data_generator;
using repository;
using repository.DAL;
using NDesk.Options;
using repository.Factory;

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

/* ClientFileSpecification fileSpecification = new ClientFileSpecification(4, 7);
IEnumerable<Client> e = repository.Get(fileSpecification);
foreach (Client client in e)
{
    Console.WriteLine(client);
}
*/

void ShowHelp()
{
    Console.WriteLine("Usage: data-generator.exe --path=path --quantity=int [--help]");
    Console.WriteLine("An interactive client for loading clients.");
    Console.WriteLine("Options:");
    Console.WriteLine("  path - path to repository file.");
    Console.WriteLine("  quantity - quantity of records to be generated.");
    Console.WriteLine("  help - this help.");
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

// Проверка входных параметров
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

    if (!CanCreateFile(repositoryPath))
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
    // ISerializer<Client> serializer = new ClientCSVSerializer();
    // IClientContext context = new ClientCSVFileContext(repositoryPath, serializer);
    // IClientRepository repository = new ClientRepository(context);
    repository.Insert(clients);
}