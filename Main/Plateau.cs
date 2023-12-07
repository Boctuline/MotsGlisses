using System;
using System.Net.Http.Headers;

namespace MotsGlisses
{
    /// <summary>
    /// Classe permettant de définir un tableau, de l'afficher, et de le modifier.
    /// </summary>
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
            List<char> lettresOcc = new List<char>(26);
            try
            {
                string[] lignes = File.ReadAllLines("..\\..\\..\\Main\\Fichiers\\Lettre.txt");
                foreach (string ligne in lignes)
                {
                    for (int i = 0; i < int.Parse(ligne.Split(",")[1]); i++)
                    {
                        lettres.Add(char.Parse(ligne.Split(",")[0].ToLower()));
                        lettresOcc.Add(char.Parse(ligne.Split(",")[0].ToLower()));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Random r = new Random();
            char lettre;
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    lettre = lettres[r.Next(lettres.Count)];
                    if (!lettresOcc.Contains(lettre)) while (lettres.Contains(lettre)) lettres.Remove(lettre);
                    plateau[i, j] = lettre;
                    lettresOcc.Remove(plateau[i, j]);
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
        /// <param name="nomfile">Nom du fichier</param>
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
        /// <param name="nomfile">Nom du fichier</param>
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
        /// <summary>
        /// Fonction permettant de chercher si une entrée string est dans le plateau de manière adjacente
        /// </summary>
        /// <param name="mot">Mot à chercher</param>
        /// <returns></returns>
        public bool Recherche_Mot(string mot)
        {
            for(int k = 0; k < plateau.GetLength(1); k++)
            {

                if (plateau[plateau.GetLength(0) - 1, k] == mot[0])
                {
                    if (Recherche_Adj(mot, 2 * plateau.GetLength(0) - 1, plateau.GetLength(1) + k)) return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Permet de trouver si le kème charactère d'une entrée string se trouve autour de la position (i,j)
        /// </summary>
        /// <param name="mot">Mot à utiliser</param>
        /// <param name="i">Position (ligne)</param>
        /// <param name="j">Position (colonne)</param>
        /// <param name="k">Position de la lettre dans le mot à chercher</param>
        /// <returns></returns>
        public bool Recherche_Adj(string mot, int i, int j, int k = 1, int key = -1)
        {
                if (k == mot.Length) return true;
            if (plateau[i % plateau.GetLength(0), (j - 1) % plateau.GetLength(1)] != mot[k] && plateau[i % plateau.GetLength(0), (j + 1) % plateau.GetLength(1)] != mot[k] && plateau[(i - 1) % plateau.GetLength(0), j % plateau.GetLength(1)] != mot[k] && plateau[(i + 1) % plateau.GetLength(0), j % plateau.GetLength(1)] != mot[k])
            {
                return false;
            }
            if (plateau[i % plateau.GetLength(0), (j - 1) % plateau.GetLength(1)] == mot[k] && key !=1) if (Recherche_Adj(mot, i, j - 1, k + 1, 0)) return true;
            if (plateau[i % plateau.GetLength(0), (j + 1) % plateau.GetLength(1)] == mot[k] && key != 0) if (Recherche_Adj(mot, i, j + 1, k + 1, 1)) return true;
            if (plateau[(i - 1) % plateau.GetLength(0), j % plateau.GetLength(1)] == mot[k] && key != 3) if (Recherche_Adj(mot, i - 1, j, k + 1, 2)) return true;
            if (plateau[(i + 1) % plateau.GetLength(0), j % plateau.GetLength(1)] == mot[k] && key != 2) if (Recherche_Adj(mot, i + 1, j, k + 1, 3)) return true;
            return false;
        }
        public void Maj_Plateau(object obj)
        {
            
        }
    }
}