/******************************************************************************
  Toad.net (ToadDotNet)
  Copyright (C) 2008 Pierre Delporte � Tous droits r�serv�s.

  This program is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 
  ----------------------------------------------------------------------------- 
 
  Ce programme est un logiciel libre ; vous pouvez le redistribuer ou le
  modifier suivant les termes de la �GNU General Public License� telle que
  publi�e par la Free Software Foundation : soit la version 3 de cette
  licence, soit toute version ult�rieure.
  
  Ce programme est distribu� dans l�espoir qu�il vous sera utile, mais SANS
  AUCUNE GARANTIE : sans m�me la garantie implicite de COMMERCIALISABILIT�
  ni d�AD�QUATION � UN OBJECTIF PARTICULIER. Consultez la Licence G�n�rale
  Publique GNU pour plus de d�tails.
  
  Vous devriez avoir re�u une copie de la Licence G�n�rale Publique GNU avec
  ce programme ; si ce n�est pas le cas, consultez :
  <http://www.gnu.org/licenses/>.
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace ULib
{
    public class Vigenere
    {
        private string key;

        public string Key
        {
            get { return key; }
        }

        public Vigenere(string cle)
        {
            // //Je code la cl� avec une autre cl� qui reprend tout les caract�res
            // #region "Mis en place de Alphabet"
            // StringBuilder _key = new StringBuilder();
            // for (int i = 0; i < 65535; i++)
            // {
            // _key.Append((char)i);
            // }
            // key = _key.ToString();
            // #endregion
            // key = Encrypt(cle)

            key = cle;
        }

        public string Encrypt(string ChaineaEncoder)
        {
            string encrypt = null;

            for (int i = 0; i < ChaineaEncoder.Length; i++)
            {
                // encrypt += (char)(((short)key[i % key.Length] + (short)ChaineaEncoder[i])%65536);
                // Equivalent � :
                //int c = (Convert.ToInt16(key[i%key.Length]) + Convert.ToInt16(ChaineaEncoder[i]))%65536;
                //Console.Write(c + " "  + "|");

                encrypt +=
                    String.Format("{0:x2}",
                                  (Convert.ToInt16(key[i%key.Length]) + Convert.ToInt16(ChaineaEncoder[i]))%65536);
                //Convert.ToString(Convert.ToChar((Convert.ToInt16(key[i%key.Length]) + Convert.ToInt16(ChaineaEncoder[i]))%65536));
                //encrypt += (char)((Convert.ToInt16(key[i % key.Length]) + Convert.ToInt16(ChaineaEncoder[i])) % 65536);
            }

            return encrypt;
        }

        public string Decrypt(string ChaineaDecrypter)
        {
            string decrypt = null;
            for (int i = 0; i < ChaineaDecrypter.Length; i++)
            {
                decrypt += (char) Convert.ToInt16(ChaineaDecrypter.Substring(i, 2), 16);
                i++;
            }
            ChaineaDecrypter = decrypt;
            decrypt = null;
            //int myHex = Convert.ToInt32(ChaineaDecrypter.Substring(i,2), 16);


            for (int i = 0; i < ChaineaDecrypter.Length; i++)
            {
                // decrypt += (Char)(((short)ChaineaDecrypter[i] - (short)(key[i % key.Length])) % 65536);
                // Equivalent � :
                decrypt +=
                    Convert.ToString(
                        Convert.ToChar(((Convert.ToInt16(ChaineaDecrypter[i]) - Convert.ToInt16(key[i%key.Length]))%
                                        65536)));
            }

            return decrypt;
        }
    }
}