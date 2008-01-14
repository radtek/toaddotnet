using System;
using System.Collections.Generic;
using System.Text;

namespace Connexion.utils.Oracle
{
    /// <summary>
    /// Custom Object to hold tnsentry information
    /// </summary>
    public class TnsEntryType
    {
        #region instance members

        private string _tnsnameEntry;
        private string _serviceName;
        private string _sid;
        private string _hostName;
        private int _portNumber;

        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }

        public string Sid
        {
            get { return _sid; }
            set { _sid = value; }
        }

        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        public int PortNumber
        {
            get { return _portNumber; }
            set { _portNumber = value; }
        }

        public string TnsnameEntry
        {
            get { return _tnsnameEntry; }
            set { _tnsnameEntry = value; }
        }

        //Constructor
        public TnsEntryType() : this(string.Empty, string.Empty, string.Empty, string.Empty, 0) { }

        public TnsEntryType(string tnsnameEntry, string serviceName, string sid, string hostName, int portNumber)
        {
            this._tnsnameEntry = tnsnameEntry;
            this._serviceName = serviceName;
            this._sid = sid;
            this._hostName = hostName;
            this._portNumber = portNumber;
        }

        public override string ToString()
        {
            return _tnsnameEntry;
        }

        #endregion
    }
}
