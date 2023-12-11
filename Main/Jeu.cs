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

        public Jeu(Plateau plateau, Joueur j1, Joueur j2, Dictionnaire dictionnaire, bool modePortail, int animation) {
            int minutes, minutesTour, secondesTour;
            int secondes = 0;
            string rep;
            Joueur jactuel = j2;
            Console.Write("Combien de temps avant la fin du jeu ? 59mn maximum. (Tapez \"d\" pour le temps par défaut de 5mn.)\nEn minutes : ");
            minutes = DemandeTimer(5);
            Console.Write("La partie durera donc " + minutes + " minutes !\nCombien de temps par tour ? 59mn et 59s maximum. (Tapez \"d\" pour le temps par défaut de 0mn30s.)\nEn minutes : ");
            minutesTour = DemandeTimer(0);
            Console.Write("Et en secondes ? : ");
            secondesTour = DemandeTimer(30, true);
            Titre();
            Console.WriteLine(minutes > 0 ? "Chaque tour durera donc " + minutesTour + " minutes et " + secondesTour + " secondes !" : "Chaque tour durera donc " + secondesTour + " secondes !");
            Console.Write("Vous pouvez entrer "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("\"Skip\""); Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" pour passer votre tour, ou "); Console.ForegroundColor = ConsoleColor.Red; Console.Write("\"Exit\""); Console.ForegroundColor = ConsoleColor.White; Console.WriteLine(" pour terminer le jeu plus vite.");
            plateau.Afficher();
            DateTime debut = DateTime.Now;
            TimeSpan restant = new TimeSpan(0, minutes, secondes) - (DateTime.Now - debut);
            while (restant > new TimeSpan(0) && !plateau.Plateau_Vide())
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
                    if (restantTour < new TimeSpan(0, 0, 0)) { Titre(); plateau.Afficher(); Console.WriteLine(restantTour.Minutes > 0 ? "Temps écoulé ! Vous avez pris " + -restantTour.Minutes + " minutes et " + -restantTour.Seconds + " secondes de trop!" : "Temps écoulé ! Vous avez pris " + -restantTour.Seconds + " secondes de trop!"); }
                    else if (rep.ToLower() == "skip") { Titre(); plateau.Afficher(); Console.WriteLine(jactuel.Nom + ", vous avez décider de passer ce tour."); }
                    else if (rep.ToLower() == "exit") { debut = DateTime.Now  - new TimeSpan(2,0,0); }
                    else if(!dictionnaire.RechDichoRecursif(rep) && rep.ToLower() != "gilles nocturne") { Titre(); plateau.Afficher(); Console.WriteLine(rep + " n'est pas un mot " + dictionnaire.Langue + " !\n" + jactuel.Nom + ", il vous reste " + restantTour.Minutes + " minutes et " + restantTour.Seconds + " secondes.\nEntrez un mot à nouveau."); repeat = true; }
                    else if (rep.Length < 2) { Titre(); plateau.Afficher(); Console.WriteLine("Vous devez proposer un mot d'au moins 2 lettre !\n" + jactuel.Nom + ", il vous reste " + restantTour.Minutes + " minutes et " + restantTour.Seconds + " secondes.\nEntrez un mot à nouveau."); repeat = true; }
                    else if (jactuel.Contient(rep.ToLower())) { Titre(); plateau.Afficher(); Console.WriteLine("Vous avez déjà trouvé ce mot !\n" + jactuel.Nom + ", il vous reste " + restantTour.Minutes + " minutes et " + restantTour.Seconds + " secondes.\nEntrez un mot à nouveau."); repeat = true; }
                    else
                    {
                        List<Case> cases = plateau.Recherche_Mot(rep, modePortail);
                        if (cases == null) { Titre(); plateau.Afficher(); Console.WriteLine( rep +" n'est pas dans le tableau ou ne commence pas à partir du bas !\n" + jactuel.Nom + ", il vous reste " + restantTour.Minutes + " minutes et " + restantTour.Seconds + " secondes.\nEntrez un mot à nouveau."); repeat = true; }
                        else
                        {
                            //Si le mot est correct
                            plateau.Maj_Plateau(new Cases(cases), animation);
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
        public static int DemandeTimer(int temps, bool type = false)
        {
            bool repeat;
            do
            {
                string rep = Console.ReadLine();
                repeat = false;
                try
                {
                    if (rep.ToLower() != "d") temps = int.Parse(rep);
                    if (temps > 59) { Console.WriteLine(type?"59 secondes maximum !":"59 minutes maximum !"); repeat = true; }
                    if (temps < 0) { Console.WriteLine("Entrez un nombre positif"); repeat = true; }
                }
                catch (FormatException) { Console.WriteLine("Veuillez taper un nombre"); repeat = true; }
                catch (OverflowException) { Console.WriteLine("Veuillez taper un temps plus petit"); repeat = true; }
                catch (ArgumentNullException ex) { Console.Write(ex); repeat = true; }
            } while (repeat);
            return temps;
        }
        public static void Titre()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  __  __  ___ _____ ___    ___ _    ___ ___ ___ ___ ___   _ _ _                                              \r\n |  \\/  |/ _ \\_   _/ __|  / __| |  |_ _/ __/ __| __/ __| | | | |                                             \r\n | |\\/| | (_) || | \\__ \\ | (_ | |__ | |\\__ \\__ \\ _|\\__ \\ |_|_|_|                                             \r\n |_|_ |_|\\___/ |_|_|___/  \\___|____|___|___/___/___|___/ (_|_|_)  _                _        _____ ___    ___ \r\n | _ \\__ _ _ _  / __| |_ (_)_  _ __ _ _ __    ___| |_   _ | |__ _(_)_ __  ___   __| |_  _  |_   _|   \\  | __|\r\n |  _/ _` | '_| \\__ \\ ' \\| | || / _` | '  \\  / -_)  _| | || / _` | | '  \\/ -_) / _` | || |   | | | |) | | _| \r\n |_| \\__,_|_|   |___/_||_|_|\\_, \\__,_|_|_|_| \\___|\\__|  \\__/\\__,_|_|_|_|_\\___| \\__,_|\\_,_|   |_| |___/  |_|  \r\n                            |__/                                                                             \r\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
