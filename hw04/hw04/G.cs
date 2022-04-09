using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hw04
{
    internal class G
    {
        // [JsonInclude]
        int g1 = 1;
        // [JsonInclude]
        int g2 = 2;

        public override string? ToString()
        {
            return "g1 = " + g1 + "; g2 = " + g2;
        }
    }
}
