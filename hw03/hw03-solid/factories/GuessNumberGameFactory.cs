using hw03_solid.strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw03_solid.factories
{

    /// <summary>
    /// SRP – принцип единой ответственности: 
    /// единственная цель фабрики - сборка игры со всеми инициализациями и выдача готового экземпляра.
    /// LCP - Принцип подстановки Лисков
    /// В качестве генератора "загаданного" числа можно использовать производный класс LiskovGuessNumberGeneratorStrategy
    /// вместо родительского класса GuessNumberGeneratorStrategy. Производный класс LiskovGuessNumberGeneratorStrategy
    /// не нарушает определение типа родительского класса и его поведение.
    /// </summary>
    class GuessNumberGameFactory : GameFactory
    {
        public override IGameStrategy CreateStrategy()
        {
            IAppInit appInit = new AppInitFromFile();
            Config config = appInit.LoadConfig();

            config.numberGenerator = new LiskovGuessNumberGeneratorStrategy(config.RangeFrom,config.RangeTo);
            return new GuessNumberGameStrategy(config);
        }
    }
}
