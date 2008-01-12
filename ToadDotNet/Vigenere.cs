using System;
using System.Collections.Generic;
using System.Text;

namespace ToadDotNet
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
            // //Je code la clé avec une autre clé qui reprend tout les caractères
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
                // Equivalent à :
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
                // Equivalent à :
                decrypt +=
                    Convert.ToString(
                        Convert.ToChar(((Convert.ToInt16(ChaineaDecrypter[i]) - Convert.ToInt16(key[i%key.Length]))%
                                        65536)));
            }

            return decrypt;
        }
    }
}