using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    public class CyprinidsSquad : ClawedSubclass, ISquad, IMyCloneable<CyprinidsSquad>
    {
        public string SquadName { get; set; }
        public string SquadDescription { get; set; }

        public CyprinidsSquad()
        {
            SquadName = "Oтряд карпообразные";
            SquadDescription = "Отряд лучепёрых рыб (Actinopterygii). Характеризуются наличием веберова аппарата; " + 
                "плавательный пузырь соединён с кишечником. Преимущественно пресноводные рыбы.";
        }

        public CyprinidsSquad(CyprinidsSquad obj) : base(obj)
        {
            SquadName = obj.SquadName;
            SquadDescription = obj.SquadDescription;
        }

        public new CyprinidsSquad clone()
        {
            return new CyprinidsSquad(this);
        }

        public override string? ToString()
        {
            return base.ToString() + "/" + SquadName;
        }

        public override bool Equals(object? obj)
        {
            return obj is CyprinidsSquad squad &&
                   ClassName == squad.ClassName &&
                   ClassDescription == squad.ClassDescription &&
                   SubClassName == squad.SubClassName &&
                   SubClassDescription == squad.SubClassDescription &&
                   SquadName == squad.SquadName &&
                   SquadDescription == squad.SquadDescription;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public new object Clone()
        {
            return new CyprinidsSquad(this);
        }
    }
}
