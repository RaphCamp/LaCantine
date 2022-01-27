using System;

public class Utilisateur
{
    
    public int Id { get => id; set => id = value; }
    public string Mail { get => mail; set => mail = value; }
    public string NumTel { get => numTel; set => numTel = value; }
    public string Password { get => password; set => password = value; }
    public string Login { get => login; set => login = value; }
    public double Solde { get => solde; set => solde = value; }
    public string Nom { get => nom; set => nom = value; }
    public string Prenom { get => prenom; set => prenom = value; }
    public DateTime DateDeNaissance { get => dateDeNaissance; set => dateDeNaissance = value; }
    public List<Commande> LesCommandes { get => lesCommandes; set => lesCommandes = value; }

    public Utilisateur()
	{
		
	}
}
