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
            return null;
        }
        public bool RechDichRecursif(string mot)
        {
            return false;
        }
        public static bool RechercheDichotomique(string[] tableau, string motRecherche)
        {
            return false;
        }

        private static bool RechercheDichotomiqueRecursive(string[][] tableau, string motRecherche, int min, int max)
        {
            return false;
        }

        public void Tri_Fusion()
        {

        }
    }
}
