using System;

namespace MotsGlisses
{
    public class Dictionnaire
    {
        //Attributs
        //Constructeur
        public Dictionnaire()
        {
        }
        //Méthodes
        public int NombreMots(string nomFichier, char lettre)
        {
            try
            {
                int nombreMots = 0;
                using (StreamReader sr = new StreamReader(nomFichier))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] mots = line.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string mot in mots)
                        {
                            if (mot.Length > 0 && char.ToLower(mot[0]) == char.ToLower(lettre))
                            {
                                nombreMots++;
                            }
                        }
                    }
                }
                return nombreMots;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        
        public string toString(char a)
        {
            string a = "La lettre "+ a+ " apparait "+ NombreMots+ " fois."
            return a;
        }
        public bool RechDichRecursif(string mot)
        {
            return false;
        }
        public void Tri_Fusion()
        {

        }
    }
}
