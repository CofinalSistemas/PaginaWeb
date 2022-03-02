using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenio.BLL
{
    public static class Encriptar
    {
        public static string encrypt(string ToEncrypt)
        {
           
           return Convert.ToBase64String(Encoding.ASCII.GetBytes(ToEncrypt));
            
        }
        public static string decrypt(string cypherString)
        {
           
           return Encoding.ASCII.GetString(Convert.FromBase64String(cypherString));
           
        }
        public static string encryptint(int ToEncrypt)
        {

            return Convert.ToBase64String(BitConverter.GetBytes(ToEncrypt));

        }
        public static int decryptint(string cypherString)
        {

            return BitConverter.ToInt32(Convert.FromBase64String(cypherString),0);

        }

    }
}
