using System;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
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
            p.Afficher();
            Console.WriteLine("Quel est votre nom Joueur 1 ?");
            rep = Console.ReadLine();
            Joueur j1 = new Joueur(rep);
            Console.WriteLine("Quel est votre nom Joueur 2 ?");
            rep = Console.ReadLine();
            Joueur j2 = new Joueur(rep);
            DateTime dt = DateTime.Now;
            while (true)
            {
               rep = Console.ReadLine();
               List<Case> cases = p.Recherche_Mot(rep);
                if (cases == null) Console.WriteLine("Le mot n'est pas dans le tableau");
                else
                {
                    p.Maj_Plateau(new Cases(cases));
                    Console.WriteLine(j1.Score(rep));
                    //Add score
                    //Add mot

                }
            }

        }
    }
}
