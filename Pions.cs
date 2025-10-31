using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    internal class Pions : Piece
    {

        public Pions(Couleur couleur, Position position,string symbole) : base(couleur, position,symbole)
        {
        }

        public override bool Deplacement(string mouvement, Piece[,] echiquier)
        {
            RaisonsDeplacementImpossible.Clear();

            if (mouvement.Length != 5 ||
                mouvement[0] < 'a' || mouvement[0] > 'h' ||
                mouvement[1] < '2' || mouvement[1] > '7' ||
                mouvement[2] != ' ' ||
                mouvement[3] < 'a' || mouvement[3] > 'h' ||
                mouvement[4] < '2' || mouvement[4] > '7')
            {
                RaisonsDeplacementImpossible.Add("Format de mouvement invalide. Veuillez entrer un mouvement valide.");
                return false;
            }

            Position positionDepart = new Position(mouvement[1] - '0', mouvement[0]);
            Position positionArrivee = new Position(mouvement[4] - '0', mouvement[3]);

            int direction = (couleur == Couleur.Blanc) ? 1 : -1;

            Piece pieceDepart = echiquier[positionDepart.Ligne - 1, positionDepart.Colonne - 'a'];
            Piece pieceArrivee = echiquier[positionArrivee.Ligne - 1, positionArrivee.Colonne - 'a'];

            if (pieceArrivee == null)
            {
                this.position = positionArrivee;
                return true;
            }
            else if (PremierMouvement() && pieceArrivee == null)
            {
                if (echiquier[positionDepart.Ligne - 1 + direction, positionDepart.Colonne - 'a'] == null &&
                    echiquier[positionArrivee.Ligne - 1, positionArrivee.Colonne - 'a'] == null)
                {
                    this.position = positionArrivee;
                    return true;
                }
                else
                {
                    RaisonsDeplacementImpossible.Add("Les deux cases entre la position de départ et d'arrivée doivent être vides.");
                }
            }
            else
            {
                RaisonsDeplacementImpossible.Add("Déplacement invalide pour un pion.");
            }

            return false;
        }

        public override List<Position> CoupPossible()
        {
            List<Position> coups = new List<Position>();

            int direction = (couleur == Couleur.Blanc) ? 1 : -1;
            int nouvelleLigne = position.Ligne + direction;

            AjouterCoupSiValide(coups, nouvelleLigne, position.Colonne);

            if (PremierMouvement())
            {
                AjouterCoupSiValide(coups, nouvelleLigne + direction, position.Colonne);
            }

            AjouterCoupSiValide(coups, nouvelleLigne, (char)(position.Colonne - 1));
            AjouterCoupSiValide(coups, nouvelleLigne, (char)(position.Colonne + 1));

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

        public bool PremierMouvement()
        {
            int ligneCible = (couleur == Couleur.Blanc) ? 2 : 7;
            return position.Ligne == ligneCible;
        }
    }
}
