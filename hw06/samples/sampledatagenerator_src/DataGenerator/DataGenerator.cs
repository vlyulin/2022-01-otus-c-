/* Copyright (c) Aspiring Technology Pty Ltd 2006 http://aspiring-technology.com */

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Aspiring.DataGenerator;

namespace DataGenerator
{
    class DataGeneratorInfo
    {
        Assembly _assembly;
        DataGeneratorAttribute _attribute;
        Type _dataGeneratorType;

        public Assembly Assembly
        {
            get { return _assembly; }
            set { _assembly = value; }
        }

        public Type DataGeneratorType
        {
            get { return _dataGeneratorType; }
            set { _dataGeneratorType = value; }
        }

        public DataGeneratorAttribute Attribute
        {
            get { return _attribute; }
            set { _attribute = value; }
        }

        public override string ToString()
        {
            return Attribute.Name;
        }

    }
}
