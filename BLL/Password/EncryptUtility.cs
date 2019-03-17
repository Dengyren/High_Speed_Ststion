using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BLL.Password
{
    public class EncryptUtility
    {
        private const string encryptKey = "2018604";
        private static readonly byte[] keyvi = new byte[] { 0x01, 0x03, 0x05, 0x07, 0x09, 0x0A, 0x0C, 0x0E };

        public static string DesEncrypt(string normalTxt)//加密
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);
            var key = Encoding.UTF8.GetBytes(encryptKey.PadLeft(8, '0').Substring(0, 8));
            using(MemoryStream ms = new MemoryStream())
            {
                var encry = new DESCryptoServiceProvider();
                CryptoStream cs = new CryptoStream(ms, encry.CreateEncryptor(key, keyvi), CryptoStreamMode.Write);
                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
        public static string DesDecrypt(string securityTxt)//解密  
        {
            try
            {
                var bytes = Convert.FromBase64String(securityTxt);
                var key = Encoding.UTF8.GetBytes(encryptKey.PadLeft(8, '0').Substring(0, 8));
                using(MemoryStream ms = new MemoryStream())
                {
                    var descrypt = new DESCryptoServiceProvider();
                    CryptoStream cs = new CryptoStream(ms, descrypt.CreateDecryptor(key, keyvi), CryptoStreamMode.Write);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch(Exception)
            {
                return string.Empty;
            }
        }
    }
}