using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace AppGlobal
{
    public static class clsHelper
    {
        private const string Ek64 = "rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5";
        private const string Iv = "qualityi";
        public static string Cifrado(string input)
        {
            try
            {
                byte[] ivBytes = Encoding.ASCII.GetBytes(Iv);
                byte[] encryptionKey = Convert.FromBase64String(Ek64);
                byte[] buffer = Encoding.UTF8.GetBytes(input);

                using (var des = TripleDES.Create())
                {
                    des.Key = encryptionKey;
                    des.IV = ivBytes;

                    string encrypted = Convert.ToBase64String(
                        des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));

                    string decrypted = Descifrar(encrypted);

                    if (decrypted != input)
                        throw new Exception("El algoritmo de encripción tipo Erik dice que no coincide el patrón");

                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Encriptar: " + ex.Message, ex);
            }
        }

        public static string Descifrar(string input)
        {
            try
            {
                byte[] ivBytes = Encoding.ASCII.GetBytes(Iv);
                byte[] encryptionKey = Convert.FromBase64String(Ek64);
                byte[] buffer = Convert.FromBase64String(input);

                using (var des = TripleDES.Create())
                {
                    des.Key = encryptionKey;
                    des.IV = ivBytes;

                    return Encoding.UTF8.GetString(
                        des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Desencriptar: " + ex.Message, ex);
            }
        }

    }
}
