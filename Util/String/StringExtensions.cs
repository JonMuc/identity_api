using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Util.String
{
    public static class StringExtensions
    {
        /// <summary>
        /// Formata a string de acordo com os parâmetros especificados (o mesmo de string.Format).
        /// </summary>
        /// <param name="format">String a ser formatada.</param>
        /// <param name="args">Parâmetros para serem substituidos na string.</param>
        /// <returns>String</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return string.Format(format, args);
        }

        /// <summary>
        /// Formata a string de acordo com os parâmetros especificados (o mesmo de string.Format).
        /// </summary>
        /// <param name="format">String a ser formatada.</param>
        /// <param name="provider">Um objeto que permite especificar a cultura em que a string será formatada.</param>
        /// <param name="args">Parâmetros para serem substituidos na string.</param>
        /// <returns>String</returns>
        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            return string.Format(provider, format, args);
        }

        /// <summary>
        /// Substitui tudo o que não está em [a-zA-Z0-9]. (incluindo letras com acento)
        /// </summary>
        /// <param name="text">Texto original.</param>
        /// <param name="replacement">Valor substituto.</param>
        public static string ReplaceNonAZ09(this string text, string replacement)
        {
            const string expr = @"((?![a-zA-Z0-9]).)";
            return Regex.Replace(text, expr, replacement);
        }

        /// <summary>
        /// Remove os acentos da string.
        /// </summary>
        /// <param name="text">Texto com acentos.</param>
        /// <returns>A mesma string porém sem nenhum acento.</returns>
        public static string RemoveAccent(this string text)
        {
            string bad_ = "áéíóúàèìòùâêîôûãõäëïöüçÁÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕÄËÏÖÜÇ";
            string good = "aeiouaeiouaeiouaoaeioucAEIOUAEIOUAEIOUAOAEIOUC";

            var newString = new StringBuilder();
            int index;
            string current;

            for (int i = 0; i < text.Length; i++)
            {
                current = text.Substring(i, 1);
                index = bad_.IndexOf(current);

                if (index >= 0)
                {
                    newString.Append(good[index]);
                }
                else
                {
                    newString.Append(current);
                }
            }

            return newString.ToString();
        }

        /// <summary>
        /// Retorna uma nova string com todas as ocorrências de cada uma das strings especificadas substituídas pela
        /// string especificada em replacement.
        /// </summary>
        /// <param name="text">String contendo o texto original.</param>
        /// <param name="oldChars">Array com os caracteres a serem substituidas no texto original.</param>
        /// <param name="newChar">Caractere para ser utilizado toda vez que um dos caracteres de oldChras for encontrado.</param>
        /// <returns>String com as ocorrencias de oldChars substituidas por newChar.</returns>
        public static string Replace(this string text, char[] oldChars, char newChar)
        {
            foreach (char item in oldChars)
            {
                text = text.Replace(item, newChar);
            }
            return text;
        }

        /// <summary>
        /// Substitui todos os NewLine pela tag HTML BR.
        /// </summary>
        /// <param name="text">Texto original.</param>
        /// <returns>String.</returns>
        public static string NewLineToBr(this string text)
        {
            return text.Replace(Environment.NewLine, "<br/>");
        }

        /// <summary>
        /// Verifica se existem apenas números na string informada.
        /// </summary>
        /// <param name="input">String.</param>
        /// <returns>True se houver apenas números.</returns>
        public static bool HasOnlyNumbers(this string input)
        {
            return (input.Where(c => char.IsDigit(c) == false).Count() == 0);
        }

        /// <summary>
        /// Verifica se uma string é um número inteiro.
        /// </summary>
        /// <param name="input">Texto original.</param>
        /// <returns>String.</returns>
        public static bool IsInteger(this string input)
        {
            int x;
            return int.TryParse(input, out x);
        }

        /// <summary>
        /// Transforma a string em um Md5 Hash.
        /// </summary>
        /// <param name="text">Texto a ser transformado.</param>
        /// <returns>String.</returns>
        public static string Md5Hash(this string text)
        {
            var md5 = new MD5CryptoServiceProvider();
            byte[] bs = Encoding.UTF8.GetBytes(text);
            bs = md5.ComputeHash(bs);
            var hash = new StringBuilder();
            foreach (byte b in bs)
            {
                hash.Append(b.ToString("x2").ToLower());
            }
            return hash.ToString();
        }

        /// <summary>
        /// Converte texto para formato Base64.
        /// </summary>
        /// <param name="text">Texto original.</param>
        /// <returns>String em formato Base64.</returns>
        public static string ToBase64(this string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Le texto codificado em Base64.
        /// </summary>
        /// <param name="text">Texto em formato Base64.</param>
        /// <returns>String.</returns>
        public static string FromBase64(this string text)
        {
            // TODO::
            // Pensar em uma solução para remover o try catch
            try
            {
                var bytes = Convert.FromBase64String(text);
                return Encoding.UTF8.GetString(bytes);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Formata texto com as primeiras lestras em maiúsculo e as demais minusculo para cada palavra.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Capitalize(this string text)
        {
            char[] ca = text.ToLower().ToCharArray();

            foreach (Match m in Regex.Matches(text.ToLower(), @"\b[a-z]"))
            {
                ca[m.Index] = Char.ToUpper(ca[m.Index]);
            }

            return new string(ca);
        }

    }
}
