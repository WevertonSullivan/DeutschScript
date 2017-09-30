using System.Collections.Generic;
using System.IO;

namespace LexicalAnalyzer
{
    public class Token
    {
        private List<string> reservedWords;
        private List<string> operators;
        private List<string> delimiters;

        private string image, kind;
        private int line, column;
        private int? index;

        public string Image { get => image; set => image = value; }
        public string Kind { get => kind; set => kind = value; }
        public int? Index { get => index; set => index = value; }
        public int Line { get => line; set => line = value; }
        public int Column { get => column; set => column = value; }

        public Token()
        {
            reservedWords = new List<string>();
            string url = @"F:\Projetos\DeutschScript\DeutschScript\LexicalAnalyzer\Files\Tokens.txt";
            StreamReader file = new StreamReader(url);

            while (!file.EndOfStream)
                reservedWords.Add(file.ReadLine());

            operators = new List<string>();
            url = @"F:\Projetos\DeutschScript\DeutschScript\LexicalAnalyzer\Files\Tokens.txt";
            file = new StreamReader(url);

            while (!file.EndOfStream)
                operators.Add(file.ReadLine());

            delimiters = new List<string>();
            url = @"F:\Projetos\DeutschScript\DeutschScript\LexicalAnalyzer\Files\Delimiters.txt";
            file = new StreamReader(url);

            while (!file.EndOfStream)
                delimiters.Add(file.ReadLine());
        }

        /// <summary>
        /// Verifica se o token recebido é uma palavra reservada.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool existsReservedWord(string value)
        {
            return reservedWords.Exists(x => x == value);
        }

        /// <summary>
        /// Verifica se o token recebido é um operador.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool existsOperator(string value)
        {
            return operators.Exists(x => x == value);
        }

        /// <summary>
        /// Verifica se o token recebido é um delimitador.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool existsDelimiters(string value)
        {
            return delimiters.Exists(x => x == value);
        }

        public override string ToString()
        {
            return "{Image: " + image + ", Classe: " + kind + ", Indice: " + index + ", Linha: " + line + ", Coluna: " + column + "}";
        }
    }
}
