using System;
using System.IO;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace MotsGlisses
{

    public class Dictionnaire
    {
        public Dictionnaire(string nameFile)
        {
            List<List<string>> dictionnaire = new List<List<string>>();
            StreamReader sReader = null;

            try
            {
                sReader = new StreamReader(nameFile);
                string line;
                while ((line = sReader.ReadLine()) != null)
                {
                    List<string> ligne = new List<string>();
                    string[] mots = line.Split(' ');

                    foreach (string mot in mots)
                    {
                        ligne.Add(mot);
                    }
                    dictionnaire.Add(ligne);
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
            
            string result = "La lettre "+a+"contient " +"mots et est en français.";
            return result;
        }
        public bool RechDichRecursif(string mot)
        {
            return false;
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
