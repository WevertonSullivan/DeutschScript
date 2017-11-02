using LexicalAnalyzer;
using System.Collections.Generic;
using System.Text;
using Errors;

namespace SyntaticParser
{
    public class ParserSyntatic
    {
        private List<Token> tokens;
        private Token token;
        private int controlToken;
        private List<Error> errors = new List<Error>();

        public ParserSyntatic(List<Token> tokens)
        {
            this.tokens = tokens;
            controlToken = 0;
            errors = new List<Error>();
        }

        public void parse()
        {
            readToken();
            _funcs();
        }

        /// <summary>
        /// <funcs>    ::= <func> <funcs> | 
        /// </summary>
        private void _funcs()
        {
            if (token.Image == "alle" || token.Image == "leer" || token.Image == "text" || token.Image == "real" || token.Image == "logisch")
            {
                _func();
                _funcs();
            }
        }

        /// <summary>
        /// <func>    ::= <tipo> id '[' <params> ']' '<<' <comans> '>>'
        /// </summary>
        private void _func()
        {
            _tipo();
            if (token.Kind == "ID")
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
        }

        /// <summary>
        /// <tipo>    ::= 'alle' | 'leer' | 'text' | 'real' | 'logisch'
        /// </summary>
        private void _tipo()
        {
            if (token.Image == "alle")
            {
                readToken();
            }
            else if (token.Image == "leer")
            {
                readToken();
            }
            else if (token.Image == "text")
            {
                readToken();
            }
            else if (token.Image == "real")
            {
                readToken();
            }
            else if (token.Image == "logisch")
            {
                readToken();
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {alle, leer, text, real, logisch}.", "alle || leer || text || real || logisch", token, "SyntaticError"));
            }
        }

        /// <summary>
        /// <params>  ::= <param> <params2> | 
        /// </summary>
        private void _params()
        {
            if (token.Image == "alle" || token.Image == "leer" || token.Image == "text" || token.Image == "real" || token.Image == "logisch")
            {
                _param();
                _params2();
            }
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

            }


        }

        /// <summary>
        /// <param>   ::= <tipo> id
        /// </summary>
        private void _param()
        {
            _tipo();
            if (token.Kind == "ID")
            {
                readToken();

            }
        }

        /// <summary>
        /// <comans>  ::= <coman> <comans> |
        /// </summary>
        private void _comans()
        {
            if (token.Image == "alle" || token.Image == "leer" || token.Image == "text" || token.Image == "real" || token.Image == "logisch" ||
                token.Kind == "ID" || token.Image == "lessen" || token.Image == "show" || token.Image == "wenn" || token.Image == "zum" || token.Image == "out")
            {
                _coman();
                _comans();

            }
        }

