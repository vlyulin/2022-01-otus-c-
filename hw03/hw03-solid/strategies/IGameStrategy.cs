using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw03_solid
{
    public interface IGameStrategy
    {
        public void Play(TextReader input, TextWriter output);
    }
}
