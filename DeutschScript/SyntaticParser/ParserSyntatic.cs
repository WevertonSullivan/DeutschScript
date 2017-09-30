using LexicalAnalyzer;
using System.Collections.Generic;
using Errors;

namespace SyntaticParser
{
    public class ParserSyntatic
    {
        private List<Token> tokens;
        private Token token;
        private int controlToken;
        private List<Error> errors;

        public ParserSyntatic(List<Token> tokens)
        {
            this.tokens = tokens;
            token = new Token();
            controlToken = 0;
        }

        public void parse()
        {
            _funcs();
        }

        /// <summary>
        /// <funcs>    ::= <func> <funcs> | 
        /// </summary>
        private void _funcs()
        {
            _func();
            _funcs();
            return;
        }

        /// <summary>
        /// <func>    ::= <tipo> id '[' <params> ']' '<<' <comans> '>>'
        /// </summary>
        private void _func()
        {
            _tipo();
            if (token.Kind.ToLower() == "id")
            {
                readToken();
                if (token.Image == "[")
                {
                    readToken();
                    _params();
                    if (token.Image == "]")
                    {
                        readToken();
                        if (token.Image == "<<")
                        {
                            readToken();
                            _comans();
                            if (token.Image == ">>")
                            {
                                readToken();
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <tipo>    ::= 'alle' | 'leer' | 'text' | 'real' | 'logisch'
        /// </summary>
        private void _tipo()
        {
            if (token.Image == "alle")
            {
                readToken();
                return;
            }
            else if (token.Image == "leer")
            {
                readToken();
                return;
            }
            else if (token.Image == "text")
            {
                readToken();
                return;
            }
            else if (token.Image == "real")
            {
                readToken();
                return;
            }
            else if (token.Image == "logisch")
            {
                readToken();
                return;
            }
        }

        /// <summary>
        /// <params>  ::= <param> <params2> | 
        /// </summary>
        private void _params()
        {
            _param();
            _params2();
            return;
        }

        /// <summary>
        /// <params2> ::= ',' <param> <params2> |
        /// </summary>
        private void _params2()
        {
            if (token.Image == ",")
            {
                readToken();
                _param();
                _params2();
                return;
            }
        }

        /// <summary>
        /// <param>   ::= <tipo> id
        /// </summary>
        private void _param()
        {
            _tipo();
            if (token.Kind.ToLower() == "id")
            {
                readToken();
                return;
            }
        }

        /// <summary>
        /// <comans>  ::= <coman> <comans> |
        /// </summary>
        private void _comans()
        {
            _coman();
            _comans();
            return;
        }

        /// <summary>
        /// <coman>   ::= <decl> '.' | <atrib> '.' | <leitura> '.' | <escrita> '.' | <cond> | <laco> | <retorno> '.' | <chamada> '.'
        /// </summary>
        private void _coman()
        {
            _decl();
            if (token.Image == ".")
            {
                readToken();
                return;
            }
            //else
            //{
            _atrib();
            if (token.Image == ".")
            {
                readToken();
                return;
            }
            //else
            //{
            _leitura();
            if (token.Image == ".")
            {
                readToken();
                return;
            }
            //else
            //{
            _escrita();
            if (token.Image == ".")
            {
                readToken();
                return;
            }
            //else
            //{
            _cond();
            _laco();
            _retorno();
            if (token.Image == ".")
            {
                readToken();
                return;
            }
            //else
            //{
            _chamada();
            if (token.Image == ".")
            {
                readToken();
                return;
            }
            //}
            //}
            //}
            //}
            //}
        }

        /// <summary>
        /// <decl>    ::= <tipo> <ids>
        /// </summary>
        private void _decl()
        {
            _tipo();
            _ids();
            return;
        }

        /// <summary>
        /// <ids>     ::= id <ids2>
        /// </summary>
        private void _ids()
        {
            if (token.Kind.ToLower() == "id")
            {
                readToken();
                _ids2();
                return;
            }
        }

        /// <summary>
        /// <ids2>    ::= ',' id <ids2> | 
        /// </summary>
        private void _ids2()
        {
            if (token.Image == ",")
            {
                readToken();
                return;
            }
        }

        /// <summary>
        /// <atrib>   ::= id '<-' <exp>
        /// </summary>
        private void _atrib()
        {
            if (token.Kind.ToLower() == "id")
            {
                readToken();
                if (token.Image == "<-")
                {
                    readToken();
                    _exp();
                    return;
                }
            }
        }

        /// <summary>
        /// <exp>     ::= <operan> <exp2>
        /// </summary>
        private void _exp()
        {
            _operan();
            _exp2();
            return;
        }

        /// <summary>
        /// <exp2>    ::=  | <op> <operan>
        /// </summary>
        private void _exp2()
        {
            //vazio?

            _op();
            _operan();
            return;
        }

        /// <summary>
        /// <operan>  ::= id | cli | clr | cls | cll |  <chamada>
        /// </summary>
        private void _operan()
        {
            if (token.Kind.ToLower() == "id" || token.Kind.ToLower() == "cli" || token.Kind.ToLower() == "cls" || token.Kind.ToLower() == "cll")
            {
                readToken();
                _chamada();
            }
        }

        /// <summary>
        /// <op>      ::= '+' | '-' | '*' | '/' | '&' | '|' | '>' | '<' | '>=' | '<=' | '=' | '<>' | '@'
        /// </summary>
        private void _op()
        {
            if (token.Image == "+" || token.Image == "-" || token.Image == "*" || token.Image == "/" || token.Image == "&"
            || token.Image == "|" || token.Image == ">" || token.Image == "<" || token.Image == ">=" || token.Image == "<="
            || token.Image == "=" || token.Image == "<>" || token.Image == "@")
            {
                readToken();
                return;
            }
        }

        /// <summary>
        /// <leitura> ::= 'lessen' '[' id ']'
        /// </summary>
        private void _leitura()
        {
            if(token.Image =="lessen")
            {
                readToken();
                if(token.Image == "[")
                {
                    readToken();
                    if(token.Kind.ToLower() == "id")
                    {
                        readToken();
                        if(token.Image=="]")
                        {
                            readToken();
                            return;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// <escrita> ::= 'show' '[' <exp> ']'
        /// </summary>
        private void _escrita()
        {
            if(token.Image == "show")
            {
                readToken();
                if(token.Image =="[")
                {
                    readToken();
                    _exp();
                    if(token.Image == "]")
                    {
                        readToken();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// <cond>    ::= 'wenn' '[' <exp> ']' '<<' <comans> '>>' <senao>
        /// </summary>
        private void _cond()
        {
            if(token.Image =="wenn")
            {
                readToken();
                if(token.Image == "[")
                {
                    readToken();
                    _exp();
                    if(token.Image == "]")
                    {
                        readToken();
                        if(token.Image == "<<")
                        {
                            readToken();
                            _comans();
                            if(token.Image == ">>")
                            {
                                readToken();
                                _senao();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// <senao>   ::=  | 'sont' '<<' <comans> '>>'
        /// </summary>
        private void _senao()
        {
            if(token.Image == "sonst")
            {
                readToken();
                if(token.Image =="<<")
                {
                    readToken();
                    _comans();
                    if(token.Image == ">>")
                    {
                        readToken();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// <laco>    ::= 'zum' '[' <exp> ']' '<<' <comans> '>>'
        /// </summary>
        private void _laco()
        {
            if(token.Image == "zum")
            {
                readToken();
                if(token.Image =="[")
                {
                    readToken();
                    _exp();
                    if(token.Image =="]")
                    {
                        readToken();
                        if(token.Image =="<<")
                        {
                            readToken();
                            _comans();
                            if(token.Image ==">>")
                            {
                                readToken();
                                return;
                            }
                        }
                    }

                }                
            }
        }

        /// <summary>
        /// <retorno> ::= 'out' <exp>
        /// </summary>
        private void _retorno()
        {
            if(token.Image == "out")
            {
                readToken();
                _exp();
            }
        }

        /// <summary>
        /// <chamada> ::= id '[' <args> ']'
        /// </summary>
        private void _chamada()
        {
            if (token.Kind == "id")
            {
                readToken();
                if (token.Image == "[")
                {
                    readToken();
                    _args();
                    if (token.Image == "]")
                    {
                        readToken();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// <args>    ::=  | <operan> <args2>
        /// </summary>
        private void _args()
        {
            _operan();
            _args2();
            return;
        }

        /// <summary>
        /// <args2>   ::=  | ',' <operan> <args2>
        /// </summary>
        private void _args2()
        {
            if (token.Image == ",")
            {
                readToken();
                _operan();
                _args2();
                return;
            }
        }

        private void readToken()
        {
            token = tokens[controlToken++];
        }
    }
}
