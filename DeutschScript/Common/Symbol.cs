using System.Collections.Generic;

namespace Common
{
    public class Symbol
    {
        private string image, escope, type, nature;
        private List<Symbol> parameters;

        public Symbol() { }
        public Symbol(string image)
        {
            this.image = image;
        }

        public string Image { get => image; set => image = value; }
        public string Escope { get => escope; set => escope = value; }
        public string Type { get => type; set => type = value; }
        public string Nature { get => nature; set => nature = value; }
        public List<Symbol> Parameters { get => parameters; set => parameters = value; }
    }
}
