using System;
using System.Collections.Generic;

namespace LexicalAnalyzer
{
    public class ParserLexical
    {
        public static List<String> reservedWord;

        private string image, kind;
        private int index, line;

        public string Image { get => image; set => image = value; }
        public string Kind { get => kind; set => kind = value; }
        public int Index { get => index; set => index = value; }
        public int Line { get => line; set => line = value; }

        public ParserLexical()
        {

        }
    }
}
