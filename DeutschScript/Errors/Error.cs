using Common;
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

        string msg, typeError, expectedToken;
        int line, column;
        Token token;

        public string Msg { get => msg; set => msg = value; }
        public string TypeError { get => typeError; set => typeError = value; }
        public string ExpectedToken { get => expectedToken; set => expectedToken = value; }
        public int Line { get => line; set => line = value; }
        public int Column { get => column; set => column = value; }
        public Token Token { get => token; set => token = value; }

        public Error(string msg,string expectedToken, Token token, string typeError)
        {
            Msg = msg;
            Line = token.Line;
            Column = token.Column;
            Token = token;
            TypeError = typeError;
            ExpectedToken = expectedToken;
        }
    }
}
