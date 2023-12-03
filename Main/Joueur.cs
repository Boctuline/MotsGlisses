using System;

namespace MotsGlisses
{
	public class Joueur
	{
		//Attributs
		string nom;
        List<string> motsTrouves = new List<string>();
        int[] scores;


		//Constructeur
		public Joueur (string nom, int[] scores, List<string> motsTrouves)
		{
			this.nom = nom;
			this.scores = scores;
			this.motsTrouves = motsTrouves;
			scores = 0;
			motsTrouves = null;
		}
        public string Nom
        {
            get { return nom; }
            set { this.nom = value; }
        }
        public List<string> MotsTrouves
        {
            get { return motsTrouves; }
            set { this.motsTrouves = value; }
        }
        public int[] Scores
        {
            get { return scores; }
            set { this.scores = value; }
        }
		//Méthodes
		public void Add_Mot (string mot)
		{

		}
		public string toString()
		{
			string a = "Le joueur " + nom + " a un score de " + scores + " avec " + motsTrouves + "comme mots trouvés";
			return a;
		}
		public void Add_Score(int val)
		{
			foreach(int a in scores)
			{
				a = a + val;
			}
		}
		public bool Contient(string mot)
		{
			bool b = false;
			foreach (string s in motsTrouves) 
			{ 
				if(mot == s) {  b = true; break; }
			}
			return b;
		}
	}
}
