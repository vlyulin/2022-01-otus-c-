/* Copyright (c) Aspiring Technology Pty Ltd 2006 http://aspiring-technology.com */

using System;
using System.Collections.Generic;
using System.Text;
using Aspiring.DataGenerator;
using System.ComponentModel;

namespace Aspiring.DataGenerator.Primitives
{
    [DataGeneratorAttribute(Name = "Lorem Ipsum", ReturnType = typeof(string), Description = "Generates Lorem Ipsum sample text.")]
    public class LoremIpsumGenerator : IDataGenerator<string>
    {
        #region private fields

        private int _paragraphs = 2;
        private string _loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

        #endregion // private fields

        #region public properties

        [Description("The number of Lorem Ipsum paragraphs to generate.")]
        public int Paragraphs
        {
            get { return _paragraphs; }
            set { 
                    if (value < 1)
                        throw new ArgumentOutOfRangeException("Paragraphs", "There must be at least one paragraph" );

                    _paragraphs = value; 
            }
        }

        #endregion // public properties

        #region IDataGenerator<string> Members

        public void Setup()
        {
        }

        public void TearDown()
        {
        }

        public string Next()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Paragraphs; i++)
            {
                if (i > 0)
                    builder.AppendLine();
                
                builder.AppendLine(_loremIpsum);
            }

            return builder.ToString();
        }

        public List<string> Next(int count)
        {
            List<string> result = new List<string>(count);

            for (int i = 0; i < count; i++)
                result.Add(Next());
            
            return result;
        }

        public string ToString(string value)
        {
            return value;
        }

        public string[] ToStringArray(List<string> values)
        {
            return values.ToArray();
        }
        #endregion
    }
}
