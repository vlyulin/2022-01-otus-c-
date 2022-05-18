/* Copyright (c) Aspiring Technology Pty Ltd 2006 http://aspiring-technology.com */

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Aspiring.DataGenerator;

namespace Aspiring.DataGenerator.Dates
{
    [DataGeneratorAttribute(Name = "Birth Dates", ReturnType = typeof(DateTime), Description = "Generates a random date between a minimun and maximum age range. Eg. Generate a birthdate between a minimun and maximum age")]
    public class BirthDateGenerator : IDataGenerator<DateTime>
    {
        #region private fields
        Random random = new Random();
        private int _minAge = 18;
        private int _maxAge = 85;
        private string _formatString = @"MM/dd/yyyy";
        #endregion //private fields

        #region Public Methods
        [Category("Age Range")]
        [Description("The minimum age allowed for the birthdate. Must be less than MaxAge.")]
        public int MinAge
        {
            get { return _minAge; }
            set 
            { 
                if (value > MaxAge)
                    throw new ArgumentOutOfRangeException("MinAge", "MinAge must be less than or equal to MaxAge." );

                _minAge = value;
            }
        }

        [Category("Age Range")]
        [Description("The maximum age allowed for the birthdate. Must be greater than MinAge.")]
        public int MaxAge
        {
            get { return _maxAge; }
            set 
            { 
                 if (value < MinAge)
                    throw new ArgumentOutOfRangeException("MaxAge", "MaxAge must be greater than or equal to MinAge." );
               
                _maxAge = value; 
            }
        }
        #endregion // Public Methods

        #region IDataGenerator<DateTime> Members

        [Category("String Formatting")]
        [Description("Standard DateTime format string. eg. MM/dd/yyyy or dd-MMM-yyyy")]
        public string FormatString
        {
            get
            {
                return _formatString;
            }
            set
            {
                _formatString = value;
            }
        }

        public void Setup()
        {
        }

        public void TearDown()
        {
        }

        public DateTime Next()
        {
            // calculate year
            int yearDiff = random.Next(MinAge, MaxAge+1);
            int year = DateTime.Now.Year - yearDiff;

            // calculate month
            int month = random.Next(1,13);

            // calculate the day, must be valid for the year and month
            int day = random.Next(1, maxDaysForMonth(year, month) + 1);

            return new DateTime(year, month, day);
        }

        public List<DateTime> Next(int count)
        {
            List<DateTime> result = new List<DateTime>(count);
            
            for (int i = 0; i < count; i++)
                result.Add(Next());

            return result;
        }

        #endregion

        #region private methods

        private int maxDaysForMonth(int year, int month)
        {
            int result = 0;

            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8: 
                case 10:
                case 12: result = 31; break;
 
                case 4:
                case 6: 
                case 9:
                case 11: result = 30; break;

                case 2 : 
                    if (DateTime.IsLeapYear(year))
                        result = 29;
                    else
                        result = 28;
                    break;
            }

            return result;
        }

        #endregion //private methods


        #region IDataGenerator<DateTime> Members

        public string ToString(DateTime value)
        {
            return value.ToString(FormatString);
        }

        public string[] ToStringArray(List<DateTime> values)
        {
            List<string> result = new List<string>(values.Count);
            
            foreach (DateTime d in values)
	        {
                result.Add(d.ToString(FormatString));
            }

            return result.ToArray();
        }

        #endregion
    }
}
