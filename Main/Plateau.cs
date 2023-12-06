using System;

namespace MotsGlisses
{
    public class Plateau
    {
        char[,] plateau;
        //Constructeur

        /// <summary>
        /// Création de plateau aléatoire selon Lettre.txt
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
        /// Génération d'un plateau à partir d'un fichier de plateau en CSV
        /// </summary>
        /// <param name="namefile">Nom du fichier CSV</param>
        public Plateau(string namefile)
        {
            try
            {
                string[] lines = File.ReadAllLines(namefile);
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
        /// <summary>
        /// Fonction permettant d'écrire un fichier à partir d'un plateau
        /// </summary>
        /// <param name="nomfile"></param>
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
        /// <summary>
        /// Fonction permettant de lire un fichier comportant un plateau
        /// </summary>
        /// <param name="nomfile"></param>
        public void ToRead(string nomfile)
        {
            try
            {
                string[] lines = File.ReadAllLines(nomfile);
                foreach(string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        public bool Recherche_Mot(string mot)
        {
            int i,j = 0;
            bool ret = false;
            for(int k = 0; k < plateau.GetLength(1); k++)
            {

                if (plateau[plateau.GetLength(0) - 1, k] == mot[0])
                {
                    ret = true;
                    i = 2*plateau.GetLength(0)-1;
                    j = plateau.GetLength(1)+k;
                    for(int l = 1; l < mot.Length && ret == true; l++)
                    {
                        if (plateau[i%plateau.GetLength(0), (j - 1) % plateau.GetLength(1)] != mot[l] && plateau[i % plateau.GetLength(0), (j + 1) % plateau.GetLength(1)] != mot[l] && plateau[(i - 1) % plateau.GetLength(0), j%plateau.GetLength(1)] != mot[l] && plateau[(i + 1) % plateau.GetLength(0), j % plateau.GetLength(1)] != mot[l])
                        {
                            ret = false;
                        }
                        else if (plateau[i% plateau.GetLength(0), (j - 1) % plateau.GetLength(1)] == mot[l]) j--;
                        else if (plateau[i% plateau.GetLength(0), (j + 1) % plateau.GetLength(1)] == mot[l]) j++;
                        else if (plateau[(i - 1)% plateau.GetLength(0), j % plateau.GetLength(1)] == mot[l]) i--;
                        else if (plateau[(i  + 1)% plateau.GetLength(0), j % plateau.GetLength(1)] == mot[l]) i++;
                    }
                }
            }
            return ret;
        }
        public void Maj_Plateau(object obj)
        {
            
        }
    }
}