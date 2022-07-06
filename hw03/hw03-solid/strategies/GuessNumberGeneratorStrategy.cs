using hw03_solid.strategies;

namespace hw03_solid
{
    public class GuessNumberGeneratorStrategy : IGuessNumberGeneratorStrategy
    {
        public int _rangeFrom { get; set; }
        public int _rangeTo { get; set; }
        public GuessNumberGeneratorStrategy(int from, int to)
        {
            _rangeFrom = from;
            _rangeTo = to;
        }

        public int GuessNumber()
        {
            Random r = new Random();
            return r.Next(_rangeFrom, _rangeTo);
        }
    }
}