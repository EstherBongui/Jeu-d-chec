using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    internal class Reines : Piece
    {
        public Reines(Couleur couleur, Position position,string symbole) : base(couleur, position, symbole)
        {
        }

        public override bool Deplacement(string mouvement, Piece[,] echiquier)
        {
            RaisonsDeplacementImpossible.Clear();

            if (mouvement.Length != 5 ||
                mouvement[0] < 'a' || mouvement[0] > 'h' ||
                mouvement[1] < '1' || mouvement[1] > '8' ||
                mouvement[2] != ' ' ||
                mouvement[3] < 'a' || mouvement[3] > 'h' ||
                mouvement[4] < '1' || mouvement[4] > '8')
            {
                RaisonsDeplacementImpossible.Add("Format de mouvement invalide. Veuillez entrer un mouvement valide.");
                return false;
            }

            Position positionDepart = new Position(mouvement[1] - '0', mouvement[0]);
            Position positionArrivee = new Position(mouvement[4] - '0', mouvement[3]);

            if (echiquier[positionDepart.Ligne - 1, positionDepart.Colonne - 'a'] != this)
            {
                RaisonsDeplacementImpossible.Add("Il n'y a pas de reine de la couleur spécifiée à la position de départ.");
                return false;
            }

            List<Position> coupsPossibles = CoupPossible();

            if (!coupsPossibles.Contains(positionArrivee))
            {
                RaisonsDeplacementImpossible.Add("Déplacement non autorisé pour la reine. Veuillez entrer un mouvement valide.");
                return false;
            }

            Piece pieceArrivee = echiquier[positionArrivee.Ligne - 1, positionArrivee.Colonne - 'a'];

            if (pieceArrivee != null && pieceArrivee.Couleurs == this.Couleurs)
            {
                RaisonsDeplacementImpossible.Add("Une pièce de la même couleur occupe la case d'arrivée.");
                return false;
            }

            return true;
        }

        public override List<Position> CoupPossible()
        {
            List<Position> coups = new List<Position>();

            for (int i = 1; i <= 8; i++)
            {
                AjouterCoupSiValide(coups, position.Ligne + i, position.Colonne);

                AjouterCoupSiValide(coups, position.Ligne - i, position.Colonne);

                AjouterCoupSiValide(coups, position.Ligne, (char)(position.Colonne - i));

                AjouterCoupSiValide(coups, position.Ligne, (char)(position.Colonne + i));

                AjouterCoupSiValide(coups, position.Ligne + i, (char)(position.Colonne + i));

                AjouterCoupSiValide(coups, position.Ligne + i, (char)(position.Colonne - i));

                AjouterCoupSiValide(coups, position.Ligne - i, (char)(position.Colonne + i));

                AjouterCoupSiValide(coups, position.Ligne - i, (char)(position.Colonne - i));
            }

            return coups;
        }

        public void AjouterCoupSiValide(List<Position> coups, int ligne, char colonne)
        {
            if (SurEchiquier(ligne, colonne))
            {
                coups.Add(new Position(ligne, colonne));
            }
        }

        public bool SurEchiquier(int ligne, char colonne)
        {
            return ligne >= 1 && ligne <= 8 && colonne >= 'a' && colonne <= 'h';
        }
    }
}
