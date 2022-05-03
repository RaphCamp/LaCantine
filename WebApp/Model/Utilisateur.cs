using LaCantine.Model;
using System;
using System.Collections.Generic;


namespace LaCantine.Model
{
    public class Utilisateur
    {

        public int Id { get; set; }
        public string Mail { get; set; }
        public string NumTel { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public double Solde { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public List<Commandes> LesCommandes { get; set; }


        public void RetirerSolde(Utilisateur utilisateur, double montant)
        {
            Utilisateur user = utilisateur;
            if (montant < user.Solde)
            {
                user.Solde = user.Solde - montant;
            }
        }

        public void AjouterSolde(Utilisateur utilisateur, double montant)
        {
            Utilisateur user = utilisateur;
            user.Solde += montant;
        }
    }
}