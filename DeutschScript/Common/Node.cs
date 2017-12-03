using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Node
    {
        private Node father = new Node();
        private List<Node> issues = new List<Node>();
        private Token token = new Token();
        private string type;
        private StringBuilder treeImprint;

        public Node Father { get => father; set => father = value; }
        public string Type { get => type; set => type = value; }
        public Token Token { get => token; set => token = value; }

        public Node()
        {
        }

        public Node(Token token, string type)
        {
            this.type = type;
            this.token = token;
        }

        /// <summary>
        /// Adiciona novo filho.
        /// </summary>
        /// <param name="node"></param>
        public void addIssue(Node node)
        {
            issues.Add(node);
        }

        /// <summary>
        /// Retorna filho de acordo com indice.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Node getIssue(int index)
        {
            return issues[index];
        }

        /// <summary>
        /// Imprime toda árvore em ordem
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public string printTree(Node root)
        {
            treeImprint = new StringBuilder();
            printNo("", root);

            return treeImprint.ToString();
        }

        private void printNo(string space, Node tree)
        {
            treeImprint.AppendLine(space + tree.token.Image);

            foreach (Node node in tree.issues)
                printNo(space + " ", node);
        }
    }
}
