using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AppGlobal
{
    public static class clsPublic
    {
        public static string Quitar_Caracteres_No_Permitidos(string? input)
        {
            string vResultText = "";

            if (input != null)
            {
                Regex regexCaracteresNoValidos = new Regex("(?:[^a-z0-9 .,-/]|(?<=['\"&<>])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
                string vTexto = regexCaracteresNoValidos.Replace(input, string.Empty);
                vResultText = Regex.Replace(vTexto, @"\s{2,}", " ");
            }

            return vResultText;
        }
        private const string Ek64 = "rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5";
        private const string Iv = "qualityi";

        public static string Encriptar(string input)
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

                    string decrypted = Desencriptar(encrypted);

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

        public static string Desencriptar(string input)
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