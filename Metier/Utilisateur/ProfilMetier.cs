using BackOnWay.Dtos.Utilisateur;
using BackOnWay.Models;
using BackOnWay.Repository.Utilisateur;

namespace BackOnWay.Metier.Utilisateur
{
    public class ProfilMetier
    {
        private ProfilRepo _repo = new ProfilRepo();

        public UtilProfilDto? GetProfilUtil(int unId)
        {
            Utilisateurs infoProfil = _repo.GetProfilUtil(unId);
            if (infoProfil == null)
            {
                return null;
            }
            else
            {
                UtilProfilDto utilProfil = new UtilProfilDto
                {
                    UtilId = infoProfil.UtilId,
                    UtilNom = infoProfil.UtilNom,
                    UtilPrenom = infoProfil.UtilPrenom,
                    UtilPseudo = infoProfil.UtilPseudo,
                    UtilEmail = infoProfil.UtilEmail,
                    UtilNaissance = infoProfil.UtilNaissance,
                    UtilGenre = infoProfil.UtilGenre,
                    UtilCredit = infoProfil.UtilCredit,
                    UtilTelephone = infoProfil.UtilTelephone

                };

                return utilProfil;
            }
        }

        public int UpdateProfil(UtilProfilDto utilProfil)
        {
            int reponse;

            Utilisateurs infoUtil = new Utilisateurs
            {
                UtilId = utilProfil.UtilId,
                UtilNom = utilProfil.UtilNom,
                UtilPrenom = utilProfil.UtilPrenom,
                UtilPseudo = utilProfil.UtilPseudo,
                UtilEmail = utilProfil.UtilEmail,
                UtilNaissance = utilProfil.UtilNaissance,
                UtilTelephone = utilProfil.UtilTelephone,
                UtilGenre = utilProfil.UtilGenre
            };

            Utilisateurs util = _repo.GetProfilUtil(infoUtil.UtilId);
            int emailUnique = _repo.CheckExistEmail(infoUtil.UtilEmail);

            if (util.UtilEmail != utilProfil.UtilEmail && emailUnique >= 1)
            {
                //Email déjà pris 
                reponse = 0;
            }
            else
            {
                int resultat = _repo.UpdateProfil(infoUtil);
                if (resultat == 0)
                {

                    //Erreur de mise à jour 
                    reponse = 1;
                }
                else
                {
                    reponse = 2;
                }
            }
            return reponse;
        }

        public int UpdateMdpUtil(UpdateMdpUtilDto mdpUtil)
        {
            int reponse;
            string oldMdpUtil = _repo.GetOldMdpUtil(mdpUtil.UtilId);
            bool oldMdpValide = BCrypt.Net.BCrypt.Verify(mdpUtil.OldMdp, oldMdpUtil);

            if (!oldMdpValide)
            {
                //Mot de passe ne correspond pas 
                reponse = 0;
            }
            else
            {
                string newMdp = BCrypt.Net.BCrypt.HashPassword(mdpUtil.UtilMdp, 12);
                Utilisateurs newUtilMdp = new Utilisateurs
                {
                    UtilId = mdpUtil.UtilId,
                    UtilMdp = newMdp,
                };
                int resultat = _repo.UpdateMdpUtil(newUtilMdp);
                if (resultat == 0)
                {
                    //Erreur lors de la mise à jour du mot de passe !
                    reponse = 1;
                }
                else
                {
                    // Mise à jour réussie !
                    reponse = 2;
                }
            }
            return reponse;
        }
        public bool DeleteProfilUtil(int unId)
        {
            int resultat = _repo.DeleteProfilUtil(unId);
            bool reponse;

            if (resultat == 0)
            {
                reponse = false;
            }
            else
            {
                reponse = true;
            }

            return reponse;
        }
    }
}
