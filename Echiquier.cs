using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    internal class Echiquier
    {
        private Piece[,] Case;

        public Echiquier()
        {
            Case = new Piece[8, 8];
        }

        public Piece[,] GetCase()
        {
            return Case;
        }

        public void EmplacementInitial(Piece piece, Position position)
        {
            Case[position.Ligne - 1, position.Colonne - 'a'] = piece;
            piece.Positions = position;
        }

        public void AfficherEchiquier()
        {
            for (int ligne = 7; ligne >= 0; ligne--)
            {
                Console.Write($"{ligne + 1} ");
                for (int colonne = 0; colonne < 8; colonne++)
                {
                    Piece piece = Case[ligne, colonne];
                    if (piece != null)
                    {
                        Console.Write($"{piece} ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public bool EstEnEchec(Couleur couleurRoi, out Position positionRoi)
        {
            positionRoi = new Position(-1, '0');

            for (int ligne = 0; ligne < 8; ligne++)
            {
                for (int colonne = 0; colonne < 8; colonne++)
                {
                    Piece piece = Case[ligne, colonne];
                    if (piece != null)
                    {
                        if (piece is Rois && piece.Couleurs == couleurRoi)
                        {
                            positionRoi = new Position(ligne + 1, (char)('a' + colonne));
                        }
                        else if (piece.Couleurs != couleurRoi)
                        {
                            List<Position> coupsPossibles = piece.CoupPossible();
                            if (coupsPossibles.Contains(positionRoi))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }


        public bool Stalemate(Couleur couleurJoueur)
        {
            for (int ligne = 0; ligne < 8; ligne++)
            {
                for (int colonne = 0; colonne < 8; colonne++)
                {
                    Piece piece = Case[ligne, colonne];
                    if (piece != null && piece.Couleurs == couleurJoueur)
                    {
                        List<Position> coupsPossibles = piece.CoupPossible();
                        foreach (Position coup in coupsPossibles)
                        {
                            Piece[,] copieEchiquier = (Piece[,])Case.Clone();
                            
                            copieEchiquier[ligne, colonne] = null;
                            copieEchiquier[coup.Ligne - 1, coup.Colonne - 'a'] = piece;

                            
                            if (!EstEnEchec(couleurJoueur, out _))
                            {
                                return false; 
                            }
                        }
                    }
                }
            }

            return true; 
        }

        public bool EstEnEchecEtMat(Couleur couleurJoueur)
        {
            if (!EstEnEchec(couleurJoueur, out _))
            {
                return false;
            }

            for (int ligne = 0; ligne < 8; ligne++)
            {
                for (int colonne = 0; colonne < 8; colonne++)
                {
                    Piece piece = Case[ligne, colonne];
                    if (piece != null && piece.Couleurs == couleurJoueur)
                    {
                        List<Position> coupsPossibles = piece.CoupPossible();
                        foreach (Position coup in coupsPossibles)
                        {
                            
                            Piece pieceAvant = Case[coup.Ligne - 1, coup.Colonne - 'a'];
                            
                            Case[coup.Ligne - 1, coup.Colonne - 'a'] = piece;
                            Case[ligne, colonne] = null;
                            if (!EstEnEchec(couleurJoueur, out _))
                            {
                                
                                Case[ligne, colonne] = piece;
                                Case[coup.Ligne - 1, coup.Colonne - 'a'] = pieceAvant;
                                return false;
                            }
                            
                            Case[ligne, colonne] = piece;
                            Case[coup.Ligne - 1, coup.Colonne - 'a'] = pieceAvant;
                        }
                    }
                }
            }

            return true;
        }


    }
}
