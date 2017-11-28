using Common;
using LexicalAnalyzer;
using SyntaticParser;
using System;
using System.Collections.Generic;
using System.IO;

namespace DeutschScript
{
    class Program
    {

        static string script = "";
        static ParserSyntatic parserSyntatic;
        static List<Token> tokens;
        static ParserLexical parserLexical;

        static void Main(string[] args)
        {
            parserLexical = new ParserLexical();
            string url = @"F:\Projetos\DeutschScript\DeutschScript\DeutschScript\Files\DeutschScript.ds";
            StreamReader file = new StreamReader(url);
            tokens = parserLexical.parser(file);

            if (parserLexical.inError())
            {
                Console.WriteLine(parserLexical.Errors);
                return;
            }
            else
            {
                foreach (Token token in tokens)
                {
                    Console.WriteLine(token.Image + "->" + token.Kind);
                }
            }

            Console.WriteLine();


            parserSyntatic = new ParserSyntatic(tokens);
            parserSyntatic.parse();

            if (parserSyntatic.inError())
            {
                Console.Write(parserSyntatic.errorsToString());
                //return;
            }
            else
            {

                Console.WriteLine("Sucesso...");
            }


            Console.Read();
        }

        static void Header()
        {
            Console.Clear();
            //Console.WriteLine("CTRL + S - Salvar arquivo");
            //Console.WriteLine("CTRL + N - Novo arquivo");
            //Console.WriteLine("CTRL + Q - Sair");
            Console.WriteLine("Deutsch Script");
            Console.WriteLine();
        }

        static string FileName()
        {
            Header();
            Console.Write("Nome do Arquivo: ");
            string nome = Console.ReadLine();
            return nome;
        }

        static bool ValidateFile(string fileName)
        {
            bool save = false;
            if (File.Exists(fileName + ".ds"))
            {
                string sub = "";
                while (!sub.Equals("S") && !sub.Equals("s") && !sub.Equals("N") && !sub.Equals("n"))
                {
                    Header();
                    Console.Write("Arquivo " + fileName + ".ds já existe, deseja substituir?[S/N] ");
                    sub = Console.ReadLine();
                    save = (sub.Equals("S") ? true : false);
                }
            }
            return save;
        }

        static void saveScript()
        {
            StreamWriter textValue = new StreamWriter(@"F:\Projetos\DeutschScript\DeutschScript\DeutschScript\Files\DeutschScript.ds", true);
            textValue.Flush();
            textValue.Write(script);
            textValue.Close();
        }
    }
}