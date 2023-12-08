using System;
using System.Security.Cryptography.X509Certificates;

namespace MotsGlisses
{
	public class Joueur
	{
		//Attributs
		string nom;
        int score;
        List<string> motsTrouves;


		//Constructeur
		public Joueur (string nom)
		{
			this.nom = nom;
			score = 0;
			motsTrouves = new List<string>();
		}
        public string Nom
        {
            get { return nom; }
            set { this.nom = value; }
        }
		//Méthodes
		public void Add_Mot (string mot)
		{
			motsTrouves.Add(mot);
		}
		public string toString()
		{
			string a = "Le joueur " + nom + " a " + score + "pts et a trouvé les mots : ";
			a += motsTrouves[0];
			for(int i = 1; i < motsTrouves.Count-1; i++)
			{
				if (i == 1) a += " - ";
				a += motsTrouves[i] + " - ";
			}
			a += motsTrouves[motsTrouves.Count - 1];
			return a;
		}
		public void Add_Score(int val)
		{
			this.score += val;
		}
		public int Score(string mot, string chemin = "..\\..\\..\\Main\\Fichiers\\Lettre.txt")
		{
			string[] lines = File.ReadAllLines(chemin);
			int score = 0;
			for(int i = 0; i < lines.Length; i++)
			{
				foreach(char letter in mot)
				if (lines[i].Split(",")[0] == Convert.ToString(letter).ToUpper()) score+=Convert.ToInt32(lines[i].Split(",")[2]);
			}
			score *= mot.Length;
			return score;
		}
		public bool Contient(string mot)
		{
			return motsTrouves.Contains(mot);
		}
	}
}
