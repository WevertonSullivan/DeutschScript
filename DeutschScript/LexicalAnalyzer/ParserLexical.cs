using Common;
using System;
using System.Collections.Generic;
using System.IO;

namespace LexicalAnalyzer
{
    public class ParserLexical
    {

        List<string> errors = new List<string>();

        public List<string> Errors { get => errors; set => errors = value; }

        int testerInt;
        float testerFloat;
        bool testerBool;

        /// <summary>
        /// Realiza a verificação léxica de um fonte Deutsch Script.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public List<Token> parser(StreamReader source)
        {
            try
            {
                List<Token> tokens = new List<Token>();
                Token token;

                ///TODO: Verificar colunas e indices
                for (int lineIndex = 0; !source.EndOfStream; lineIndex++)
                {
                    string line = source.ReadLine();
                    string text = "";
                    string lineController = "";
                    Stack<string> textToken = new Stack<string>();

                    foreach (string word in line.Split(' '))
                    {
                        string wordDealt = word.Replace("\talle", "").Replace("\tx", "").Replace("\t", "").ToString();

                        if (line.Contains("#") && line != lineController)
                        {
                            lineController = line;
                            for (int i = 0; i < line.Length; i++)
                            {
                                if (line[i] == '#' && textToken.Count == 0)
                                {
                                    textToken.Push(line[i].ToString());
                                }
                                else if (line[i] != '#' && textToken.Count > 0)
                                {
                                    text += line[i].ToString();
                                }
                                else if (line[i] == '#' && textToken.Count > 0)
                                {
                                    textToken.Peek();

                                    token = new Token()
                                    {
                                        Image = text,
                                        Kind = "CLS",
                                        Line = lineIndex + 1,
                                        Column = 0,
                                        Index = -1
                                    };
                                    tokens.Add(token);

                                    text = "";
                                }
                            }
                        }
                        else if (Class.PR.Exists(x => x == wordDealt))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "PR",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.DE.Exists(x => x == wordDealt))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "DE",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.OPA.Exists(x => x == wordDealt))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "OPA",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.OPR.Exists(x => x == wordDealt))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "OPR",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.OPL.Exists(x => x == wordDealt))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "OPL",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (int.TryParse(wordDealt, out testerInt))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "CLI",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (Class.CLS.Equals(wordDealt))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "CLS",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (float.TryParse(wordDealt, out testerFloat))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "CLR",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (bool.TryParse(wordDealt, out testerBool))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "CLL",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (!wordDealt.Equals("\talle") && !wordDealt.Equals("\tx") && !wordDealt.Contains("#") && !String.IsNullOrEmpty(wordDealt))
                        {
                            token = new Token()
                            {
                                Image = wordDealt,
                                Kind = "ID",
                                Line = lineIndex + 1,
                                Column = 0,
                                Index = -1
                            };
                            tokens.Add(token);
                        }
                        else if (!string.IsNullOrEmpty(word))
                        {
                            errors.Add("Lexema " + word + "não classificado.");
                        }
                    }

                }

                token = new Token()
                {
                    Image = "~",
                    Kind = "~",
                    Line = 0,
                    Column = 0,
                    Index = -1
                };
                tokens.Add(token);

                return tokens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool inError()
        {
            return (errors.Count > 0 ? true : false);
        }
    }
}
