using ConsoleHotKey;
using LexicalAnalyzer;
using System;
using System.IO;

namespace DeutschScript
{
    class Program
    {

        static string script = "";        

        static void Main(string[] args)
        {
            ParserLexical parserLexical = new ParserLexical();

            //while (true)
            //{
            //    Header();
            //    Console.Write(script);
            //    script += Console.ReadLine();
            //    script += "\n";
            //    saveScript();
            //}

            //Console.WriteLine("Saindo");
            //Console.ReadKey();
            string url = @"F:\Projetos\DeutschScript\DeutschScript\DeutschScript\Files\DeutschScript.ds";
            StreamReader file = new StreamReader(url);
            ParserLexical.parser(file).ToString();
            Console.Read();
        }

        static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (e.Modifiers.ToString() == "Control" && e.Key.ToString() == "S")
            {
                string name = FileName();
                bool save = false;
                do
                {
                    save = ValidateFile(name);
                    if (!save)
                        name = FileName();

                } while (!save);

                System.IO.File.WriteAllText(@"~Files/" + name + ".ds", script);
                Console.WriteLine("Salvar");
            }
            else if (e.Modifiers.ToString() == "Control" && e.Key.ToString() == "Q")
            {
                Console.Write(script);
                Console.ReadKey();
                Environment.Exit(1);
            }
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