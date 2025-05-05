using BackOnWay.Dtos.Admin;
using BackOnWay.Models;
using BackOnWay.Repository.Admin;

namespace BackOnWay.Metier.Admin
{
    public class ListeCompteMetier
    {
        private ListeCompteRepo _repository = new ListeCompteRepo();

        public List<UtilisateurDto> GetListeCompte()
        {
            List<Utilisateurs> utilisateurs = _repository.ListeCompte();
            List<UtilisateurDto> listeUtilisateurs = new List<UtilisateurDto>();

            foreach (var util in utilisateurs)
            {
                UtilisateurDto utilDto = new UtilisateurDto
                {
                    UtilId = util.UtilId,
                    UtilNom = util.UtilNom,
                    UtilEmail = util.UtilEmail,
                    UtilPseudo = util.UtilPseudo,
                    UtilSuspendu = util.UtilSuspendu,
                    RoleLabel = util.Roles?.RoleLabel
                };
                listeUtilisateurs.Add(utilDto);
            }

            return listeUtilisateurs;
        }
        public UtilisateurDto InfoCompte(int unId)
        {
            Utilisateurs compteInfos = _repository.SelectCompte(unId);
            if (compteInfos == null)
            {
                return null;
            }
            else
            {
                UtilisateurDto unUtilisateur = new UtilisateurDto
                {
                    UtilId = compteInfos.UtilId,
                    UtilNom = compteInfos.UtilNom,
                    UtilEmail = compteInfos.UtilEmail,
                    UtilPseudo = compteInfos.UtilPseudo,
                    UtilSuspendu = compteInfos.UtilSuspendu,
                    RoleLabel = compteInfos.Roles?.RoleLabel
                };
                return unUtilisateur;
            }

        }

        /// <summary>
        /// Méthode pour suspendre un compte utilisateur ou employé
        /// </summary>
        /// <param name="unUtil">Identifiant de l'utilisateur à modifier et valeur pour la suspenssion</param>
        /// <returns>true ou false</returns>
        public bool MajStatut(UpdateUtilDto unUtil)
        {
            Utilisateurs util = new Utilisateurs
            {
                UtilId = unUtil.UtilId,
                UtilSuspendu = unUtil.UtilSuspendu
            };
            int resultatModif = _repository.UpdateStatut(util);

            bool resultat;
            if (resultatModif > 0)
            {
                // "Modification effectuée avec succès !";
                resultat = true;
            }
            else
            {
                // "Une erreur c'est produite lors de la mise à jour du statut de l'utilisateur.";
                resultat = false;
            }
            return resultat;
        }

        public bool SupEmployeCpte(int unId)
        {
            bool resultat;
            int resultatSup = _repository.DeleteCompte(unId);
            if (resultatSup > 0)
            {
                //"Suppression effectuée avec succès !"
                resultat = true;
            }
            else
            {
                //"Une erreur c'est produite lors de la suppression de l'employé."
                resultat = false;
            }
            return resultat;
        }

        public int AjoutEmploye(AddEmployeDto unEmploye)
        {
            int resultat;

            int existe = CheckExistEmploye(unEmploye.UtilEmail);
            if (existe > 0)
            {
                // "L'employé existe déjà"
                resultat = 0;
            }
            else
            {
                int ajoutEmploye = _repository.InsertEmploye(unEmploye);
                if (ajoutEmploye > 0)
                {
                    //"Ajout effectué avec succès !"
                    resultat = 1;
                }
                else
                {
                    //"Une erreur c'est produite lors de l'ajout de l'employé."
                    resultat = 2;
                }
            }
            return resultat;
        }
        public int CheckExistEmploye(string unEmail)
        {
            int existe = _repository.CheckExistEmploye(unEmail);

            return existe;
        }
    }
}
