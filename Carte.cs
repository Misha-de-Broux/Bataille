namespace Bataille {
    internal partial class Program {
        public struct Carte {
            public Suite suite;
            public Valeur valeur;

            public int CompareTo(Carte other) {
                return this.valeur - other.valeur;
            }

            override
            public string ToString() {
                return $"{this.valeur.ToString()} de {this.suite.ToString()}";
            }
        }
    }
}
