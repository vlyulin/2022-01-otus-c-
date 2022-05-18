/* Copyright (c) Aspiring Technology Pty Ltd 2006 http://aspiring-technology.com */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Aspiring.DataGenerator;

namespace DataGenerator
{
    public partial class MainForm : Form
    {
        #region private fields

        List<DataGeneratorInfo> generators = new List<DataGeneratorInfo>();
        PropertyGrid propertyGrid = new PropertyGrid();
        DataGeneratorInfo currentDataGeneratorInfo = null;
        object currentGenerator = null;

        #endregion // private fields

        #region constructor

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion // constructor

        #region event handlers

        private void MainForm_Load(object sender, EventArgs e)
        {
            // search for and build a list of generators
            findAllDataGenerators(Path.GetDirectoryName(Application.ExecutablePath));
            listBoxGenerators.DataSource = generators;

            // add the property grid to the form
            splitContainerGenerators.Panel2.Controls.Add(propertyGrid);
            propertyGrid.Dock = DockStyle.Fill;

            comboStyle.SelectedIndex = 0;
            comboQuantity.SelectedIndex = 0;
        }

        private void listBoxGenerators_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxGenerators.SelectedIndex > -1)
            {
                currentDataGeneratorInfo = (DataGeneratorInfo)listBoxGenerators.Items[listBoxGenerators.SelectedIndex];
                toolTip1.SetToolTip(listBoxGenerators, currentDataGeneratorInfo.Attribute.Description);

                // construct the generator and connect it to the property grid
                Type t = currentDataGeneratorInfo.DataGeneratorType;
                currentGenerator = Activator.CreateInstance(t);
                propertyGrid.SelectedObject = currentGenerator;

                /* initially I had the generators generic types which required this code to construct.
                 * I decided not to use a generic type, but will keep this code just in case!
                // construct the generator and connect it to the property grid
                Type tg = currentDataGeneratorInfo.DataGeneratorType;
                Type[] typeArgs = {currentDataGeneratorInfo.Attribute.ReturnType.GetType()};
                Type constructed = tg.MakeGenericType(typeArgs);
                currentGenerator = Activator.CreateInstance(constructed);
                propertyGrid.SelectedObject = currentGenerator;
                */
            }
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            int count;
            if (!int.TryParse(comboQuantity.Text, out count))
            {
                count = 10;
            }

            generate(count);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.AppendAllText(saveFileDialog.FileName, textBoxResults.Text);
            }
        }
        #endregion // event handlers

        #region private methods

        private void findAllDataGenerators(string path)
        {
            string[] files = Directory.GetFiles(path, "*.dll");
            foreach (string file in files)
            {
                Assembly a = Assembly.LoadFile(file);
                Type[] types = a.GetTypes();
                foreach(Type t in types)
                {
                    object[] attributes = t.GetCustomAttributes(typeof(Aspiring.DataGenerator.DataGeneratorAttribute), true);
                    if (attributes.Length == 1)
                    {
                        DataGeneratorInfo g = new DataGeneratorInfo();
                        g.Assembly = a;
                        g.DataGeneratorType = t;
                        g.Attribute = (DataGeneratorAttribute)attributes[0];
                        generators.Add(g);
                    }
                }
            }
        }

        private void generate(int count)
        {
            IEnumerable q;
            currentGenerator.GetType().InvokeMember("Setup", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, currentGenerator, new object[] {});
            try
            {
                q = (IEnumerable)currentGenerator.GetType().InvokeMember("Next", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, currentGenerator, new object[] {count});
            }
            finally
            {
                currentGenerator.GetType().InvokeMember("TearDown", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, currentGenerator, new object[] {});
            }
            
            switch (comboStyle.SelectedIndex) 
            {
                case 0 : // CR/LF
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (object o in q)
                        builder.AppendLine(getObjectString(o));

                    textBoxResults.Text = builder.ToString();
                    break;
                }
                case 1 : // CSV
                case 2 : // CSV with values quoted
                {
                    StringBuilder builder = new StringBuilder();
                    foreach (object o in q)
                    {
                        string s = getObjectString(o);
                        if (comboStyle.SelectedIndex == 2)
                            s = quotedString(s);
                        builder.Append(s);
                        builder.Append(",");
                    }
                    builder.Remove(builder.Length-1,1);
                    textBoxResults.Text = builder.ToString();
                    break;
                }

                case 3 : // string[]
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine("string[] data = new string[] { ");
                    foreach (object o in q)
                    {
                        builder.AppendLine("@\"" + getObjectString(o) + "\",");
                    }

                    // remove the last CR/LF and comma
                    builder.Remove(builder.Length-3,3);
                    
                    builder.Append(" };");
                    textBoxResults.Text = builder.ToString();
                    break;
                }
                case 4 : // C# array
                case 5 : // C# array with values quoted
                {
                    StringBuilder builder = new StringBuilder();
                    string arrayType = currentDataGeneratorInfo.Attribute.ReturnType.ToString();
                    builder.AppendLine(arrayType + "[] data = new " + arrayType + "[] {");
                    foreach (object o in q)
                    {
                        string s = getObjectString(o);
                        if (comboStyle.SelectedIndex == 5)
                            s = quotedString(s);
                        
                        builder.AppendLine(s + ",");
                    }
                    
                    // remove the last CR/LF and comma
                    builder.Remove(builder.Length-3,3);
                    
                    builder.Append(" };");
                    textBoxResults.Text = builder.ToString();
                    break;
                }
            }
        }

        private string quotedString(string s)
        {
            // double any quotes in the original string
            s = s.Replace("\"", "\"\"");
            return "\"" + s + "\""; 
        }

        private string getObjectString(object o)
        {
            return (string)currentGenerator.GetType().InvokeMember("ToString", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, currentGenerator, new object[] { o });
        }

        #endregion // private methods

    }
}