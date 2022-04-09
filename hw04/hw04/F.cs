using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hw04
{
    public class F
    {
        int i1, i2, i3, i4, i5;

        // public string str = "Str";
        public int prop { get; set; }

        int[] arr = new int[] { 1, 2, 3 };
        private G clazz { get; set; }

        public F Get() => new()
        {
            i1 = 1,
            i2 = 2,
            i3 = 3,
            i4 = 4,
            i5 = 5,
            clazz = new G()
        };
        public override string? ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
                   "i1 = " + i1 + "; i2 = " + i2 + "; i3 = " + i3 + "; i4 = " + i4 + "; i5 = " + i5
                   + "; prop = " + prop + "; arr = [");
            for(int i = 0; i < arr.Length; i++)
            {
                stringBuilder.Append(arr[i]+",");
            }
            if(arr.Length > 0) stringBuilder.Length--;
            stringBuilder.Append("];");
            if (clazz != null) { stringBuilder.Append(" clazz {" + clazz.ToString() + "};"); };
            return stringBuilder.ToString();
        }
    }
}
