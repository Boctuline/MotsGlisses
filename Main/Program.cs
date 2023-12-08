using System;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Comment voulez-vous ouvrir le plateau ?");

            Plateau p = new Plateau();
            p.Afficher();
            Plateau p1 = new Plateau("..\\..\\..\\Main\\Fichiers\\Test1.csv");
            p1.Afficher();
            Joueur j1 = new Joueur("Shire");
            DateTime dt = DateTime.Now;
            while (true)
            {
               string rep = Console.ReadLine();
               List<Case> cases = p1.Recherche_Mot(rep);
                if (cases == null) Console.WriteLine("Le mot n'est pas dans le tableau");
                else
                {
                    p1.Maj_Plateau(new Cases(cases));
                    Console.WriteLine(j1.Score(rep));
                    //Add score
                    //Add mot

                }
            }

        }
    }
}
