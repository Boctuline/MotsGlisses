using System;

namespace MotsGlisses
{
    public class Plateau
    {
        char[,] plateau;
        //Constructeur

        /// <summary>
        /// Cr�ation de plateau al�atoire selon Lettre.txt
        /// </summary>
        /// <param name="n">Nombre de lignes du plateau</param>
        /// <param name="m">Nombre de colonnes du plateau</param>
        public Plateau(int n = 8, int m = 8)
        {

            plateau = new char[n, m];
            List<char> list = new List<char>();
            List<char> lettres = new List<char>(26);
            try
            {
                string[] lignes = File.ReadAllLines("..\\..\\..\\Main\\Fichiers\\Lettre.txt");
                foreach (string ligne in lignes)
                {
                    for(int i = 0; i < int.Parse(ligne.Split(",")[1]); i++)
                    lettres.Add(char.Parse(ligne.Split(",")[0].ToLower()));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Random r = new Random();
            int lettre;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    plateau[i, j] = lettres[r.Next(lettres.Count)];
                }
            }
        }
        /// <summary>
        /// G�n�ration d'un plateau � partir d'un fichier de plateau en CSV
        /// </summary>
        /// <param name="namefile">Nom du fichier CSV</param>
        public Plateau(string namefile)
        {
            ToRead(namefile);
        }
        public string toString()
        {
            string a = "";
            for (int i = 0; i < plateau.GetLength(0); i++)
            {
                for (int j = 0; j < plateau.GetLength(1); j++)
                {
                    a += plateau[i, j] + ";";
                }
                a += "\n";
            }
            return a;
        }
        public void ToFile(string nomfile)
        {
            try
            {
                FileStream fileStream = new FileStream("test.txt", FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fileStream);
                for (int i = 0; i < plateau.GetLength(0); i++)
                {
                    for (int j = 0; j < plateau.GetLength(1); j++)
                    {
                        sw.Write(plateau[i, j] + ";");
                    }
                    sw.WriteLine();
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public void ToRead(string nomfile)
        {
            try
            {
                string[] lines = File.ReadAllLines(nomfile);
                plateau = new char[lines.Length, lines[0].Split(";").Length];
                for (int i = 0; i < plateau.GetLength(0); i++)
                {
                    for (int j = 0; j < plateau.GetLength(1); j++)
                    {
                        plateau[i, j] = char.Parse(lines[i].Split(";")[j]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public object Recherche_Mot(string mot)
        {

            return null;
        }
        public void Maj_Plateau(object obj)
        {
            
        }
    }
}