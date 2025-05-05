using BackOnWay.Dtos.Visiteur;
using BackOnWay.Models;
using BackOnWay.Repository.Visiteur;
using System.Globalization;

namespace BackOnWay.Metier.Visiteur
{
    public class InscriptionMetier
    {
        private InscriptionRepo _repo = new InscriptionRepo();

        public int CreateCompte(CreateCompteDto unCompte)
        {
            int reponse;

            int existEmail = _repo.CheckExistEmail(unCompte.UtilEmail);

            if (existEmail > 0)
            {
                reponse = 0;
            }
            else
            {
                Utilisateurs newUtilisateur = new Utilisateurs
                {
                    UtilPseudo = unCompte.UtilPseudo,
                    UtilPrenom = "default",
                    UtilNom = "default",
                    UtilNaissance = DateOnly.ParseExact("01/01/0001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ImgChemin = "default",
                    UtilCredit = 20,
                    UtilEmail = unCompte.UtilEmail,
                    UtilTelephone = "0000000000",
                    UtilMdp = unCompte.UtilMdp,
                    UtilSuspendu = false,
                    UtilGenre = unCompte.UtilGenre,
                    UtilRole = 2
                };
                int resultat = _repo.CreateCompte(newUtilisateur);

                if (resultat == 0)
                {
                    reponse = 1;
                }
                else
                {
                    reponse = 2;
                }
            }

            return reponse;
        }
    }
}
