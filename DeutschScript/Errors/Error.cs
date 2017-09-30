using LexicalAnalyzer;

namespace Errors
{
    public class Error
    {
        enum ErrorClass
        {
            SemanticError,
            LexicalError,
            SintaticError
        };

        string msg;
        int line, column;
        Token token;
        private string typeError;

        public string Msg { get => msg; set => msg = value; }
        public int Line { get => line; set => line = value; }
        public int Column { get => column; set => column = value; }
        public Token Token { get => token; set => token = value; }
        public string TypeError { get => typeError; set => typeError = value; }

        public Error(string msg, int line, int column, Token token, string typeError)
        {
            Msg = msg;
            Line = line;
            Column = column;
            Token = token;
            TypeError = typeError;
        }
    }
}
