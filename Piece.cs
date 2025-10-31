using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public enum Couleur
    {
        Blanc,
        Noir
    }
    internal abstract class Piece
    {
        protected Couleur couleur;
        protected Position position;
        protected string symbole;
        public List<string> RaisonsDeplacementImpossible { get; private set; }

        public Piece(Couleur couleur, Position position, string symbole)
        {
            this.couleur = couleur;
            this.position = position;
            this.symbole = symbole;
            RaisonsDeplacementImpossible = new List<string>();
        }

        public Couleur Couleurs { get { return couleur; } }

        public string Symbole {  get { return symbole; } }

        public Position Positions { get { return position; } set { } }

        public abstract bool Deplacement(string mouvement, Piece[,] echiquier);

        public abstract List<Position> CoupPossible();

        public override string ToString()
        {
            return symbole;
        }
    }
}
