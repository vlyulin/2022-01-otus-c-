﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw03_solid.factories
{
    abstract class GameFactory
    {
        public abstract IGameStrategy CreateStrategy();
    }
}
