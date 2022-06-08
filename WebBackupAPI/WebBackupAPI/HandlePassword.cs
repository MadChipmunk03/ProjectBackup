using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebBackupAPI
{
    public class HandlePassword
    {
        public static string RandomPassword(int length)
        {
            using (var crypto = new RNGCryptoServiceProvider())
            {
                var bits = (length * 6);
                var byte_size = ((bits + 7) / 8);
                var bytesarray = new byte[byte_size];
                crypto.GetBytes(bytesarray);
                return Convert.ToBase64String(bytesarray);
            }
        }
    }
}
