using System;

namespace MotsGlisses
{
    class Program
    {
        public static void Main(string[] args)
        {
            Dictionnaire monDictionnaire = new Dictionnaire("Mots_Français.txt");
            List<List<string>> dico = monDictionnaire.Dico;
        }
    }
}
