using System;
using System.Collections.Generic;
using System.Text;

namespace ULib
{
    public class DbObjectItem
    {
        private string name;
        private string type;

        public DbObjectItem(string name, string type)
        {
            Name = name;
            Type = type;
        }

        #region properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }

    }
}
