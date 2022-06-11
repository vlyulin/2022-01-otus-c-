using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw08_prototype
{
    public class StingraySquad : CartilaginousSubclass, ISquad, IMyCloneable<StingraySquad>
    {
        public string SquadName { get; set; }
        public string SquadDescription { get; set; }
        public StingraySquad()
        {
            SquadName = "Отряд скаты";
            SquadDescription = "Для скатов характерно сильно уплощённое тело и большие грудные плавники, " +
                          " сросшиеся с головой. Пасть, ноздри и пять пар жабр находятся на плоской и, " + 
                          "как правило, светлой нижней стороне. Хвост бичеобразной формы. " + 
                          "Большинство скатов живёт в морской воде, однако существует " + 
                          "и несколько пресноводных видов";
        }

        public StingraySquad(StingraySquad obj) : base(obj)
        {
            SquadName = obj.SquadName;
            SquadDescription = obj.SquadDescription;
        }

        public new StingraySquad clone()
        {
            return new StingraySquad(this);
        }

        public override string? ToString()
        {
            return base.ToString() + "/" + SquadName;
        }

        public override bool Equals(object? obj)
        {
            return obj is StingraySquad squad &&
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
            return new StingraySquad(this);
        }
    }
}
