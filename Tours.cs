using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    internal class Tours:Piece
    {
        public Tours(Couleur couleur, Position position, string symbole) : base(couleur, position, symbole)
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

            if (positionDepart.Ligne != positionArrivee.Ligne && positionDepart.Colonne != positionArrivee.Colonne)
            {
                RaisonsDeplacementImpossible.Add("Les tours se déplacent horizontalement ou verticalement uniquement.");
                return false;
            }

            if (!VerifDeplacementSansObstacle(positionDepart, positionArrivee, echiquier))
            {
                RaisonsDeplacementImpossible.Add("Obstacle sur le chemin. Le déplacement est bloqué par une autre pièce.");
                return false;
            }

            Piece pieceArrivee = echiquier[positionArrivee.Ligne, positionArrivee.Colonne - 'a'];
            if (pieceArrivee != null && pieceArrivee.Couleurs == couleur)
            {
                RaisonsDeplacementImpossible.Add("La position de destination est occupée par une pièce de la même couleur.");
                return false;
            }

            position = positionArrivee;
            return true;
        }

        private bool VerifDeplacementSansObstacle(Position depart, Position arrivee, Piece[,] echiquier)
        {
            if (depart.Ligne == arrivee.Ligne)
            {
                int increment = (arrivee.Colonne > depart.Colonne) ? 1 : -1;
                for (char colonne = (char)(depart.Colonne + increment); colonne != arrivee.Colonne; colonne = (char)(colonne + increment))
                {
                    if (echiquier[depart.Ligne, colonne - 'a'] != null)
                    {
                        return false;
                    }
                }
            }
            
            else if (depart.Colonne == arrivee.Colonne)
            {
                int increment = (arrivee.Ligne > depart.Ligne) ? 1 : -1;
                for (int ligne = depart.Ligne + increment; ligne != arrivee.Ligne; ligne += increment)
                {
                    if (echiquier[ligne - 1, depart.Colonne - 'a'] != null)
                    {
                        return false; 
                    }
                }
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
