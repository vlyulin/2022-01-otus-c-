namespace hw05_delegates_and_events
{
    // 2. Написать класс, обходящий каталог файлов и выдающий событие при нахождении каждого файла;
    // 3. Оформить событие и его аргументы с использованием .NET соглашений: public event EventHandler FileFound;
    //    FileArgs – будет содержать имя файла и наследоваться от EventArgs
    // 4. Добавить возможность отмены дальнейшего поиска из обработчика;

    public class FileArgs : EventArgs
    {
        public string filePath { get; set; }
        public FileArgs(string filePath = "")
        {
            this.filePath = filePath;
        }
    }
}
