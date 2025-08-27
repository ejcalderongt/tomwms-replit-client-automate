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
    }
}
