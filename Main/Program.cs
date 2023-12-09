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
                //Sélection du plateau
                Console.WriteLine("Comment voulez-vous ouvrir le plateau ?" +
                    "\n1. Plateau généré aléatoirement" +
                    "\n2. Plateau à partir d'un fichier" +
                    "\n3. Sortir");
                string rep;
                Plateau p = null;
                do
                {
                    rep = Console.ReadLine();
                    if (rep == "3") { Console.WriteLine("Au revoir !"); sortir = true; }
                    else if (rep == "1") p = new Plateau();
                    else if (rep == "2") p = new Plateau("..\\..\\..\\Main\\Fichiers\\Test1.csv");
                    else Console.WriteLine("Réponse incorrecte");
                } while (rep != "1" && rep != "2" && !sortir);
                if (!sortir)
                {
                    //Choix du mode de jeu
                    Console.WriteLine("Notre jeu propose un mode de jeu alternatif appelé \"Mode portail\".\nC'est un mode de jeu permettant de considérer les bords du plateau comme des portails vers les bords opposés." +
                        "\nSouhaite-vous l'activer ? y/n");
                    bool modePortail = false;
                    do
                    {
                        rep = Console.ReadLine().ToLower();
                        if (rep == "y") { modePortail = true; Console.WriteLine("Mode portail activé ! "); }
                        else if (rep == "n") Console.WriteLine("Mode portail désactivé !");
                        else Console.WriteLine("Réponse incorrecte");
                    } while (rep != "y" && rep != "n");

                    //Création des joueurs
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
