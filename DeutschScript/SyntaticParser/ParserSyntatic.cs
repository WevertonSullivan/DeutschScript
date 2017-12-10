using LexicalAnalyzer;
using System.Collections.Generic;
using System.Text;
using Errors;
using Common;
using System;

namespace SyntaticParser
{
    public class ParserSyntatic
    {
        private List<Token> tokens;
        private Token token;
        private int controlToken;
        private List<Error> errors = new List<Error>();
        private Node tree;
        private string scope;

        public ParserSyntatic(List<Token> tokens)
        {
            this.tokens = tokens;
            controlToken = 0;
            errors = new List<Error>();
        }

        public void parse()
        {
            readToken();
            tree = _funcs();

            Console.Write(new Node().printTree(tree));
        }

        /// <summary>
        /// <funcs>    ::= <func> <funcs> | 
        /// </summary>
        private Node _funcs()
        {
            Node node = new Node("funcs");
            if (token.Image == "alle" || token.Image == "leer" || token.Image == "text" || token.Image == "real" || token.Image == "logisch")
            {                
                node.addIssue(_func());
                node.addIssue(_funcs());
            }

            return node;
        }

        /// <summary>
        /// <func>    ::= <tipo> id '[' <params> ']' '<<' <comans> '>>'
        /// </summary>
        private Node _func()
        {
            Node node = new Node("func");
            node.addIssue(_tipo());

            if (token.Kind == "ID")
            {
                scope = token.Image;
                node.addIssue(new Node(token));
                readToken();

                if (token.Image == "[")
                {
                    node.addIssue(new Node(token));
                    readToken();
                    node.addIssue(_params());

                    if (token.Image == "]")
                    {
                        node.addIssue(new Node(token));
                        readToken();

                        if (token.Image == "<<")
                        {
                            node.addIssue(new Node(token));
                            readToken();
                            node.addIssue(_comans());

                            if (token.Image == ">>")
                            {
                                node.addIssue(new Node(token));
                                readToken();
                            }
                            else
                            {
                                errors.Add(new Error("Erro ao validar token, esperado: {>>}.", ">>", token, "SyntaticError"));
                            }
                        }
                        else
                        {
                            errors.Add(new Error("Erro ao validar token, esperado: {<<}.", "<<", token, "SyntaticError"));
                        }
                    }
                    else
                    {
                        errors.Add(new Error("Erro ao validar token, esperado: ']'.", "]", token, "SyntaticError"));
                    }
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: '['.", "[", token, "SyntaticError"));
                }
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {id}.", "Classe{ID}", token, "SyntaticError"));
            }

            return node;
        }

        /// <summary>
        /// <tipo>    ::= 'alle' | 'leer' | 'text' | 'real' | 'logisch'
        /// </summary>
        private Node _tipo()
        {
            Node node = new Node("tipo");
            if (token.Image == "alle")
            {
                node.addIssue(new Node(token));
                readToken();
            }
            else if (token.Image == "leer")
            {
                node.addIssue(new Node(token));
                readToken();
            }
            else if (token.Image == "text")
            {
                node.addIssue(new Node(token));
                readToken();
            }
            else if (token.Image == "real")
            {
                node.addIssue(new Node(token));
                readToken();
            }
            else if (token.Image == "logisch")
            {
                node.addIssue(new Node(token));
                readToken();
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {alle, leer, text, real, logisch}.", "alle || leer || text || real || logisch", token, "SyntaticError"));
            }

            return node;
        }

        /// <summary>
        /// <params>  ::= <param> <params2> | 
        /// </summary>
        private Node _params()
        {
            Node node = new Node("params");
            if (token.Image == "alle" || token.Image == "leer" || token.Image == "text" || token.Image == "real" || token.Image == "logisch")
            {
                //node.addIssue(new Node(token, "params"));
                node.addIssue(_param());
                node.addIssue(_params2());
            }
            return node;
        }

        /// <summary>
        /// <params2> ::= ',' <param> <params2> |
        /// </summary>
        private Node _params2()
        {
            Node node = new Node("params2");

            if (token.Image == ",")
            {
                node.addIssue(new Node(token));
                readToken();
                node.addIssue(_param());
                node.addIssue(_params2());
            }

            return node;
        }

        /// <summary>
        /// <param>   ::= <tipo> id
        /// </summary>
        private Node _param()
        {
            Node node = new Node("param");
            node.addIssue(_tipo());

            if (token.Kind == "ID")
            {
                node.addIssue(new Node(token));
                readToken();
            }
            return node;
        }

        /// <summary>
        /// <comans>  ::= <coman> <comans> |
        /// </summary>
        private Node _comans()
        {
            Node node = new Node("comans");
            if (token.Image == "alle" || token.Image == "leer" || token.Image == "text" || token.Image == "real" || token.Image == "logisch" ||
                token.Kind == "ID" || token.Image == "lessen" || token.Image == "show" || token.Image == "wenn" || token.Image == "zum" || token.Image == "out")
            {
                //node.addIssue(new Node(token, "comans"));
                node.addIssue(_coman());
                node.addIssue(_comans());
           }

            return node;
        }

        /// <summary>
        /// <coman>   ::= <decl> '.' | <atrib> '.' | <leitura> '.' | <escrita> '.' | <cond> | <laco> | <retorno> '.' | <chamada> '.'
        /// </summary>
        private Node _coman()
        {
            Node node = new Node("coman");
            if (token.Image == "alle" || token.Image == "leer" || token.Image == "text" || token.Image == "real" || token.Image == "logisch")
            {
                node.addIssue(_decl());
                if (token.Image == ".")
                {
                    node.addIssue(new Node(token));
                    readToken();
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                }
            }
            else if (token.Kind == "ID")
            {
                if (lookaHead().Image == "<-")
                {
                    node.addIssue(_atrib());
                    if (token.Image == ".")
                    {
                        node.addIssue(new Node(token));
                        readToken();
                    }
                    else
                    {
                        errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                    }
                }
                else if (lookaHead().Image == "[")
                {
                    node.addIssue(_chamada());
                    if (token.Image == ".")
                    {
                        node.addIssue(new Node(token));
                        readToken();
                    }
                    else
                    {
                        errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                    }
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {<- ou [}.", "{<-, [}", token, "SyntaticError"));
                }
            }
            else if (token.Image == "lessen")
            {
                //node.addIssue(new Node(token, "coman"));
                node.addIssue(_leitura());

                if (token.Image == ".")
                {
                    node.addIssue(new Node(token));
                    readToken();
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                }
            }
            else if (token.Image == "show")
            {
                //node.addIssue(new Node(token, "coman"));
                node.addIssue(_escrita());

                if (token.Image == ".")
                {
                    node.addIssue(new Node(token));
                    readToken();
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                }
            }
            else if (token.Image == "wenn")
            {
                //node.addIssue(new Node(token, "coman"));
                node.addIssue(_cond());
            }
            else if (token.Image == "zum")
            {
                //node.addIssue(new Node(token, "coman"));
                node.addIssue(_laco());
            }
            else if (token.Image == "out")
            {
                //node.addIssue(new Node(token, "coman"));
                node.addIssue(_retorno());

                if (token.Image == ".")
                {
                    node.addIssue(new Node(token));
                    readToken();
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                }
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {alle, leer, text, real, logisch, ID, lessen, show, wenn, zum ou out}.", "{alle, leer, text, real, logisch, ID, lessen, show, wenn, zum ou out}", token, "SyntaticError"));

            }
            return node;
        }

        /// <summary>
        /// <decl>    ::= <tipo> <ids>
        /// </summary>
        private Node _decl()
        {
            Node node = new Node("decl");
            node.addIssue(_tipo());
            node.addIssue(_ids());
            return node;
        }

        /// <summary>
        /// <ids>     ::= id <ids2>
        /// </summary>
        private Node _ids()
        {
            Node node = new Node("ids");
            if (token.Kind == "ID")
            {
                node.addIssue(new Node(token));
                readToken();
                node.addIssue(_ids2());
            }

            return node;
        }

        /// <summary>
        /// <ids2>    ::= ',' id <ids2> | 
        /// </summary>
        private Node _ids2()
        {
            Node node = new Node("ids2");

            if (token.Image == ",")
            {
                node.addIssue(new Node(token));
                readToken();

                if (token.Kind == "ID")
                {
                    node.addIssue(new Node(token));
                    readToken();
                    node.addIssue(_ids2());
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {ID}.", "ID", token, "SyntaticError"));
                }
            }

            return node;
        }

        /// <summary>
        /// <atrib>   ::= id '<-' <exp>
        /// </summary>
        private Node _atrib()
        {
            Node node = new Node("atrib");
            if (token.Kind == "ID")
            {
                node.addIssue(new Node(token));
                readToken();

                if (token.Image == "<-")
                {
                    node.addIssue(new Node(token));
                    readToken();
                    node.addIssue(_exp());
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {<-}.", "<-", token, "SyntaticError"));
                }
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {ID}.", "ID", token, "SyntaticError"));
            }

            return node;
        }

        /// <summary>
        /// <exp>     ::= <operan> <exp2>
        /// </summary>
        private Node _exp()
        {
            Node node = new Node("exp");
            node.addIssue(_operan());
            node.addIssue(_exp2());
            return node;
        }

        /// <summary>
        /// <exp2>    ::=  | <op> <operan>
        /// </summary>
        private Node _exp2()
        {

            Node node = new Node("exp2");

            if (token.Image == "+" || token.Image == "-" || token.Image == "*" || token.Image == "/" || token.Image == "&"
             || token.Image == "|" || token.Image == ">" || token.Image == "<" || token.Image == ">=" || token.Image == "<="
             || token.Image == "=" || token.Image == "<>" || token.Image == "@")
            {
                node.addIssue(_op());
                node.addIssue(_operan());
            }
            return node;
        }

        /// <summary>
        /// <operan>  ::= id | cli | clr | cls | cll |  <chamada>
        /// </summary>
        private Node _operan()
        {
            Node node = new Node("operan");

            if (token.Kind == "ID")
            {
                if (lookaHead().Image == "[")
                {
                    node.addIssue(_chamada());
                }
                else
                {
                    node.addIssue(new Node(token));
                    readToken();
                }
            }
            else if (token.Kind == "CLI" || token.Kind == "CLS" || token.Kind == "CLL" || token.Kind == "CLR")
            {
                node.addIssue(new Node(token));
                readToken();
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {ID, CLI, CLS ou CLL}.", "{ID, CLI, CLS, CLL}", token, "SyntaticError"));
            }
            return node;
        }

        /// <summary>
        /// <op>      ::= '+' | '-' | '*' | '/' | '&' | '|' | '>' | '<' | '>=' | '<=' | '=' | '<>' | '@'
        /// </summary>
        private Node _op()
        {
            Node node = new Node("op");

            if (token.Image == "+" || token.Image == "-" || token.Image == "*" || token.Image == "/" || token.Image == "&"
            || token.Image == "|" || token.Image == ">" || token.Image == "<" || token.Image == ">=" || token.Image == "<="
            || token.Image == "=" || token.Image == "<>" || token.Image == "@")
            {
                node.addIssue(new Node(token));
                readToken();

            }
            return node;
        }

        /// <summary>
        /// <leitura> ::= 'lessen' '[' id ']'
        /// </summary>
        private Node _leitura()
        {
            Node node = new Node("leitura");
            if (token.Image == "lessen")
            {
                node.addIssue(new Node(token));
                readToken();

                if (token.Image == "[")
                {
                    node.addIssue(new Node(token));
                    readToken();

                    if (token.Kind == "ID")
                    {
                        node.addIssue(new Node(token));
                        readToken();

                        if (token.Image == "]")
                        {
                            node.addIssue(new Node(token));
                            readToken();
                        }
                        else
                        {
                            errors.Add(new Error("Erro ao validar token, esperado: {]}.", "]", token, "SyntaticError"));
                        }
                    }
                    else
                    {
                        errors.Add(new Error("Erro ao validar token, esperado: {ID}", "ID", token, "SyntaticError"));
                    }
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {[}", "]", token, "SyntaticError"));
                }
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {lessen}", "lessen", token, "SyntaticError"));
            }

            return node;
        }

        /// <summary>
        /// <escrita> ::= 'show' '[' <exp> ']'
        /// </summary>
        private Node _escrita()
        {
            Node node = new Node("escrita");

            if (token.Image == "show")
            {
                node.addIssue(new Node(token));
                readToken();

                if (token.Image == "[")
                {
                    node.addIssue(new Node(token));
                    readToken();
                    node.addIssue(_exp());

                    if (token.Image == "]")
                    {
                        node.addIssue(new Node(token));
                        readToken();
                    }
                    else
                    {
                        errors.Add(new Error("Erro ao validar token, esperado: {]}", "]", token, "SyntaticError"));
                    }
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {[}", "[", token, "SyntaticError"));
                }
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {show}", "show", token, "SyntaticError"));
            }
            return node;
        }

        /// <summary>
        /// <cond>    ::= 'wenn' '[' <exp> ']' '<<' <comans> '>>' <senao>
        /// </summary>
        private Node _cond()
        {
            Node node = new Node("cond");
            if (token.Image == "wenn")
            {
                node.addIssue(new Node(token));
                readToken();

                if (token.Image == "[")
                {
                    node.addIssue(new Node(token));
                    readToken();
                    node.addIssue(_exp());

                    if (token.Image == "]")
                    {
                        node.addIssue(new Node(token));
                        readToken();

                        if (token.Image == "<<")
                        {
                            node.addIssue(new Node(token));
                            readToken();
                            node.addIssue(_comans());

                            if (token.Image == ">>")
                            {
                                node.addIssue(new Node(token));
                                readToken();
                                node.addIssue(_senao());
                            }
                            else { errors.Add(new Error("Erro ao validar token, esperado: {>>}", ">>", token, "SyntaticError")); }
                        }
                        else { errors.Add(new Error("Erro ao validar token, esperado: {<<}", "<<", token, "SyntaticError")); }
                    }
                    else { errors.Add(new Error("Erro ao validar token, esperado: {]}", "]", token, "SyntaticError")); }
                }
                else { errors.Add(new Error("Erro ao validar token, esperado: {[}", "[", token, "SyntaticError")); }
            }
            else { errors.Add(new Error("Erro ao validar token, esperado: {wenn}", "wenn", token, "SyntaticError")); }

            return node;
        }

        /// <summary>
        /// <senao>   ::=  | 'sont' '<<' <comans> '>>'
        /// </summary>
        private Node _senao()
        {
            Node node = new Node("senao");
            if (token.Image == "sonst")
            {
                node.addIssue(new Node(token));
                readToken();

                if (token.Image == "<<")
                {
                    node.addIssue(new Node(token));
                    readToken();
                    node.addIssue(_comans());

                    if (token.Image == ">>")
                    {
                        node.addIssue(new Node(token));
                        readToken();
                    }
                    else { errors.Add(new Error("Erro ao validar token, esperado: {>>}", ">>", token, "SyntaticError")); }
                }
                else { errors.Add(new Error("Erro ao validar token, esperado: {<<}", "<<", token, "SyntaticError")); }
            }
            return node;
        }

        /// <summary>
        /// <laco>    ::= 'zum' '[' <exp> ']' '<<' <comans> '>>'
        /// </summary>
        private Node _laco()
        {
            Node node = new Node("laco");
            if (token.Image == "zum")
            {
                node.addIssue(new Node(token));
                readToken();

                if (token.Image == "[")
                {
                    node.addIssue(new Node(token));
                    readToken();
                    node.addIssue(_exp());

                    if (token.Image == "]")
                    {
                        node.addIssue(new Node(token));
                        readToken();

                        if (token.Image == "<<")
                        {
                            node.addIssue(new Node(token));
                            readToken();
                            node.addIssue(_comans());

                            if (token.Image == ">>")
                            {
                                node.addIssue(new Node(token));
                                readToken();
                            }
                            else { errors.Add(new Error("Erro ao validar token, esperado: {>>}", ">>", token, "SyntaticError")); }
                        }
                        else { errors.Add(new Error("Erro ao validar token, esperado: {<<}", "<<", token, "SyntaticError")); }
                    }
                    else { errors.Add(new Error("Erro ao validar token, esperado: {]}", "]", token, "SyntaticError")); }
                }
                else { errors.Add(new Error("Erro ao validar token, esperado: {[}", "[", token, "SyntaticError")); }
            }
            else { errors.Add(new Error("Erro ao validar token, esperado: {zum}", "zum", token, "SyntaticError")); }

            return node;
        }

        /// <summary>
        /// <retorno> ::= 'out' <exp>
        /// </summary>
        private Node _retorno()
        {
            Node node = new Node("retorno");
            if (token.Image == "out")
            {
                node.addIssue(new Node(token));
                readToken();
                node.addIssue(_exp());
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {out}", "out", token, "SyntaticError"));
            }
            return node;
        }

        /// <summary>
        /// <chamada> ::= id '[' <args> ']'
        /// </summary>
        private Node _chamada()
        {
            Node node = new Node("chamada");
            if (token.Kind == "ID")
            {
                node.addIssue(new Node(token));
                readToken();

                if (token.Image == "[")
                {
                    node.addIssue(new Node(token));
                    readToken();
                    node.addIssue(_args());

                    if (token.Image == "]")
                    {
                        node.addIssue(new Node(token));
                        readToken();
                    }
                    else { errors.Add(new Error("Erro ao validar token, esperado: {]}", "]", token, "SyntaticError")); }
                }
                else { errors.Add(new Error("Erro ao validar token, esperado: {[}", "[", token, "SyntaticError")); }
            }
            else { errors.Add(new Error("Erro ao validar token, esperado: {ID}", "ID", token, "SyntaticError")); }
            return node;
        }

        /// <summary>
        /// <args>    ::=  | <operan> <args2>
        /// </summary>
        private Node _args()
        {
            Node node = new Node("args");

            if (token.Kind == "ID" || token.Kind == "CLI" || token.Kind == "CLS" || token.Kind == "CLL" || token.Kind == "CLR")
            {
                node.addIssue(_operan());
                node.addIssue(_args2());
            }

            return node;
        }

        /// <summary>
        /// <args2>   ::=  | ',' <operan> <args2>
        /// </summary>
        private Node _args2()
        {
            Node node = new Node("args2");
            if (token.Image == ",")
            {
                node.addIssue(new Node(token));
                readToken();
                node.addIssue(_operan());
                node.addIssue(_args2());
            }
            return node;
        }

        private void readToken()
        {
            if(token != null && token.Kind == "ID")
            {
                TableSymbol.addSymbol(token, scope);
            }
                
            token = tokens[controlToken++];
        }

        private Token lookaHead()
        {
            return tokens[controlToken];
        }

        public bool inError()
        {
            return (errors.Count > 0 ? true : false);
        }

        public string errorsToString()
        {
            StringBuilder text = new StringBuilder();

            foreach (Error error in errors)
            {
                text.AppendLine(error.Msg);
            }

            return text.ToString();
        }
    }
}