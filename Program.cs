using Projet;
using System.Runtime.CompilerServices;

Echiquier echiquier = new Echiquier();
List<Piece> pieces = InitialiserEchiquier(echiquier);

bool partieTerminee=false;
int nombreCoups = 0;
Couleur joueurActuel = Couleur.Blanc;

while (!partieTerminee)
{
    Console.Clear();
    echiquier.AfficherEchiquier();

    if (echiquier.EstEnEchec(joueurActuel, out _))
    {
        Console.WriteLine("Le roi est en échec !");
        if (echiquier.EstEnEchecEtMat(joueurActuel))
        {
            Console.WriteLine("Le roi est en échec et mat !");
            partieTerminee = true;
            break;
        }
    }
    else if (echiquier.Stalemate(joueurActuel))
    {
        Console.WriteLine("Stalemate !");
        partieTerminee = true;
        break;
    }

    Console.WriteLine($"Tour du joueur {joueurActuel.ToString().ToLower()}");

    Console.Write("Entrez le mouvement (par exemple, b2 b4) : ");
    string mouvement = Console.ReadLine();

    bool deplacementReussi = false;
    foreach (var piece in pieces)
    {
        if (piece.Deplacement(mouvement, echiquier.GetCase()))
        {
            nombreCoups++;
            deplacementReussi = true;
            Console.WriteLine("Déplacement réussi !");
            break;
        }
    }

    if (!deplacementReussi)
    {
        Console.WriteLine("Déplacement invalide. Raisons :");
        foreach (var piece in pieces)
        {
            if (!piece.Deplacement(mouvement, echiquier.GetCase()))
            {
                foreach (var raison in piece.RaisonsDeplacementImpossible)
                {
                    Console.WriteLine($"  - {raison}");
                }
            }
        }
    }


    partieTerminee = echiquier.EstEnEchec(joueurActuel, out _) || echiquier.Stalemate(joueurActuel) || echiquier.EstEnEchecEtMat(joueurActuel);

    joueurActuel = (joueurActuel == Couleur.Blanc) ? Couleur.Noir : Couleur.Blanc;

    Thread.Sleep(2000);
}

Console.WriteLine($"La partie est terminée en {nombreCoups} coups.");

static List<Piece> InitialiserEchiquier(Echiquier echiquier)
{
    List<Piece> pieces = new List<Piece>();

    Pions pionBlanc1 = new Pions(Couleur.Blanc, new Position(2, 'a'), "P");
    pieces.Add(pionBlanc1);
    Pions pionBlanc2 = new Pions(Couleur.Blanc, new Position(2, 'b'), "P");
    pieces.Add(pionBlanc2);
    Pions pionBlanc3 = new Pions(Couleur.Blanc, new Position(2, 'c'), "P");
    pieces.Add(pionBlanc3);
    Pions pionBlanc4 = new Pions(Couleur.Blanc, new Position(2, 'd'), "P");
    pieces.Add(pionBlanc4);
    Pions pionBlanc5 = new Pions(Couleur.Blanc, new Position(2, 'e'), "P");
    pieces.Add(pionBlanc5);
    Pions pionBlanc6 = new Pions(Couleur.Blanc, new Position(2, 'f'), "P");
    pieces.Add(pionBlanc6);
    Pions pionBlanc7 = new Pions(Couleur.Blanc, new Position(2, 'g'), "P");
    pieces.Add(pionBlanc7);
    Pions pionBlanc8 = new Pions(Couleur.Blanc, new Position(2, 'h'), "P");
    pieces.Add(pionBlanc8);
    Pions pionNoir1 = new Pions(Couleur.Noir, new Position(7, 'a'), "p");
    pieces.Add(pionNoir1);
    Pions pionNoir2 = new Pions(Couleur.Noir, new Position(7, 'b'), "p");
    pieces.Add(pionNoir2);
    Pions pionNoir3 = new Pions(Couleur.Noir, new Position(7, 'c'), "p");
    pieces.Add(pionNoir3);
    Pions pionNoir4 = new Pions(Couleur.Noir, new Position(7, 'd'), "p");
    pieces.Add(pionNoir4);
    Pions pionNoir5 = new Pions(Couleur.Noir, new Position(7, 'e'), "p");
    pieces.Add(pionNoir5);
    Pions pionNoir6 = new Pions(Couleur.Noir, new Position(7, 'f'), "p");
    pieces.Add(pionNoir6);
    Pions pionNoir7 = new Pions(Couleur.Noir, new Position(7, 'g'), "p");
    pieces.Add(pionNoir7);
    Pions pionNoir8 = new Pions(Couleur.Noir, new Position(7, 'h'), "p");
    pieces.Add(pionNoir8);

    Reines reineBlanche=new Reines(Couleur.Blanc,new Position(1, 'd'),"D");
    pieces.Add(reineBlanche);
    Reines reineNoire = new Reines(Couleur.Noir, new Position(8, 'd'), "d");
    pieces.Add(reineNoire);
    
    Rois roiBlanc = new Rois(Couleur.Blanc, new Position(1, 'e'), "R");
    pieces.Add(roiBlanc);
    Rois roiNoir = new Rois(Couleur.Noir, new Position(8, 'e'), "r");
    pieces.Add(roiNoir);

    Tours tourBlanche1 = new Tours(Couleur.Blanc, new Position(1, 'a'), "T");
    pieces.Add(tourBlanche1);
    Tours tourBlanche2 = new Tours(Couleur.Blanc, new Position(1, 'h'), "T");
    pieces.Add(tourBlanche2);
    Tours tourNoire1 = new Tours(Couleur.Noir, new Position(8, 'a'), "t");
    pieces.Add(tourNoire1);
    Tours tourNoire2 = new Tours(Couleur.Noir, new Position(8, 'h'), "t");
    pieces.Add(tourNoire2);
    
    Cavaliers cavalierBlanc1 = new Cavaliers(Couleur.Blanc, new Position(1, 'b'), "C");
    pieces.Add(cavalierBlanc1);
    Cavaliers cavalierBlanc2 = new Cavaliers(Couleur.Blanc, new Position(1, 'g'), "C");
    pieces.Add(cavalierBlanc2);
    Cavaliers cavalierNoir1 = new Cavaliers(Couleur.Noir, new Position(8, 'b'), "c");
    pieces.Add(cavalierNoir1);
    Cavaliers cavalierNoir2 = new Cavaliers(Couleur.Noir, new Position(8, 'g'), "c");
    pieces.Add(cavalierNoir2);
    
    Fous fouBlanc1 = new Fous(Couleur.Blanc, new Position(1, 'c'), "F");
    pieces.Add(fouBlanc1);
    Fous fouBlanc2 = new Fous(Couleur.Blanc, new Position(1, 'f'), "F");
    pieces.Add(fouBlanc2);
    Fous fouNoir1 = new Fous(Couleur.Noir, new Position(8, 'c'), "f");
    pieces.Add(fouNoir1);
    Fous fouNoir2 = new Fous(Couleur.Noir, new Position(8, 'f'), "f");
    pieces.Add(fouNoir2);

    foreach (var piece in pieces)
    {
        echiquier.EmplacementInitial(piece, piece.Positions);
    }

    return pieces;
}