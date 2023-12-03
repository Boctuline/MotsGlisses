using System;

namespace MotsGlisses
{
    public class Plateau
    {
        string[,] plateau;
        //Constructeur
        public Plateau()
        {
            char[,] plateau;
        }
        //Méthodes
        public string toString()
        {
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
                string line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = sr.ReadLine();
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