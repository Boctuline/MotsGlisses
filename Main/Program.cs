using System;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Comment voulez-vous ouvrir le plateau ?" +
                "\n1. Plateau g�n�r� al�atoirement" +
                "\n2. Plateau � partir d'un fichier");
            string rep;
            Plateau p = null;
            do
            {
                rep = Console.ReadLine();
                if (rep == "1") p = new Plateau();
                if (rep == "2") p = new Plateau("..\\..\\..\\Main\\Fichiers\\Test1.csv");
                else Console.WriteLine("R�ponse incorrecte");
            } while (rep != "1" || rep != "2");
            p.Afficher();
            Joueur j1 = new Joueur("Shire");
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
