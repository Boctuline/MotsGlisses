using System;

namespace MotsGlisses
{
	public class Joueur
	{
		//Attributs
		string nom;
        int score;
        List<string> motsTrouves = new List<string>();


		//Constructeur
		public Joueur (string nom)
		{
			this.nom = nom;
			score = 0;
			motsTrouves = null;
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
		public bool Contient(string mot)
		{
			return motsTrouves.Contient(mot);
		}
	}
}
