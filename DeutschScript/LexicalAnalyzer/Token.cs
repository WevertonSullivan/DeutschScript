using System;
using System.Collections.Generic;
using System.IO;

namespace LexicalAnalyzer
{
    public class Token
    {
        private List<string> reservedWord;

        private string image, kind;
        private int index, line, column;

        public string Image { get => image; set => image = value; }
        public string Kind { get => kind; set => kind = value; }
        public int Index { get => index; set => index = value; }
        public int Line { get => line; set => line = value; }
        public int Column { get => column; set => column = value; }

        public Token()
        {
            reservedWord = new List<string>();
            string url = @"F:\Projetos\DeutschScript\DeutschScript\LexicalAnalyzer\Tokens\Tokens.txt";
            StreamReader file = new StreamReader(url);
            
            while(!file.EndOfStream)
                reservedWord.Add(file.ReadLine());
        }

        public bool existsReservedWord(string value)
        {
            return reservedWord.Exists(x => x == value);
        }
    }
}
