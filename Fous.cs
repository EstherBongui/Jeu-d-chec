using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    internal class Fous : Piece
    {
        public Fous(Couleur couleur, Position position, string symbole) : base(couleur, position, symbole)
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

            if (Math.Abs(positionArrivee.Ligne - positionDepart.Ligne) == Math.Abs(positionArrivee.Colonne - positionDepart.Colonne))
            {
                if (VerifDeplacementSansObstacle(positionDepart, positionArrivee, echiquier))
                {
                    Piece pieceArrivee = echiquier[positionArrivee.Ligne, positionArrivee.Colonne - 'a'];

                    if (pieceArrivee == null || pieceArrivee.Couleurs != this.Couleurs)
                    {
                        position = positionArrivee;
                        return true;
                    }
                    else
                    {
                        RaisonsDeplacementImpossible.Add("La position de destination est occupée par une pièce de la même couleur.");
                    }
                }
                else
                {
                    RaisonsDeplacementImpossible.Add("Obstacle sur le chemin. Le déplacement est bloqué par une autre pièce.");
                }
            }
            else
            {
                RaisonsDeplacementImpossible.Add("Déplacement non autorisé pour le fou. Veuillez entrer un mouvement valide.");
            }

            return false;
        }

        private bool VerifDeplacementSansObstacle(Position depart, Position arrivee, Piece [,] echiquier)
        {
            int incrementLigne = (arrivee.Ligne > depart.Ligne) ? 1 : -1;
            int incrementColonne = (arrivee.Colonne > depart.Colonne) ? 1 : -1;

            int ligne = depart.Ligne + incrementLigne;
            char colonne = (char)(depart.Colonne + incrementColonne);

            while (ligne != arrivee.Ligne && colonne != arrivee.Colonne)
            {
                if (echiquier[ligne, colonne - 'a'] != null)
                {
                    return false;
                }

                ligne += incrementLigne;
                colonne = (char)(colonne + incrementColonne);
            }

            return true;
        }


        public override List<Position> CoupPossible()
        {
            List<Position> coups = new List<Position>();

            for (int i = 1; i <= 8; i++)
            {
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
