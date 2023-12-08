using System;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
            int score;
            Console.WriteLine("Comment voulez-vous ouvrir le plateau ?" +
                "\n1. Plateau généré aléatoirement" +
                "\n2. Plateau à partir d'un fichier");
            string rep;
            Plateau p = null;
            do
            {
                rep = Console.ReadLine();
                if (rep == "1") p = new Plateau();
                else if (rep == "2") p = new Plateau("..\\..\\..\\Main\\Fichiers\\Test1.csv");
                else Console.WriteLine("Réponse incorrecte");
            } while (rep != "1" && rep != "2");
            Console.WriteLine("Quel est votre nom Joueur 1 ?");
            rep = Console.ReadLine();
            Joueur j1 = new Joueur(rep);
            Console.WriteLine("Quel est votre nom Joueur 2 ?");
            rep = Console.ReadLine();
            Joueur j2 = new Joueur(rep);
            Joueur jactuel = j2;
            p.Afficher();
            DateTime debut = DateTime.Now;
            while (true)
            {
                int minutes = 4 - (DateTime.Now - debut).Minutes;
                int secondes = 59 - (DateTime.Now - debut).Seconds;
                Console.WriteLine("Il vous reste " + minutes + " minutes et " + secondes + " secondes avant la fin du jeu.");
                if (jactuel == j2) jactuel = j1;
                else jactuel = j2;
                Console.WriteLine("Tour de " + jactuel.Nom + ".\nTapez \"Skip\" pour passer.");
                DateTime dt = DateTime.Now;
                rep = Console.ReadLine();
                TimeSpan now = DateTime.Now - dt;
                if (now.Seconds > 25) Console.WriteLine("Temps écoulé ! Vous avez pris " + now.Seconds + " secondes.");
                if (rep.ToLower() == "skip") Console.WriteLine("Vous avez décider de passer ce tour.");
                else
                {
                    List<Case> cases = p.Recherche_Mot(rep);
                    if (cases == null) Console.WriteLine("Le mot n'est pas dans le tableau");
                    else
                    {
                        p.Maj_Plateau(new Cases(cases));
                        Console.WriteLine(Convert.ToString(rep[0]).ToUpper() + rep.Substring(1, rep.Length - 1) + " est dans la liste !\n" + "+" + jactuel.Score(rep) + " points pour " + jactuel.Nom);
                        jactuel.Add_Score(jactuel.Score(rep));
                        jactuel.Add_Mot(rep);

                    }
                }
            }

        }
    }
}
