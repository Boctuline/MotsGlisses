using System;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
            Plateau p = new Plateau();
            Console.WriteLine(p.toString());
            Plateau p1 = new Plateau("..\\..\\..\\Main\\Fichiers\\Test1.csv");
            Console.WriteLine(p1.toString());
            while (true)
            {
               string rep = Console.ReadLine();
               List<Case> cases = p1.Recherche_Mot(rep);
               if (cases == null) Console.WriteLine("Null");
               else p1.Maj_Plateau(new Cases(cases));
            }

        }
    }
}
