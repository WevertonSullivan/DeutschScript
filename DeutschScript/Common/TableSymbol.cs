using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class TableSymbol
    {
        public static List<Symbol> symbols = new List<Symbol>();

        /// <summary>
        /// Cria um simbolo ou seta simbolo.
        /// </summary>
        /// <param name="token"></param>
        public static void addSymbol(Token token, string scope)
        {
            Symbol symbol = getSymbolByToken(token, scope);

            if (symbol == null)
            {
                symbol = new Symbol()
                {
                    Image = token.Image,
                    Escope = scope
                };

                symbols.Add(symbol);
            }

            token.Index = symbols.IndexOf(symbol);
        }

        public static void addSymbol(string type, Token token)
        {
            //Symbol symbol = getSymbolByToken(token, scope);

            //if (symbol == null)
            //{
            Symbol symbol = new Symbol()
            {
                Image = token.Image,
                Type = type
            };

            symbols.Add(symbol);
            //}

            //token.Index = symbols.IndexOf(symbol);
        }

        private static Symbol getSymbolByToken(Token token, string scope)
        {
            return symbols.Find(x => x.Image == token.Image && x.Escope == scope);
        }

        public  static string getType(Token token)
        {
            int i = (token.Index == null ? -1 : (int)token.Index);
            return symbols[i].Type;
        }

        public static void setType(Token token, string type)
        {
            symbols[(int)token.Index].Type = type;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            foreach (Symbol s in symbols)
            {
                str.Append("\n" + symbols.IndexOf(s));
                str.Append(" " + s.Image);
                str.Append(" " + s.Escope);
            }

            return str.ToString();
        }
    }
}
