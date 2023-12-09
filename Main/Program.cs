using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool sortir = false;
            while (!sortir)
            {
                //S�lection du plateau
                Console.WriteLine("Comment voulez-vous ouvrir le plateau ?" +
                    "\n1. Plateau g�n�r� al�atoirement" +
                    "\n2. Plateau � partir d'un fichier" +
                    "\n3. Sortir");
                string rep;
                Plateau p = null;
                do
                {
                    rep = Console.ReadLine();
                    if (rep == "3") { Console.WriteLine("Au revoir !"); sortir = true; }
                    else if (rep == "1") p = new Plateau();
                    else if (rep == "2") p = new Plateau("..\\..\\..\\Main\\Fichiers\\Test1.csv");
                    else Console.WriteLine("R�ponse incorrecte");
                } while (rep != "1" && rep != "2" && !sortir);
                if (!sortir)
                {
                    //Choix du mode de jeu
                    Console.WriteLine("Notre jeu propose un mode de jeu alternatif appel� \"Mode portail\".\nC'est un mode de jeu permettant de consid�rer les bords du plateau comme des portails vers les bords oppos�s." +
                        "\nSouhaite-vous l'activer ? y/n");
                    bool modePortail = false;
                    do
                    {
                        rep = Console.ReadLine().ToLower();
                        if (rep == "y") { modePortail = true; Console.WriteLine("Mode portail activ� ! "); }
                        else if (rep == "n") Console.WriteLine("Mode portail d�sactiv� !");
                        else Console.WriteLine("R�ponse incorrecte");
                    } while (rep != "y" && rep != "n");

                    //Cr�ation des joueurs
                    Console.WriteLine("Quel est votre nom Joueur 1 ?");
                    rep = Console.ReadLine();
                    Joueur j1 = new Joueur(rep);
                    Console.WriteLine("Quel est votre nom Joueur 2 ?");
                    rep = Console.ReadLine();
                    Joueur j2 = new Joueur(rep);

                    //Lancement du jeu
                    Jeu jeu = new Jeu(p, j1, j2, modePortail);
                    Console.WriteLine();
                    for (int i = 0; i < 8; i++)
                    {
                        Console.Write("____");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Nouveau jeu !!");
                }
            }
        }
    }
}
