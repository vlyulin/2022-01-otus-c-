/* Copyright (c) Aspiring Technology Pty Ltd 2006 http://aspiring-technology.com */

using System;
using System.Collections.Generic;
using System.Text;

namespace Aspiring.DataGenerator
{
    public interface IDataGenerator<T>
    {
        void Setup();
        void TearDown();
        T Next();
        List<T> Next(int count);
        string ToString(T value);
        string[] ToStringArray(List<T> values);
    }
}
