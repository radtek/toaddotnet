using System;
using System.Collections.Generic;
using System.Text;

namespace PluginTypes
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class ModuleInfoAttribute : Attribute
    {
        public ModuleInfoAttribute() { }
        private string name, description, author, language;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Language
        {
            get { return language; }
            set { language = value; }
        }      
    }
}
