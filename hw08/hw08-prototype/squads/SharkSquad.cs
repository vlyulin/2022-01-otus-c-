using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    public class SharkSquad : CartilaginousSubclass, ISquad, IMyCloneable<SharkSquad>
    {
        public string SquadName { get; set; }
        public string SquadDescription { get; set; }
        public SharkSquad()
        {
            SquadName = "Отряд акулы";
            SquadDescription = "надотряд хрящевых рыб (Chondrichthyes), относящийся к подклассу пластиножаберных "+
                "(Elasmobranchii) и обладающий следующими отличительными особенностями: удлинённое тело более или "+
                "менее торпедообразной формы, большой гетероцеркальный хвостовой плавник, обычно много острых "+
                "зубов на каждой челюсти";
        }

        public SharkSquad(SharkSquad obj) : base(obj)
        {
            SquadName = obj.SquadName;
            SquadDescription = obj.SquadDescription;
        }

        public new SharkSquad clone()
        {
            return new SharkSquad(this);
        }

        public override string? ToString()
        {
            return base.ToString() + "/" + SquadName;
        }

        public override bool Equals(object? obj)
        {
            return obj is SharkSquad squad &&
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
            return new SharkSquad(this);
        }
    }
}