        /// <summary>
        /// <coman>   ::= <decl> '.' | <atrib> '.' | <leitura> '.' | <escrita> '.' | <cond> | <laco> | <retorno> '.' | <chamada> '.'
        /// </summary>
        private void _coman()
        {
            if (token.Image == "alle" || token.Image == "leer" || token.Image == "text" || token.Image == "real" || token.Image == "logisch")
            {
                _decl();
                if (token.Image == ".")
                {
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
                    _atrib();
                    if (token.Image == ".")
                    {
                        readToken();

                    }
                    else
                    {
                        errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                    }
                }
                else if (lookaHead().Image == "[")
                {
                    _chamada();
                    if (token.Image == ".")
                    {
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
                _leitura();
                if (token.Image == ".")
                {
                    readToken();
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                }
            }
            else if (token.Image == "show")
            {
                _escrita();
                if (token.Image == ".")
                {
                    readToken();
                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {.}.", ".", token, "SyntaticError"));
                }
            }
            else if (token.Image == "wenn")
            {
                _cond();
            }
            else if (token.Image == "zum")
            {
                _laco();
            }
            else if (token.Image == "out")
            {
                _retorno();
                if (token.Image == ".")
                {
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
        }

        /// <summary>
        /// <decl>    ::= <tipo> <ids>
        /// </summary>
        private void _decl()
        {
            _tipo();
            _ids();

        }

        /// <summary>
        /// <ids>     ::= id <ids2>
        /// </summary>
        private void _ids()
        {
            if (token.Kind == "ID")
            {
                readToken();
                _ids2();

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
                if (token.Kind == "ID")
                {
                    readToken();
                    _ids2();

                }
                else
                {
                    errors.Add(new Error("Erro ao validar token, esperado: {ID}.", "ID", token, "SyntaticError"));
                }
            }
        }

        /// <summary>
        /// <atrib>   ::= id '<-' <exp>
        /// </summary>
        private void _atrib()
        {
            if (token.Kind == "ID")
            {
                readToken();
                if (token.Image == "<-")
                {
                    readToken();
                    _exp();

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
        }

        /// <summary>
        /// <exp>     ::= <operan> <exp2>
        /// </summary>
        private void _exp()
        {
            _operan();
            _exp2();


        }

        /// <summary>
        /// <exp2>    ::=  | <op> <operan>
        /// </summary>
        private void _exp2()
        {
            //vazio?
            if (token.Image == "+" || token.Image == "-" || token.Image == "*" || token.Image == "/" || token.Image == "&"
             || token.Image == "|" || token.Image == ">" || token.Image == "<" || token.Image == ">=" || token.Image == "<="
             || token.Image == "=" || token.Image == "<>" || token.Image == "@")
            {
                _op();
                _operan();

            }
        }

        /// <summary>
        /// <operan>  ::= id | cli | clr | cls | cll |  <chamada>
        /// </summary>
        private void _operan()
        {
            if (token.Kind == "ID")
            {
                if (lookaHead().Image == "[")
                {
                    _chamada();
                }
                else
                {
                    readToken();
                }
            }
            else if (token.Kind == "CLI" || token.Kind == "CLS" || token.Kind == "CLL" || token.Kind == "CLR")
            {
                readToken();
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {ID, CLI, CLS ou CLL}.", "{ID, CLI, CLS, CLL}", token, "SyntaticError"));
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

            }
        }

        /// <summary>
        /// <leitura> ::= 'lessen' '[' id ']'
        /// </summary>
        private void _leitura()
        {
            if (token.Image == "lessen")
            {
                readToken();
                if (token.Image == "[")
                {
                    readToken();
                    if (token.Kind == "ID")
                    {
                        readToken();
                        if (token.Image == "]")
                        {
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
        }

        /// <summary>
        /// <escrita> ::= 'show' '[' <exp> ']'
        /// </summary>
        private void _escrita()
        {
            if (token.Image == "show")
            {
                readToken();
                if (token.Image == "[")
                {
                    readToken();
                    _exp();
                    if (token.Image == "]")
                    {
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
        }

        /// <summary>
        /// <cond>    ::= 'wenn' '[' <exp> ']' '<<' <comans> '>>' <senao>
        /// </summary>
        private void _cond()
        {
            if (token.Image == "wenn")
            {
                readToken();
                if (token.Image == "[")
                {
                    readToken();
                    _exp();
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
                                _senao();
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
        }

        /// <summary>
        /// <senao>   ::=  | 'sont' '<<' <comans> '>>'
        /// </summary>
        private void _senao()
        {
            if (token.Image == "sonst")
            {
                readToken();
                if (token.Image == "<<")
                {
                    readToken();
                    _comans();
                    if (token.Image == ">>")
                    {
                        readToken();

                    }
                    else { errors.Add(new Error("Erro ao validar token, esperado: {>>}", ">>", token, "SyntaticError")); }
                }
                else { errors.Add(new Error("Erro ao validar token, esperado: {<<}", "<<", token, "SyntaticError")); }
            }
        }

        /// <summary>
        /// <laco>    ::= 'zum' '[' <exp> ']' '<<' <comans> '>>'
        /// </summary>
        private void _laco()
        {
            if (token.Image == "zum")
            {
                readToken();
                if (token.Image == "[")
                {
                    readToken();
                    _exp();
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
        }

        /// <summary>
        /// <retorno> ::= 'out' <exp>
        /// </summary>
        private void _retorno()
        {
            if (token.Image == "out")
            {
                readToken();
                _exp();
            }
            else
            {
                errors.Add(new Error("Erro ao validar token, esperado: {out}", "out", token, "SyntaticError"));
            }
        }

        /// <summary>
        /// <chamada> ::= id '[' <args> ']'
        /// </summary>
        private void _chamada()
        {
            if (token.Kind == "ID")
            {
                readToken();
                if (token.Image == "[")
                {
                    readToken();
                    _args();
                    if (token.Image == "]")
                    {
                        readToken();

                    }
                    else { errors.Add(new Error("Erro ao validar token, esperado: {]}", "]", token, "SyntaticError")); }
                }
                else { errors.Add(new Error("Erro ao validar token, esperado: {[}", "[", token, "SyntaticError")); }
            }
            else { errors.Add(new Error("Erro ao validar token, esperado: {ID}", "ID", token, "SyntaticError")); }
        }

        /// <summary>
        /// <args>    ::=  | <operan> <args2>
        /// </summary>
        private void _args()
        {
            if (token.Kind == "ID" || token.Kind == "CLI" || token.Kind == "CLS" || token.Kind == "CLL" || token.Kind == "CLR")
            {
                _operan();
                _args2();

            }
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

            }
        }

        private void readToken()
        {
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

            foreach(Error error in errors)
            {
                text.AppendLine(error.Msg);
            }

            return text.ToString();
        }
    }
}
