using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    public class ClawedSubclass : Fish, ISubClass, IMyCloneable<ClawedSubclass>
    {
        public string SubClassName { get; set; }
        public string SubClassDescription { get; set; }

        public ClawedSubclass()
        {
            SubClassName = "Подкласс когтистые";
            SubClassDescription = "";
        }

        public ClawedSubclass(ClawedSubclass obj) : base(obj)
        {
            SubClassName = obj.SubClassName;
            SubClassDescription = obj.SubClassDescription;
        }

        public new ClawedSubclass clone()
        {
            return new ClawedSubclass(this);
        }

        public override string? ToString()
        {
            return base.ToString() + "/" + SubClassName;
        }

        public override bool Equals(object? obj)
        {
            return obj is ClawedSubclass subclass &&
                   base.Equals(obj) &&
                   ClassName == subclass.ClassName &&
                   ClassDescription == subclass.ClassDescription &&
                   SubClassName == subclass.SubClassName &&
                   SubClassDescription == subclass.SubClassDescription;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), ClassName, ClassDescription, SubClassName, SubClassDescription);
        }

        public new object Clone()
        {
            return new ClawedSubclass(this);
        }
    }
}
