using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw03_solid.strategies
{
    public class LiskovGuessNumberGeneratorStrategy :GuessNumberGeneratorStrategy
    {

        public LiskovGuessNumberGeneratorStrategy(int from, int to) : base(from, to)
        {            
        }

        public int GuessNumber()
        {
            Random r = new Random();
            int interim = r.Next(base._rangeFrom, base._rangeTo);
            return Math.Min(interim + 1, base._rangeTo);
        }
    }
}
