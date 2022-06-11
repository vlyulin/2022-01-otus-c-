using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    public class CartilaginousSubclass : Fish, ISubClass, IMyCloneable<CartilaginousSubclass>
    {
        public string SubClassName { get; set; }
        public string SubClassDescription { get; set; }

        public CartilaginousSubclass()
        {
            this.SubClassName = "Подкласс Хрящевые";
            this.SubClassDescription = "У хрящевых рыб скелет состоит из хрящей, которые, однако, " +
                "вследствие отложения минералов могут становиться довольно твёрдыми.";
        }

        public CartilaginousSubclass(CartilaginousSubclass obj) : base(obj)
        {
            SubClassName = obj.SubClassName;
            SubClassDescription = obj.SubClassDescription;
        }

        public new CartilaginousSubclass clone()
        {
            return new CartilaginousSubclass(this);
        }

        public override string? ToString()
        {
            return base.ToString() + "/" + SubClassName;
        }

        public override bool Equals(object? obj)
        {
            return obj is CartilaginousSubclass subclass &&
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
            return new CartilaginousSubclass(this);
        }
    }
}
