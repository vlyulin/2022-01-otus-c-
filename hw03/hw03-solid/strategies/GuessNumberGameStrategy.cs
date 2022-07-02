using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw03_solid
{
    /// <summary>
    /// SRP – принцип единой ответственности: 
    /// класс определяет конкретную реализацию сценария игры на основе заданной конфигурации.
    /// OCP - принцип открытости/закрытости:
    /// При появлении новых требование, таких как новый алгоритм генерации числа 
    /// или ввод/вывод в silence режиме, не потребуется дорабатывать именно этот класс.
    /// Для изменения существует следующие варианты:
    ///     a) для новой версии генерации "загаданного" числа достаточно задать новую реализацию в конфигурации.
    ///     b) для изменения сценария игры создать новый класс - стратегию реализовав интерфейс IGameStrategy.
    ///     с) для перенаправления ввода/вывода достаточно передать требуемые streams через параметры.
    /// Принцип подстановки Лисков (LCP):
    /// Для класса GuessNumberGameStrategy не имеет значения конкретная реализация генерации "загаданного" числа.
    /// Принцип инверсии зависимостей (DIP):
    /// Передача в класс GuessNumberGameStrategy потоков ввода/вывода и генерации "загаданного" числа через конфигурацию.
    /// </summary>
    public class GuessNumberGameStrategy : IGameStrategy
    {
        private Config _config;

        public GuessNumberGameStrategy(Config config)
        {
            if(config.numberGenerator == null)
            {
                throw new ArgumentNullException("numberGenerator is null.");
            }
            this._config = config;
        }

        public void Play(TextReader input, TextWriter output)
        {
            int intendedNumber = _config.numberGenerator.GuessNumber();
            output.WriteLine("Try to guess an int number from " + _config.RangeFrom 
                + " to " + _config.RangeTo + " in " + _config.AttemptsNumber + " attempts.");

            for (int i = 1; i <= _config.AttemptsNumber; i++)
            {
                output.Write("Guess the number: ");
                string? enteredValue = input.ReadLine();

                int enteredNumber;
                if (!Int32.TryParse(enteredValue, out enteredNumber))
                {
                    output.WriteLine("Entered value is not int number.");
                    continue;
                }

                if (enteredNumber < intendedNumber)
                {
                    output.WriteLine("Try entering a lager number.");
                }
                else if (enteredNumber > intendedNumber)
                {
                    output.WriteLine("Try entering a lower number.");
                }
                else
                {
                    output.WriteLine("Bingo!");
                    return;
                }
                output.WriteLine((_config.AttemptsNumber - i) + " attempts left.");
            }
            output.WriteLine("You lost.");
        }
    }
}
