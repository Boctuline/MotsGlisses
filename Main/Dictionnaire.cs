using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace MotsGlisses
{

    public class Dictionnaire
    {
        List<List<string>> dico;
        string langue;
        public List<List<string>> Dico
        {
            get { return dico; }
            set { this.dico = value; }
        }
        public string Langue
        {
            get { return langue; }
            set { this.langue = value; }
        }
        public Dictionnaire(string nameFile, string langue)
        {
            this.langue = langue;
            dico = new List<List<string>>();
            StreamReader sReader = null;
            try
            {
                sReader = new StreamReader(nameFile);
                string line;
                while ((line = sReader.ReadLine()) != null)
                {
                    List<string> ligne = new List<string>();
                    string[] mots = line.Split(' ');

                    foreach (string mot in mots)
                    {
                        ligne.Add(mot);
                    }
                    dico.Add(ligne);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (sReader != null) { sReader.Close(); }
            }
        }


        // Méthode pour afficher le dictionnaire (nombre de mots par lettre et langue)
        public string toString()
        {
            string description ="";
            foreach (List<string> lettre in dico)
            {
                description+="La lettre : " + lettre[0][0] + " possède " + lettre.Count + " mots";
            }
            description += "\nLe dictionnaire est en " + this.langue;
            return description;
        }


        public void Tri_Fusion_Reccursif(List<string> liste)
        {
            if (liste.Count <= 1)
                return;

            int millieu = liste.Count / 2;
            List<string> gauche = new List<string>();
            List<string> droite = new List<string>();

            for (int i = 0; i < millieu; i++)
                gauche.Add(liste[i]);

            for (int i = millieu; i < liste.Count; i++)
                droite.Add(liste[i]);

            Tri_Fusion_Reccursif(gauche);
            Tri_Fusion_Reccursif(droite);

            Fusion(liste, gauche, droite);
        }

        public void Fusion(List<string> liste, List<string> gauche, List<string> droite)
        {
            int indexGauche = 0, indexDroite = 0, indexResultat = 0;

            while (indexGauche < gauche.Count && indexDroite < droite.Count)
            {
                if (string.Compare(gauche[indexGauche], droite[indexDroite]) < 0)
                {
                    liste[indexResultat] = gauche[indexGauche];
                    indexGauche++;
                }
                else
                {
                    liste[indexResultat] = droite[indexDroite];
                    indexDroite++;
                }
                indexResultat++;
            }

            while (indexGauche < gauche.Count)
            {
                liste[indexResultat] = gauche[indexGauche];
                indexGauche++;
                indexResultat++;
            }

            while (indexDroite < droite.Count)
            {
                liste[indexResultat] = droite[indexDroite];
                indexDroite++;
                indexResultat++;
            }
        }

        public bool RechDichoRecursif(string word)
        {
            foreach (List<string> sousListe in this.dico)
            {
                if (Dichotomie(sousListe, word, 0, sousListe.Count - 1))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Dichotomie(List<string> list, string mot_cherche, int debut, int fin)
        {
            if (fin >= debut)
            {
                int millieu = debut + (fin - debut) / 2;

                if (list[millieu] == mot_cherche)
                    return true;

                if (string.Compare(list[millieu], mot_cherche) > 0)
                    return Dichotomie(list, mot_cherche, debut, millieu - 1);

                return Dichotomie(list, mot_cherche, millieu + 1, fin);
            }

            return false;
        }
    }
}