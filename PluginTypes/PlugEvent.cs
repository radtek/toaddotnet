using System;
using System.Collections.Generic;
using System.Text;

namespace PluginTypes
{
    public class PlugEvent
    {
        #region evenements
        // le prototype des fonctions charg�es de traier l'�vt
        public delegate void _evtHandler(object sender, string data);

        //le pool des gestionnaires d'�vts
        public event _evtHandler evtHandler;

        //m�thode de demande d'�mission d'ujn �vt
        public void Send(string data)
        {
            evtHandler(this, data);
        }
        #endregion

        #region constructeur
        public PlugEvent()
        {
            
        }
        #endregion
    }
}
