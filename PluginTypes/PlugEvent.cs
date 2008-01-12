using System;
using System.Collections.Generic;
using System.Text;

namespace PluginTypes
{
    public class PlugEvent
    {
        #region evenements
        // le prototype des fonctions chargées de traier l'évt
        public delegate void _evtHandler(object sender, string data);

        //le pool des gestionnaires d'évts
        public event _evtHandler evtHandler;

        //méthode de demande d'émission d'ujn évt
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
