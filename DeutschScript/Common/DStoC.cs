using System.Collections.Generic;

namespace Common
{
    public class DStoC
    {
        private static List<DS_C> dsc = new List<DS_C>()
        {
            new DS_C("$", "//"), new DS_C("alle", "int"), new DS_C("text", "string"), new DS_C("real", "real"),
            new DS_C("logisch", "bool"), new DS_C("leer", "void"), new DS_C("haupt", "main"), new DS_C("show","cout"),
            new DS_C("lessen", "cin"), new DS_C("wenn", "if"), new DS_C("sonst", "else"), new DS_C("zum", "for"), new DS_C("out", "return"),
            new DS_C("[", "("), new DS_C("]", ")"), new DS_C("<<", "{"), new DS_C(">>", "}"), new DS_C(".", ";"), new DS_C("#", "\""),
            new DS_C("+", "+"), new DS_C("-", "-"), new DS_C("*", "*"), new DS_C("/", "/"), new DS_C("<-", "="),new DS_C(">", ">"), new DS_C("<", "<"),
            new DS_C(">=",">="), new DS_C("<=", "<="), new DS_C("==", "=="), new DS_C("!=", "!="), new DS_C("&&", "&&"), new DS_C("||", "||")
        };

        public static string getCSymbol(string DSSymbol)
        {
            return dsc.Find(x => x.DS == DSSymbol).C;
        }

    }
}
