using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {   
            //Intro
            string[] line = new string[8];
            line[0] = "  __  __  ___ _____ ___    ___ _    ___ ___ ___ ___ ___   _   _   _                                          \r\n";
            line[1] = " |  \\/  |/ _ \\_   _/ __|  / __| |  |_ _/ __/ __| __/ __| | | | | | |                                         \r\n";
            line[2] = " | |\\/| | (_) || | \\__ \\ | (_ | |__ | |\\__ \\__ \\ _|\\__ \\ |_| |_| |_|                                         \r\n";
            line[3] = " |_|_ |_|\\___/ |_|_|___/  \\___|____|___|___/___/___|___/ (_) (_) (_)               _        _____ ___    ___ \r\n";
            line[4] = " | _ \\__ _ _ _  / __| |_ (_)_  _ __ _ _ __    ___| |_   _ | |__ _(_)_ __  ___   __| |_  _  |_   _|   \\  | __|\r\n";
            line[5] = " |  _/ _` | '_| \\__ \\ ' \\| | || / _` | '  \\  / -_)  _| | || / _` | | '  \\/ -_) / _` | || |   | | | |) | | _| \r\n";
            line[6] = " |_| \\__,_|_|   |___/_||_|_|\\_, \\__,_|_|_|_| \\___|\\__|  \\__/\\__,_|_|_|_|_\\___| \\__,_|\\_,_|   |_| |___/  |_|  \r\n";
            line[7] = "                            |__/                                                                             \r\n";
            string intro;
            bool sortir = false;
            while (!sortir)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                for (int i = line.Length; i >= 0; i--)
                {
                    intro = "";
                    for (int j = i; j < line.Length; j++)
                    {
                        intro += line[j];
                    }
                    Console.WriteLine(intro);
                    Thread.Sleep(40);
                    if (i != 0) Console.Clear();
                }
                Console.ForegroundColor = ConsoleColor.White;

                //Création et tri du dictionnaire
                Dictionnaire dictionnaire = new Dictionnaire("..\\..\\..\\Main\\Fichiers\\Mots_Français.txt", "Français");
                foreach(List<string> liste in dictionnaire.Dico)
                dictionnaire.Tri_Fusion_Reccursif(liste);

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
                    else if (rep == "1") { p = new Plateau(); Console.Clear(); Console.WriteLine("Le plateau a été généré aléatoirement !"); }
                    else if (rep == "2")
                    {
                        int fichier = 0;
                        bool number = true;
                        string[] files = Directory.GetFiles("..\\..\\..\\Main\\Fichiers\\Plateaux");
                        Console.WriteLine("A partir de quel fichier voulez vous ouvrir le plateau ?");
                        for (int i = 1; i <= files.Length; i++)
                            Console.WriteLine(i + ". " + files[i - 1].Split("\\")[6]);
                        do
                        {
                            number = true;
                            try
                            {
                                fichier = int.Parse(Console.ReadLine());
                                if (fichier > files.Length) { Console.WriteLine("Veuillez entrez un nombre inférieur ou égal à " + files.Length); number = false; }
                                if (fichier < 1) { Console.WriteLine("Veuillez entrez un nombre supérieur ou égal à " + 1); number = false; }
                            }
                            catch (FormatException) { number = false; Console.WriteLine("Veuillez entrer un nombre ! "); }
                            catch (OverflowException) { number = false; Console.WriteLine("Veuillez entrez un nombre inférieur ou égal à " + files.Length); }
                            catch (ArgumentNullException ex) { number = false; Console.Write(ex); }
                        } while (!number);
                        p = new Plateau(files[fichier - 1]);
                        Console.Clear();
                        Console.WriteLine("Le plateau sera donc ouvert à partir du fichier " + files[fichier - 1].Split("\\")[6] + " !");
                    }
                    else Console.WriteLine("Réponse incorrecte");
                } while (rep != "1" && rep != "2" && !sortir);
                
                if (!sortir)
                {
                    //Choix du mode de jeu
                    Console.WriteLine("\nNotre jeu propose un mode de jeu alternatif appelé \"Mode portail\".\nC'est un mode de jeu permettant de considérer les bords du plateau comme des portails vers les bords opposés." +
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
                    Jeu jeu = new Jeu(p, j1, j2, dictionnaire, modePortail);
                    Console.WriteLine("Voulez vous lancer une nouvelle partie ? y/n");
                    do
                    {
                        rep = Console.ReadLine().ToLower();
                        if (rep == "n") sortir = true;
                        else if (rep!="y") Console.WriteLine("Réponse incorrecte");
                    } while (rep != "y" && rep != "n");
                }
            }
        }
    }
}
