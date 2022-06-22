using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    // http://900igr.net/prezentatsii/biologija/Vneshnee-stroenie-ryby/003-Klassifikatsija-Ryb.html
    public class Fish : IClass, IMyCloneable<Fish>, ICloneable
    {
        public string ClassName { get; set; }

        public string ClassDescription { get; set; }

        public Fish()
        {
            ClassName = "Класс Рыбы";
            ClassDescription = "Парафилетическая группа (по современной кладистической классификации) водных позвоночных животных.";
        }

        public Fish(Fish obj)
        {
            ClassName = obj.ClassName;
            ClassDescription = obj.ClassDescription;
        }

        public Fish clone()
        {
            return new Fish(this);
        }

        public override string? ToString()
        {
            return ClassName;
        }

        public override bool Equals(object? obj)
        {
            return obj is Fish fish &&
                   ClassName == fish.ClassName &&
                   ClassDescription == fish.ClassDescription;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClassName, ClassDescription);
        }

        public object Clone()
        {
            return new Fish(this);
        }
    }
}
