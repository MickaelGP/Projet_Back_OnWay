using BackOnWay.Dtos.Admin; // Importation des DTOs utilisés pour transférer les données d'utilisateur
using BackOnWay.Models; // Importation des modèles qui correspondent à la structure de données de la BDD
using BackOnWay.Repository.Admin; // Importation du repository qui contient les méthodes pour accéder à la base de données
using BackOnWay.Utils; // Importation des utilitaires pour la connexion à la base de données

namespace BackOnWay.Metier.Admin
{
    // La classe ListeCompteMetier contient la logique métier pour gérer les comptes utilisateurs et employés
    public class ListeCompteMetier
    {
        // Déclaration du repository qui va être utilisé pour accéder aux données
        private readonly ListeCompteRepo _repository;

        // Constructeur qui prend une fabrique de connexion à la base de données (interface IDbConnectionFactory)
        // et crée un objet ListeCompteRepo pour interagir avec la base de données
        public ListeCompteMetier(IDbConnectionFactory factory)
        {
            _repository = new ListeCompteRepo(factory); // Initialisation du repository avec la factory pour créer la connexion
        }

        /// <summary>
        /// Méthode pour récupérer la liste de tous les comptes utilisateurs
        /// </summary>
        /// <returns>Liste d'objets UtilisateurDto représentant les utilisateurs</returns>
        public List<UtilisateurDto> GetListeCompte()
        {
            // Appel au repository pour récupérer la liste des utilisateurs depuis la base de données
            List<Utilisateurs> utilisateurs = _repository.ListeCompte();

            // Création d'une liste de DTOs qui vont contenir les données formatées à renvoyer
            List<UtilisateurDto> listeUtilisateurs = new List<UtilisateurDto>();

            // Pour chaque utilisateur récupéré, on mappe les informations dans un DTO
            foreach (var util in utilisateurs)
            {
                UtilisateurDto utilDto = new UtilisateurDto
                {
                    UtilId = util.UtilId, // Récupération de l'ID de l'utilisateur
                    UtilNom = util.UtilNom, // Récupération du nom de l'utilisateur
                    UtilEmail = util.UtilEmail, // Récupération de l'email de l'utilisateur
                    UtilPseudo = util.UtilPseudo, // Récupération du pseudo de l'utilisateur
                    UtilSuspendu = util.UtilSuspendu, // Récupération de l'état suspendu de l'utilisateur
                    RoleLabel = util.Roles?.RoleLabel // Récupération du label du rôle de l'utilisateur
                };

                // Ajout du DTO à la liste des utilisateurs
                listeUtilisateurs.Add(utilDto);
            }

            // Retourne la liste des utilisateurs sous forme de DTOs
            return listeUtilisateurs;
        }

        /// <summary>
        /// Méthode pour récupérer les informations d'un compte utilisateur spécifique
        /// </summary>
        /// <param name="unId">ID de l'utilisateur dont on veut obtenir les informations</param>
        /// <returns>Un DTO représentant les informations de l'utilisateur, ou null si l'utilisateur n'existe pas</returns>
        public UtilisateurDto InfoCompte(int unId)
        {
            // Appel au repository pour récupérer les informations d'un utilisateur spécifique
            Utilisateurs compteInfos = _repository.SelectCompte(unId);

            // Si l'utilisateur n'existe pas, on retourne null
            if (compteInfos == null)
            {
                return null;
            }
            else
            {
                // Création du DTO pour mapper les informations de l'utilisateur
                UtilisateurDto unUtilisateur = new UtilisateurDto
                {
                    UtilId = compteInfos.UtilId, // Récupération de l'ID
                    UtilNom = compteInfos.UtilNom, // Récupération du nom
                    UtilEmail = compteInfos.UtilEmail, // Récupération de l'email
                    UtilPseudo = compteInfos.UtilPseudo, // Récupération du pseudo
                    UtilSuspendu = compteInfos.UtilSuspendu, // Récupération de l'état suspendu
                    RoleLabel = compteInfos.Roles?.RoleLabel // Récupération du label du rôle
                };
                return unUtilisateur;
            }
        }

        /// <summary>
        /// Méthode pour mettre à jour le statut (suspendre ou réactiver) d'un utilisateur
        /// </summary>
        /// <param name="unUtil">DTO contenant l'ID et le nouveau statut de l'utilisateur</param>
        /// <returns>True si la modification a réussi, sinon false</returns>
        public bool MajStatut(UpdateUtilDto unUtil)
        {
            // Création d'un modèle Utilisateurs à partir du DTO UpdateUtilDto
            Utilisateurs util = new Utilisateurs
            {
                UtilId = unUtil.UtilId, // Récupération de l'ID de l'utilisateur
                UtilSuspendu = unUtil.UtilSuspendu // Récupération du statut de suspension
            };

            // Appel au repository pour mettre à jour le statut dans la base de données
            int resultatModif = _repository.UpdateStatut(util);

            // Si la modification a réussi (le nombre de lignes affectées est supérieur à 0)
            bool resultat = resultatModif > 0;
            return resultat;
        }

        /// <summary>
        /// Méthode pour supprimer un compte employé
        /// </summary>
        /// <param name="unId">ID de l'employé à supprimer</param>
        /// <returns>True si la suppression a réussi, sinon false</returns>
        public bool SupEmployeCpte(int unId)
        {
            // Appel au repository pour supprimer l'employé
            int resultatSup = _repository.DeleteCompte(unId);

            // Retourne true si la suppression a réussi (le nombre de lignes affectées est supérieur à 0)
            bool resultat = resultatSup > 0;
            return resultat;
        }

        /// <summary>
        /// Méthode pour ajouter un nouvel employé
        /// </summary>
        /// <param name="unEmploye">DTO contenant les informations de l'employé à ajouter</param>
        /// <returns>Un code de résultat : 0 si l'employé existe déjà, 1 si l'ajout a réussi, 2 si une erreur est survenue</returns>
        public int AjoutEmploye(AddEmployeDto unEmploye)
        {
            // Vérification si un employé avec le même email existe déjà
            int existe = CheckExistEmploye(unEmploye.UtilEmail);

            if (existe > 0)
            {
                // Si l'employé existe déjà, on retourne 0
                return 0;
            }
            else
            {
                // Si l'employé n'existe pas, on tente d'ajouter l'employé dans la base de données
                int ajoutEmploye = _repository.InsertEmploye(unEmploye);
                if (ajoutEmploye > 0)
                {
                    // Si l'ajout a réussi, on retourne 1
                    return 1;
                }
                else
                {
                    // Si une erreur est survenue, on retourne 2
                    return 2;
                }
            }
        }

        /// <summary>
        /// Méthode pour vérifier si un employé avec un email donné existe déjà
        /// </summary>
        /// <param name="unEmail">Email de l'employé à vérifier</param>
        /// <returns>Le nombre d'employés avec cet email (0 si l'employé n'existe pas, > 0 si l'employé existe)</returns>
        public int CheckExistEmploye(string unEmail)
        {
            // Appel au repository pour vérifier si l'employé existe dans la base de données
            int existe = _repository.CheckExistEmploye(unEmail);
            return existe;
        }
    }
}
