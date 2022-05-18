/* Copyright (c) Aspiring Technology Pty Ltd 2006 http://aspiring-technology.com */

using System;
using System.Collections.Generic;
using System.Text;
using Aspiring.DataGenerator;
using System.ComponentModel;

namespace Aspiring.DataGenerator.Primitives
{
    [DataGeneratorAttribute(Name = "Integers", ReturnType = typeof(int), Description = "Generates random integer values.")]
    public class IntegerGenerator : IDataGenerator<int>
    {
        private Random _random;
        private int _seed = Environment.TickCount;
        private int _MinValue = 0;
        private int _maxValue = 999;

        [Description("The seed number for the Random generator. Defaults to the system clock.")]
        public int Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }

        [Description("The smallest integer value allowed in the sequence.")]
        public int MinValue
        {
            get { return _MinValue; }
            set { 
                    if (value > MaxValue)
                        throw new ArgumentOutOfRangeException("MinValue", "MinValue must be less than or equal to MaxValue." );

                    _MinValue = value; 
            }
        }

        [Description("The largest integer value allowed in the sequence.")]
        public int MaxValue
        {
            get { return _maxValue; }
            set { 
                    if (value < MinValue)
                        throw new ArgumentOutOfRangeException("MaxValue", "MaxValue must be greater than or equal to MinValue." );

                    _maxValue = value; 
            }
        }

        #region IDataGenerator<int> Members

        public void Setup()
        {
            // Initialization code that gets called BEFORE any generation of data.
            // This is the place you would load data files, initialise random 
            // generators and do anything that is required ONCE per generation 
            // session.
            _random = new Random(Seed);
        }

        public void TearDown()
        {
            // Finalisation code that gets called AFTER any generation of data.
            // This is the place you would close file handles, free any unmanaged 
            // resources held.
        }

        public int Next()
        {
            // return 1 element randomly generated between MinValue and MaxValue.
            return _random.Next(MinValue, MaxValue+1);
        }

        public List<int> Next(int count)
        {
            List<int> result = new List<int>(count);

            for (int i = 0; i < count; i++)
                result.Add(Next());
            
            return result;
        }

        public string ToString(int value)
        {
            return value.ToString();
        }

        public string[] ToStringArray(List<int> values)
        {
            List<string> result = new List<string>(values.Count);
            
            foreach(int i in values)
                result.Add(ToString(i));

            return result.ToArray();
        }
        #endregion
    }
}
