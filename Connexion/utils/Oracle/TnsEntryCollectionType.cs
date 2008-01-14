using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Connexion.utils.Oracle
{
    /// <summary>
    /// Tns Entry Collection type
    /// </summary>
    public class TnsEntryCollectionType : ArrayList
    {
        #region instance members

        private string _tnsFile;

        public string TnsFileName
        {
            get { return this._tnsFile; }
        }

        public TnsEntryCollectionType(string tnsFile)
        {
            this._tnsFile = tnsFile;
        }

        public TnsEntryType Add(TnsEntryType obj)
        {
            base.Add(obj);
            return obj;
        }

        public TnsEntryType Add()
        {
            return Add(new TnsEntryType());
        }

        new public TnsEntryType this[int index]
        {
            get { return (TnsEntryType)base[index]; }
            set { base[index] = value; }
        }

        #endregion
    }

}
