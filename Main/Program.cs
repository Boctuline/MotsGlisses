using System;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
            Dictionnaire monDictionnaire = new Dictionnaire("Mots_Fran�ais.txt");
            List<List<string>> dico = monDictionnaire.Dico;
        }
    }
}
