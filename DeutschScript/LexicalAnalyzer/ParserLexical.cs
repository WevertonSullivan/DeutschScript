using System;
using System.Collections.Generic;
using System.IO;

namespace LexicalAnalyzer
{
    public class ParserLexical
    {
        public static List<Token> parser(StreamReader source)
        {
            try
            {
                List<Token> tokens = new List<Token>();

                for(int lineIndex = 0; !source.EndOfStream; lineIndex++)
                {
                    lineIndex++;
                    Token token = new Token();
                    string line = source.ReadLine();
                    string tokenValue = "";
                    for(int columnIndex = 0; columnIndex < line.Length; columnIndex++)
                    {
                         tokenValue += line[columnIndex].ToString();
                        if(token.existsReservedWord(tokenValue))
                        {
                            token.Image = tokenValue;
                            token.Kind = "Reserved Word";
                            token.Line = lineIndex + 1;
                            token.Column = columnIndex + 1;
                            tokens.Add(token);

                            token = new Token();
                            tokenValue = "";
                        }
                        else if(tokenValue == " ")
                        {
                            token.Image = tokenValue;
                            token.Kind = "Id";
                            token.Line = lineIndex + 1;
                            token.Column = columnIndex + 1;

                            tokens.Add(token);

                            tokenValue = "";
                        }
                        else if(tokenValue.Contains(@"\talle"))
                        {
                            tokenValue = "";
                        }
                    }
                }

                return tokens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
