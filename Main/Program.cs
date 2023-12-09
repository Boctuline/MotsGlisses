using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
            int minutes, minutesTour,secondesTour;
            int secondes = 0;
            bool modePortail = false;
            Console.WriteLine("Comment voulez-vous ouvrir le plateau ?" +
                "\n1. Plateau g�n�r� al�atoirement" +
                "\n2. Plateau � partir d'un fichier");
            string rep;
            Plateau p = null;
            do
            {
                rep = Console.ReadLine();
                if (rep == "1") p = new Plateau();
                else if (rep == "2") p = new Plateau("..\\..\\..\\Main\\Fichiers\\Test1.csv");
                else Console.WriteLine("R�ponse incorrecte");
            } while (rep != "1" && rep != "2");

            Console.WriteLine("Notre jeu propose un mode de jeu alternatif appel� \"Mode portail\".\nC'est un mode de jeu permettant de consid�rer les bords du plateau comme des portails vers les bords oppos�s." +
                "\nSouhaite-vous l'activer ? y/n");
            do
            {
                rep = Console.ReadLine().ToLower();
                if (rep == "y") { modePortail = true; Console.WriteLine("Mode portail activ� ! "); }
                else if (rep == "n") Console.WriteLine("Mode portail d�sactiv� !");
                else Console.WriteLine("R�ponse incorrecte");
            } while (rep != "y" && rep != "n");
            Console.WriteLine("Quel est votre nom Joueur 1 ?");
            rep = Console.ReadLine();
            Joueur j1 = new Joueur(rep);
            Console.WriteLine("Quel est votre nom Joueur 2 ?");
            rep = Console.ReadLine();
            Joueur j2 = new Joueur(rep);
            Joueur jactuel = j2;
            Console.Write("Combien de temps avant la fin du jeu ? (Tapez \"d\" pour le temps par d�faut.)\nEn minutes : ");
            minutes = DemandeTimer(5);
            Console.Write("La partie durera donc " + minutes + " minutes !\nCombien de temps par tour ? (Tapez \"d\" pour le temps par d�faut.)\nEn minutes : ");
            minutesTour = DemandeTimer(0);
            Console.Write("Et en secondes ? : ");
            secondesTour = DemandeTimer(30);
            Console.WriteLine(minutes > 0 ? "Chaque tour durera donc " + minutesTour + " minutes et " + secondesTour + " secondes !" : "Chaque tour durera donc " + secondesTour + " secondes !");
            p.Afficher();
            DateTime debut = DateTime.Now;
            TimeSpan restant = new TimeSpan(0,minutes,secondes) - (DateTime.Now - debut);
            while (restant > new TimeSpan(0))
            {
                //Annonce du temps
                Console.WriteLine("Il vous reste " + restant.Minutes + " minutes et " + restant.Seconds + " secondes avant la fin du jeu.");
                //On d�cide du joueur
                if (jactuel == j2) jactuel = j1;
                else jactuel = j2;
                Console.WriteLine("Tour de " + jactuel.Nom + ".");

                //R�ponse du joueur
                DateTime debutTour = DateTime.Now;
                rep = Console.ReadLine();
                TimeSpan restantTour = new TimeSpan(0,minutesTour,secondesTour) - (DateTime.Now - debutTour);
                if (restantTour < new TimeSpan(0,0,0) ) Console.WriteLine(minutesTour>0?"Temps �coul� ! Vous avez pris " + -restantTour.Minutes + " minutes et " + -restantTour.Seconds + " secondes de trop!": "Temps �coul� ! Vous avez pris " + -restantTour.Seconds + " secondes de trop!");
                else if (rep.ToLower() == "skip") Console.WriteLine("Vous avez d�cider de passer ce tour.");
                else
                {
                    List<Case> cases = p.Recherche_Mot(rep, modePortail);
                    if (cases == null) Console.WriteLine("Le mot n'est pas dans le tableau ou ne commence pas � partir du bas !");
                    else
                    {
                        //Si le mot est correct
                        p.Maj_Plateau(new Cases(cases));
                        Console.WriteLine(Convert.ToString(rep[0]).ToUpper() + rep.Substring(1, rep.Length - 1) + " est dans la liste !\n" + "+" + jactuel.CalculScore(rep) + " points pour " + jactuel.Nom);
                        jactuel.Add_Score(jactuel.CalculScore(rep));
                        jactuel.Add_Mot(rep);

                    }
                }
                //On actualise le temps
                Thread.Sleep(50);
                restant = new TimeSpan(0, minutes, secondes) - (DateTime.Now - debut);
            }
            Console.WriteLine("Le jeu est termin� !");
            Console.WriteLine(j1.Nom + " a obtenu " + j1.Score + " points." +
                "\n" + j2.Nom + " a obtenu " + j2.Score + " points.");

        }
        public static int DemandeTimer(int temps)
        {
            bool repeat;
            do
            {
                string rep = Console.ReadLine();
                repeat = false;
                try
                {
                    if(rep.ToLower() != "d") temps = int.Parse(rep);
                }
                catch (FormatException) { Console.WriteLine("Veuillez taper un nombre"); repeat = true; }
                catch (OverflowException) { Console.WriteLine("Veuillez taper un temps plus petit"); repeat = true; }
            }while(repeat);
            return temps;
        }
    }
}
