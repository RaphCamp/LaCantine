using LaCantine.Model;
using System;
using System.Collections.Generic;

public class Utilisateur
{
    
    public int Id { get => Id; set => Id = value; }
    public string Mail { get => Mail; set => Mail = value; }
    public string NumTel { get => NumTel; set => NumTel = value; }
    public string Password { get => Password; set => Password = value; }
    public string Login { get => Login; set => Login = value; }
    public double Solde { get => Solde; set => Solde = value; }
    public string Nom { get => Nom; set => Nom = value; }
    public string Prenom { get => Prenom; set => Prenom = value; }
    public DateTime DateDeNaissance { get => DateDeNaissance; set => DateDeNaissance = value; }
    public List<Commandes> LesCommandes { get => LesCommandes; set => LesCommandes = value; }

    public Utilisateur()
	{
		
	}
}
