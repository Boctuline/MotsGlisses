using System;

namespace MotsGlisses
{
    public class Plateau
    {
        char[,] plateau;
        //Constructeur

        /// <summary>
        /// Création de plateau aléatoire
        /// </summary>
        /// <param name="n"></param>
        /// <param name="m"></param>
        public Plateau(int n = 8, int m = 8)
        {

            plateau = new char[n, m];
            List<char> list = new List<char>();
            try
            {
                StreamReader sr = new StreamReader("~Fichier\Lettre.txt");
                string[] line = sr.ReadLine().Split(";");
                while (line != null)
                {
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < m; j++)
                        {
                        }
                    }
                    line = sr.ReadLine().Split(";");
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
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
                Console.WriteLine();
            }
            return null;
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
                StreamReader sr = new StreamReader(nomfile);
                string[] line = sr.ReadLine().Split(";");
                while (line != null)
                {
                    for (int i = 0; i < plateau.GetLength(0); i++)
                    {
                        for (int j = 0; j < plateau.GetLength(1); j++)
                        {
                            plateau[i, j] = line[j];
                        }
                    }line = sr.ReadLine().Split(";") ;
                }
                sr.Close();
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