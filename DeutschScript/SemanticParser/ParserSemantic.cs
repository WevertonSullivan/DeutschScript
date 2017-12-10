using Common;
using System.Collections.Generic;

namespace SemanticParser
{
    public class ParserSemantic
    {
        private List<Error> error = new List<Error>();

        public void parse(Node tree)
        {

        }

        /// <summary>
        /// <func>    ::= <func> <funcs> | $
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _funcs(Node node)
        {
            if (node.getIssues().Count > 0)
            {
                _func(node.getIssue(0));
                _funcs(node.getIssue(1));
            }

            return null;
        }

        /// <summary>
        /// <func>    ::= <tipo> id '[' <params> ']' '<<' <comans> '>>'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _func(Node node)
        {
            string type = (string)_tipo(node.getIssue(0));
            Token id = node.getIssue(1).Token;

            if (TableSymbol.getType(id) != null)
            {
                //TODO:ADD ERRO
            }
            else
            {
                TableSymbol.setType(id, type);
                _params(node.getIssue(3));
                _comans(node.getIssue(6));
            }

            return null;
        }

        /// <summary>
        /// <params>  ::= <param> <params2> | $
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _params(Node node)
        {
            _param(node.getIssue(0));
            _params2(node.getIssue(1));
            return null;
        }

        /// <summary>
        /// <params2> ::= ',' <param> <params2> |
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _params2(Node node)
        {
            if(node != null)
            {
                _param(node.getIssue(1));
                _params2(node.getIssue(2));
            }
            return null;
        }

        /// <summary>
        /// <param>   ::= <tipo> id
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _param(Node node)
        {
            string type = (string)_tipo(node.getIssue(0));
            Token id = node.getIssue(1).Token;

            if (TableSymbol.symbols.Exists(x => x.Image == id.Image))
            {
                //TODO:ADD ERRO
            }
            else
            {
                TableSymbol.addSymbol(type, node.Token);
            }
            return null;
        }

        /// <summary>
        /// <tipo>    ::= 'alle' | 'leer' | 'text' | 'real' | 'logisch'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _tipo(Node node)
        {
            return node.getIssue(0).Token.Image;
        }

        /// <summary>
        /// <comans>  ::= <decl> '.' | <atrib> '.' | <leitura> '.' | <escrita> '.' | <cond> | <laco> | <retorno> '.' | <chamada> '.'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _comans(Node node)
        {
            if (node.getIssue(0).Type == "decl")
            {
                _decl(node.getIssue(0));
            }
            else if (node.getIssue(0).Type == "atrib")
            {
                _atrib(node.getIssue(0));
            }
            else if (node.getIssue(0).Type == "leitura")
            {
                _leitura(node.getIssue(0));
            }
            else if (node.getIssue(0).Type == "escrita")
            {
                _escrita(node.getIssue(0));
            }
            else if (node.getIssue(0).Type == "cond")
            {
                _cond(node.getIssue(0));
            }
            else if (node.getIssue(0).Type == "laco")
            {
                _laco(node.getIssue(0));
            }
            else if (node.getIssue(0).Type == "retorno")
            {
                _retorno(node.getIssue(0));
            }
            else if (node.getIssue(0).Type == "chamada")
            {
                _chamada(node.getIssue(0));
            }

            return null;
        }

        /// <summary>
        /// <decl>    ::= <tipo> <ids>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _decl(Node node)
        {
            string type = (string)_tipo(node.getIssue(0));

            List<Token> ids = (List<Token>)_ids(node.getIssue(1));

            return null;
        }

        /// <summary>
        /// <ids>     ::= id <ids2>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _ids(Node node)
        {
            Token id = node.getIssue(0).Token;
            if (TableSymbol.symbols.Exists(x => x.Image == id.Image))
            {
                //TODO: ADD ERRO
            }
            else
            {
                _ids2(node.getIssue(1));
            }

            return null;
        }

        /// <summary>
        /// <ids2>    ::= ',' id <ids2> | $
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _ids2(Node node)
        {
            Token id = node.getIssue(1).Token;
            _ids2(node.getIssue(2));

            return id;
        }

        /// <summary>
        /// <atrib>   ::= id '<-' <exp>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _atrib(Node node)
        {
            Token id = node.getIssue(0).Token;
            _exp(node.getIssue(2));

            return id;
        }

        /// <summary>
        /// <exp>     ::= <operan> <exp2>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _exp(Node node)
        {
            _operan(node.getIssue(0));
            _exp2(node.getIssue(1));

            return null;
        }

        /// <summary>
        /// <exp2>    ::= $ | <op> <operan>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _exp2(Node node)
        {
            if(node.getIssue(0)!= null)
            {
                _op(node.getIssue(0));
                _operan(node.getIssue(1));
            }

            return null;
        }

        /// <summary>
        /// <operan>  ::= id | cli | clr | cls | cll |  <chamada>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _operan(Node node)
        {
            if(node.getIssue(0).Token.Kind == "ID" || node.getIssue(0).Token.Kind == "CLI" || node.getIssue(0).Token.Kind == "CLR" ||
               node.getIssue(0).Token.Kind == "CLS" || node.getIssue(0).Token.Kind == "CLL")
            {
                return node.getIssue(0).Token.Image;
            }
            else
            {
                _chamada(node.getIssue(0));
            }
            return null;
        }

        /// <summary>
        /// <op>      ::= '+' | '-' | '*' | '/' | '&' | '|' | '>' | '<' | '>=' | '<=' | '=' | '<>' | '@'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _op(Node node)
        {
            return node.getIssue(0).Token.Image;
        }

        /// <summary>
        /// <leitura> ::= 'lessen' '[' id ']'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _leitura(Node node)
        {
            return node.getIssue(2).Token;
        }

        /// <summary>
        /// <escrita> ::= 'show' '[' <exp> ']'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _escrita(Node node)
        {
            _exp(node.getIssue(2));

            return null;
        }

        /// <summary>
        /// <cond>    ::= 'wenn' '[' <exp> ']' '<<' <comans> '>>' <senao>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _cond(Node node)
        {
            _exp(node.getIssue(2));
            _comans(node.getIssue(5));
            _senao(node.getIssue(7));

            return null;
        }

        /// <summary>
        /// <senao>   ::= $ | 'sont' '<<' <comans> '>>'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _senao(Node node)
        {
            if (node != null)
            {
                _comans(node.getIssue(2));
            }

            return null;
        }

        /// <summary>
        /// <laco>    ::= 'zum' '[' <exp> ']' '<<' <comans> '>>'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _laco(Node node)
        {
            _exp(node.getIssue(2));
            _comans(node.getIssue(5));

            return null;
        }

        /// <summary>
        /// <retorno> ::= 'out' <exp>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _retorno(Node node)
        {
            _exp(node.getIssue(1));
            return null;
        }

        /// <summary>
        /// <chamada> ::= id '[' <args> ']'
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _chamada(Node node)
        {
            Token id = node.getIssue(0).Token;
            _args(node.getIssue(2));

            return id;
        }

        /// <summary>
        /// <args>    ::= $ | <operan> <args2>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _args(Node node)
        {
            if(node != null)
            {
                _operan(node.getIssue(0));
                _args2(node.getIssue(1));
            }

            return null;
        }

        /// <summary>
        /// <args2>   ::= $ | ',' <operan> <args2>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private object _args2(Node node)
        {
            if(node != null)
            {
                _operan(node.getIssue(1));
                _args2(node.getIssue(2));
            }
            return null;
        }
            
    }
}
