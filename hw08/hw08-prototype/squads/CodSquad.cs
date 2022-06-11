using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    public class CodSquad : ClawedSubclass, ISquad, IMyCloneable<CodSquad>
    {
        public string SquadName { get; set; }
        public string SquadDescription { get; set; }

        public CodSquad()
        {
            SquadName = "Отряд тресковые";
            SquadDescription = "Отряд лучепёрых рыб. Отличительные признаки: спинные, " + 
                "анальные и брюшные плавники без колючих лучей.";
        }

        public CodSquad(CodSquad obj) : base(obj)
        {
            SquadName = obj.SquadName;
        }

        public new CodSquad clone()
        {
            return new CodSquad(this);
        }

        public override string? ToString()
        {
            return base.ToString() + "/" + SquadName;
        }

        public override bool Equals(object? obj)
        {
            return obj is CodSquad squad &&
                   base.Equals(obj) &&
                   ClassName == squad.ClassName &&
                   ClassDescription == squad.ClassDescription &&
                   SubClassName == squad.SubClassName &&
                   SubClassDescription == squad.SubClassDescription &&
                   SquadName == squad.SquadName &&
                   SquadDescription == squad.SquadDescription;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), ClassName, ClassDescription, SubClassName, SubClassDescription, SquadName, SquadDescription);
        }

        public new object Clone()
        {
            return new CodSquad(this);
        }
    }
}
