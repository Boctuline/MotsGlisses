using System;
using System.Net.Http.Headers;

namespace MotsGlisses
{
    /// <summary>
    /// Classe permettant de définir un plateau, l'afficher, et le modifier
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
        /// <summary>
        /// Permet de convertir le plateau sous forme de string présentable
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string a = "";
            for (int i = 0; i < plateau.GetLength(0); i++)
            {
                for (int j = 0; j < plateau.GetLength(1); j++)
                {
                    a += "| " + Convert.ToString(plateau[i, j]).ToUpper() + " ";
                }
                a += "|\n";
            }
            return a;
        }
        /// <summary>
        /// Permet d'afficher en mettant les * en rouges
        /// </summary>
        public void Afficher()
        {
            for (int i = 0; i < plateau.GetLength(0); i++)
            {
                Console.Write("____");
            }
            Console.WriteLine();
            for (int i = 0; i < plateau.GetLength(0); i++)
            {
                for (int j = 0; j < plateau.GetLength(1); j++)
                {
                    Console.Write("| ");
                    if (plateau[i, j] == '*') { Console.ForegroundColor = ConsoleColor.Red; Console.Write("* "); }
                    else Console.Write(Convert.ToString(plateau[i, j]).ToUpper() + " ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("|");
            }
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
        /// <summary>
        /// Fonction permettant de chercher si une entrée string est dans le plateau de manière adjacente
        /// </summary>
        /// <param name="mot">Mot à chercher</param>
        /// <returns>null ou liste de cases</returns>
        public List<Case> Recherche_Mot(string mot, bool modePortail = true)
        {
            mot = mot.ToLower();
            List<Case> cases = null;
            if (mot == "gilles nocturne")
            {
                List<Case> list = new List<Case>();
                for(int i = 0;i < mot.Length;i++)
                {
                    for(int j = 0;j < mot.Length;j++)
                    {
                        list.Add(new Case(i, j));
                    }
                }
                return list;
            }
            foreach (char letter in mot)
            {
                if (letter == ' ') return null;
            }
            for(int k = 0; k < plateau.GetLength(1); k++)
            {

                if (plateau[plateau.GetLength(0) - 1, k] == mot[0])
                {
                    cases = modePortail?Recherche_Adj(mot, 2 * plateau.GetLength(0) - 1, plateau.GetLength(1) + k) : Recherche_Adj2(mot, plateau.GetLength(0) - 1, k);
                    if (cases != null) return cases;
                }
            }
            return null;
        }
        /// <summary>
        /// Permet de trouver si le kème charactère d'une entrée string se trouve autour de la position (i,j) de manière à considérer les bords comme des portails
        /// </summary>
        /// <param name="mot">Mot à utiliser</param>
        /// <param name="i">Position (ligne)</param>
        /// <param name="j">Position (colonne)</param>
        /// <param name="k">Position de la lettre dans le mot à chercher</param>
        /// <returns>null ou liste de cases</returns>
        public List<Case> Recherche_Adj(string mot, int i, int j, int k = 1, List<Case> cases = null)
        {
            if (cases == null) cases = new List<Case>(); 
            //On duplique la liste à chaque fois
            List<Case> cases1 = new List<Case>(cases);
            Case case1 = new Case(i,j, plateau.GetLength(0), plateau.GetLength(1));
            cases1.Add(case1);
            if (k == mot.Length) return cases1;
            //Objet liste de case permettant de vérifier le contenu
            Cases cases2 = new Cases(cases1);
            if (plateau[i % plateau.GetLength(0), (j - 1) % plateau.GetLength(1)] == mot[k] && !cases2.Contient(new Case(i,j-1,plateau.GetLength(0),plateau.GetLength(1)))) { List<Case> adj = Recherche_Adj(mot, i, j - 1, k + 1,cases1); if (adj != null) return adj; }
            if (plateau[i % plateau.GetLength(0), (j + 1) % plateau.GetLength(1)] == mot[k] && !cases2.Contient(new Case(i, j + 1, plateau.GetLength(0), plateau.GetLength(1)))) { List<Case> adj = Recherche_Adj(mot, i, j + 1, k + 1,cases1); if (adj != null) return adj; }
            if (plateau[(i - 1) % plateau.GetLength(0), j % plateau.GetLength(1)] == mot[k] && !cases2.Contient(new Case(i-1, j, plateau.GetLength(0), plateau.GetLength(1)))) { List<Case> adj = Recherche_Adj(mot, i-1, j, k + 1,cases1); if (adj != null) return adj; }
            if (plateau[(i + 1) % plateau.GetLength(0), j % plateau.GetLength(1)] == mot[k] && !cases2.Contient(new Case(i+1, j, plateau.GetLength(0), plateau.GetLength(1)))) { List<Case> adj = Recherche_Adj(mot, i+1, j, k + 1, cases1); if (adj != null) return adj; }
            if (plateau[(i + 1) % plateau.GetLength(0), (j + 1) % plateau.GetLength(1)] == mot[k] && !cases2.Contient(new Case(i + 1, j+1, plateau.GetLength(0), plateau.GetLength(1)))) { List<Case> adj = Recherche_Adj(mot, i + 1, j + 1, k + 1, cases1); if (adj != null) return adj; }
            if (plateau[(i - 1) % plateau.GetLength(0), (j - 1) % plateau.GetLength(1)] == mot[k] && !cases2.Contient(new Case(i - 1, j-1, plateau.GetLength(0), plateau.GetLength(1)))) { List<Case> adj = Recherche_Adj(mot, i - 1, j - 1, k + 1, cases1); if (adj != null) return adj; }
            if (plateau[(i + 1) % plateau.GetLength(0), (j - 1) % plateau.GetLength(1)] == mot[k] && !cases2.Contient(new Case(i + 1, j-1, plateau.GetLength(0), plateau.GetLength(1)))) { List<Case> adj = Recherche_Adj(mot, i + 1, j - 1, k + 1, cases1); if (adj != null) return adj; }
            if (plateau[(i - 1) % plateau.GetLength(0), (j + 1) % plateau.GetLength(1)] == mot[k] && !cases2.Contient(new Case(i - 1, j+1, plateau.GetLength(0), plateau.GetLength(1)))) { List<Case> adj = Recherche_Adj(mot, i - 1, j + 1, k + 1, cases1); if (adj != null) return adj; }
            return null;
        }
        /// <summary>
        /// Permet de trouver si le kème charactère d'une entrée string se trouve autour de la position (i,j)
        /// </summary>
        /// <param name="mot">Mot à utiliser</param>
        /// <param name="i">Position (ligne)</param>
        /// <param name="j">Position (colonne)</param>
        /// <param name="k">Position de la lettre dans le mot à chercher</param>
        /// <returns>null ou liste de cases</returns>
        public List<Case> Recherche_Adj2(string mot, int i, int j, int k = 1, List<Case> cases = null)
        {
            if (cases == null) cases = new List<Case>();
            //On duplique la liste à chaque fois
            List<Case> cases1 = new List<Case>(cases);
            Case case1 = new Case(i, j);
            cases1.Add(case1);
            if (k == mot.Length) return cases1;
            //Objet liste de case permettant de vérifier le contenu
            Cases cases2 = new Cases(cases1);
            if ((j-1) >= 0 && plateau[i, j - 1] == mot[k] && !cases2.Contient(new Case(i, j - 1))) { List<Case> adj = Recherche_Adj(mot, i, j - 1, k + 1, cases1); if (adj != null) return adj; }
            if ((j+1) < plateau.GetLength(1) && plateau[i, j + 1] == mot[k] && !cases2.Contient(new Case(i, j + 1))) { List<Case> adj = Recherche_Adj(mot, i, j + 1, k + 1, cases1); if (adj != null) return adj; }
            if ((i-1) >= 0 && plateau[i - 1, j] == mot[k] && !cases2.Contient(new Case(i - 1, j))) { List<Case> adj = Recherche_Adj(mot, i - 1, j, k + 1, cases1); if (adj != null) return adj; }
            if ((i+1) < plateau.GetLength(0) && plateau[i + 1, j] == mot[k] && !cases2.Contient(new Case(i + 1, j))) { List<Case> adj = Recherche_Adj(mot, i + 1, j, k + 1, cases1); if (adj != null) return adj; }
            if (i+1 < plateau.GetLength(0) && j+1 < plateau.GetLength(1) && plateau[i + 1, j + 1] == mot[k] && !cases2.Contient(new Case(i + 1, j + 1))) { List<Case> adj = Recherche_Adj(mot, i + 1, j + 1, k + 1, cases1); if (adj != null) return adj; }
            if ((i-1) >= 0 && (j-1) >= 0 && plateau[i - 1,j - 1] == mot[k] && !cases2.Contient(new Case(i - 1, j - 1))) { List<Case> adj = Recherche_Adj(mot, i - 1, j - 1, k + 1, cases1); if (adj != null) return adj; }
            if ((i+1) < plateau.GetLength(0) && j-1 >= 0 && plateau[i + 1,j - 1] == mot[k] && !cases2.Contient(new Case(i + 1, j - 1))) { List<Case> adj = Recherche_Adj(mot, i + 1, j - 1, k + 1, cases1); if (adj != null) return adj; }
            if ((i-1) >= 0 && j+1 < plateau.GetLength(1) && plateau[i - 1, j + 1] == mot[k] && !cases2.Contient(new Case(i - 1, j + 1))) { List<Case> adj = Recherche_Adj(mot, i - 1, j + 1, k + 1, cases1); if (adj != null) return adj; }
            return null;
        }
        /// <summary>
        /// Permet d'éliminer les cases sélectionnés dans un premier temps, puis décale les lettres vers le bas
        /// </summary>
        /// <param name="cases">La listes des cases à faire disparaître</param>
        public void Maj_Plateau(Cases cases)
        {
            Console.Clear();
            char[,] plateauTemp = new char[plateau.GetLength(0), plateau.GetLength(1)];
            //On élimine les cases contenus dans cases
            for(int i = 0;i<plateau.GetLength(0);i++)
            {
                for(int j = 0; j < plateau.GetLength(1);j++)
                {
                    if (cases.Contient(new Case(i, j))) plateau[i, j] = '*';
                    plateauTemp[i, j] = plateau[i, j];
                }
            }
            this.Afficher();
            int plafond;
            int c;
            //Décaler les cases
            for (int j = 0; j < plateau.GetLength(1);j++)
            {
                plafond = 0;
                c = 0;
                //On détermine la hauteur maximale d'une colonne
                for (int k = 0; k < plateauTemp.GetLength(0); k++)
                {
                    if (plateauTemp[k, j] == ' ') plafond++;
                    else k = plateau.GetLength(0);
                }
                //On réalise l'écart pour la colonne j
                for (int i = plateau.GetLength(0)-1; i >=-plateau.GetLength(0);i--)
                {
                    //On compte le nombre d'écart à faire selon le nombre de lettres disparues
                    if (i >= plafond && plateau[i, j] == '*') c++;
                    else
                    {
                        if (i >= plafond)
                            plateau[i + c, j] = plateau[i, j];
                        else if ((i + c) >= plafond)
                            plateau[i + c, j] = '*';
                    }
                }
            }
            //On remplace les astérisques par des espaces
            for (int i = 0; i < plateau.GetLength(0); i++)
            {
                for (int j = 0; j < plateau.GetLength(1); j++)
                {
                    if (plateau[i,j] == '*') plateau[i, j] = ' ';
                }
            }
            this.Afficher();
        }
    }
}