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

            //string objectType = TabTipo.converte(tipoDS);

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

        }

    }
}
