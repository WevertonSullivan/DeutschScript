using System;
using System.Collections.Generic;
using System.IO;

namespace LexicalAnalyzer
{
    public class ParserLexical
    {

        /// <summary>
        /// Realiza a verificação léxica de um fonte Deutsch Script.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<Token> parser(StreamReader source)
        {
            try
            {
                List<Token> tokens = new List<Token>();

                ///TODO: Verificar colunas e indices
                for (int lineIndex = 0; !source.EndOfStream; lineIndex++)
                {
                    Token token;
                    string line = source.ReadLine();
                    foreach (string word in line.Split(' '))
                    {
                        if (Class.PR.Exists(x => x == word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "PR",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.DE.Exists(x => x == word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "DE",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.OPA.Exists(x => x == word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "OPA",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.OPR.Exists(x => x == word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "OPR",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.OPL.Exists(x => x == word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "OPL",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.CLI.Equals(word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "CLI",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.CLS.Equals(word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "CLS",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.CLR.Equals(word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "CLR",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.CLL.Equals(word))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "CLL",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (!word.Equals("\talle") && !word.Equals("\tx"))
                        {
                            token = new Token()
                            {
                                Image = word,
                                Kind = "ID",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
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
