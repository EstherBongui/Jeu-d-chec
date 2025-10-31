using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    internal class Position
    {
        private int ligne;
        private char colonne;

        public Position(int ligne, char colonne)
        {
            this.ligne = ligne;
            this.colonne = colonne;
        }

        public int Ligne { get {  return ligne; } }
        public char Colonne {  get { return colonne; } }

        public override string ToString()
        {
            return $"{Colonne}{Ligne}";
        }
    }
}
