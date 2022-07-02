using hw03_solid.strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw03_solid
{
    /// <summary>
    /// SRP – принцип единой ответственности: 
    /// зона ответственности класса - хранение настроек игры.
    /// </summary>
    public class Config
    {
        public int AttemptsNumber { get; set; }
        public int RangeFrom { get; set; }
        public int RangeTo { get; set; }
        public IGuessNumberGeneratorStrategy numberGenerator { get; set; }
    }
}
