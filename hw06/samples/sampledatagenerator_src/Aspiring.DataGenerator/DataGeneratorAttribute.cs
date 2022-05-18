/* Copyright (c) Aspiring Technology Pty Ltd 2006 http://aspiring-technology.com */

using System;
using System.Collections.Generic;
using System.Text;

namespace Aspiring.DataGenerator
{
    public class DataGeneratorAttribute : Attribute
    {
        private string _description;
        private string _name;
        private Type _returnType;

        public Type ReturnType
        {
            get { return _returnType; }
            set { _returnType = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
