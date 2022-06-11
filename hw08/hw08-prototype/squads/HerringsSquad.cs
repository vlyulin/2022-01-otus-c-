using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    internal class HerringsSquad : ClawedSubclass, ISquad, IMyCloneable<HerringsSquad>
    {
        public string SquadName { get; set; }
        public string SquadDescription { get; set; }
        public HerringsSquad()
        {
            SquadName = "Отряд сельдеобразные";
            SquadDescription = "Отряд лучепёрых рыб. Это примитивные костистые рыбы. Плавательный пузырь у них " + 
                "соединён каналом с пищеводом и имеет отростки, которые входят впереди в полость черепа";
        }

        public HerringsSquad(HerringsSquad obj) : base(obj)
        {
            SquadName = obj.SquadName;
            SquadDescription = obj.SquadDescription;
        }

        public new HerringsSquad clone()
        {
            return new HerringsSquad(this);
        }

        public override string? ToString()
        {
            return base.ToString() + "/" + SquadName;
        }

        public override bool Equals(object? obj)
        {
            return obj is HerringsSquad squad &&
                   ClassName == squad.ClassName &&
                   ClassDescription == squad.ClassDescription &&
                   SubClassName == squad.SubClassName &&
                   SubClassDescription == squad.SubClassDescription &&
                   SquadName == squad.SquadName &&
                   SquadDescription == squad.SquadDescription;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClassName, ClassDescription, SubClassName, SubClassDescription, SquadName, SquadDescription);
        }

        public new object Clone()
        {
            return new HerringsSquad(this);
        }

    }
}
