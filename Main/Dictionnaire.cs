using System;
using System.IO;

namespace MotsGlisses
{
    public class Dictionnaire
    {
        public Dictionnaire(string[][] tab)
        {
            tab = new string[26][];
            for (int i = 0; i < 26; i++)
            {
                tab[i] = new string[0]; 
            }
        }
        public static void ReadStringToFile(string fileName)
        {

            StreamReader sReader = null;
            try
            {

                sReader = new StreamReader(fileName);
                string line;
                while ((line = sReader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sReader != null) { sReader.Close(); }
            }

        }

        // Méthode pour afficher le dictionnaire (nombre de mots par lettre et langue)
        public string ToString(char a)
        {
            
            string result = "La lettre "+a+"contient "+ +"mots et est en français.;
            return result;
        }
        public bool RechDichRecursif(string mot)
        {
                
        }
        public static bool RechercheDichotomique(string[] tableau, string motRecherche)
        {
            // Vérifie si le tableau est trié
            Array.Sort(tableau);

            int min = 0;
            int max = tableau.Length - 1;

            return RechercheDichotomiqueRecursive(tableau, motRecherche, min, max);
        }

        private static bool RechercheDichotomiqueRecursive(string[][] tableau, string motRecherche, int min, int max)
        {
            if (tableau == null )
            {
                return false;
            }
            if (tableau.Length == 0)
            {
                return false;
            }

            int milieu = (tableau.Length) / 2;

            int comparison = string.Compare(tableau[milieu], motRecherche);

            if (comparison == 0)
            {
                return true; 
            }
            else if (comparison > 0)
            {
                return RechercheDichotomiqueRecursive(tableau, motRecherche, min, milieu - 1);
            }
            else
            {
 
                return RechercheDichotomiqueRecursive(tableau, motRecherche, milieu + 1, max);
            }
        }

        public void Tri_Fusion()
        {

        }
    }
}
