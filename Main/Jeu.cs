using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MotsGlisses
{
    public class Jeu
    {
        Dictionnaire dictionnaire;
        Plateau p;
        Joueur j1;
        Joueur j2;
        public Jeu(Plateau p, Joueur j1, Joueur j2, Dictionnaire dictionnaire, bool modePortail) {
            int minutes, minutesTour, secondesTour;
            int secondes = 0;
            string rep;
            Joueur jactuel = j2;
            Console.Write("Combien de temps avant la fin du jeu ? 59min maximum. (Tapez \"d\" pour le temps par défaut.)\nEn minutes : ");
            minutes = DemandeTimer(5);
            Console.Write("La partie durera donc " + minutes + " minutes !\nCombien de temps par tour ? 59min maximum. (Tapez \"d\" pour le temps par défaut.)\nEn minutes : ");
            minutesTour = DemandeTimer(0);
            Console.Write("Et en secondes ? : ");
            secondesTour = DemandeTimer(30);
            Console.Clear();
            Console.WriteLine(minutes > 0 ? "Chaque tour durera donc " + minutesTour + " minutes et " + secondesTour + " secondes !\nVous pouvez entrer \"Skip\" pour passer votre tour." : "Chaque tour durera donc " + secondesTour + " secondes !\nVous pouvez entrer \"Skip\" pour passer votre tour.");
            p.Afficher();
            DateTime debut = DateTime.Now;
            TimeSpan restant = new TimeSpan(0, minutes, secondes) - (DateTime.Now - debut);
            while (restant > new TimeSpan(0))
            {
                //Annonce du temps
                Console.WriteLine("Il vous reste " + restant.Minutes + " minutes et " + restant.Seconds + " secondes avant la fin du jeu.");
                //On décide du joueur
                if (jactuel == j2) jactuel = j1;
                else jactuel = j2;
                Console.WriteLine("Tour de " + jactuel.Nom + ".");
                DateTime debutTour = DateTime.Now;
                bool repeat;
                do
                {
                    repeat = false;
                    //Réponse du joueur
                    rep = Console.ReadLine();
                    TimeSpan restantTour = new TimeSpan(0, minutesTour, secondesTour) - (DateTime.Now - debutTour);
                    if (restantTour < new TimeSpan(0, 0, 0)) { Console.Clear(); p.Afficher(); Console.WriteLine(restantTour.Minutes > 0 ? "Temps écoulé ! Vous avez pris " + -restantTour.Minutes + " minutes et " + -restantTour.Seconds + " secondes de trop!" : "Temps écoulé ! Vous avez pris " + -restantTour.Seconds + " secondes de trop!"); }
                    else if (rep.ToLower() == "skip") { Console.Clear(); p.Afficher(); Console.WriteLine(jactuel.Nom + ", vous avez décider de passer ce tour."); }
                    else if(!dictionnaire.RechDichoRecursif(rep) && rep.ToLower() != "gilles nocturne") { Console.Clear(); p.Afficher(); Console.WriteLine(rep + " n'est pas un mot " + dictionnaire.Langue + " !\n" + jactuel.Nom + ", il vous reste " + restantTour.Minutes + " minutes et " + restantTour.Seconds + " secondes.\nEntrez un mot à nouveau."); repeat = true; }
                    else if (rep.Length < 2) { Console.Clear(); p.Afficher(); Console.WriteLine("Vous devez proposer un mot d'au moins 2 lettre !\n" + jactuel.Nom + ", il vous reste " + restantTour.Minutes + " minutes et " + restantTour.Seconds + " secondes.\nEntrez un mot à nouveau."); repeat = true; }
                    else if (jactuel.Contient(rep.ToLower())) { Console.Clear(); p.Afficher(); Console.WriteLine("Vous avez déjà trouvé ce mot !\n" + jactuel.Nom + ", il vous reste " + restantTour.Minutes + " minutes et " + restantTour.Seconds + " secondes.\nEntrez un mot à nouveau."); repeat = true; }
                    else
                    {
                        List<Case> cases = p.Recherche_Mot(rep, modePortail);
                        if (cases == null) { Console.Clear(); p.Afficher(); Console.WriteLine( rep +" n'est pas dans le tableau ou ne commence pas à partir du bas !\n" + jactuel.Nom + ", il vous reste " + restantTour.Minutes + " minutes et " + restantTour.Seconds + " secondes.\nEntrez un mot à nouveau."); repeat = true; }
                        else
                        {
                            //Si le mot est correct
                            p.Maj_Plateau(new Cases(cases));
                            Console.WriteLine(Convert.ToString(rep[0]).ToUpper() + rep.Substring(1, rep.Length - 1) + " est dans la liste !\n" + "+" + jactuel.CalculScore(rep) + " points pour " + jactuel.Nom);
                            jactuel.Add_Score(jactuel.CalculScore(rep));
                            jactuel.Add_Mot(rep);

                        }
                    }
                } while (repeat && new TimeSpan(0, minutes, secondes) - (DateTime.Now - debut) > new TimeSpan(0,0,0));
                //On actualise le temps
                Thread.Sleep(50);
                restant = new TimeSpan(0, minutes, secondes) - (DateTime.Now - debut);
            }
            Console.WriteLine("Le jeu est terminé !");
            Console.WriteLine(j1.toString() + "\n" +j2.toString());
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
                    if (rep.ToLower() != "d") temps = int.Parse(rep);
                    if (temps > 59) { Console.WriteLine("59 minutes maximum !"); repeat = true; }
                }
                catch (FormatException) { Console.WriteLine("Veuillez taper un nombre"); repeat = true; }
                catch (OverflowException) { Console.WriteLine("Veuillez taper un temps plus petit"); repeat = true; }
            } while (repeat);
            return temps;
        }
    }
}
