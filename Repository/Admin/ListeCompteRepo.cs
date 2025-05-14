// Importation des espaces de noms nécessaires pour gérer les DTO, les modèles de données, 
// la connexion à la base de données, ainsi que les interactions avec SQL Server.
using BackOnWay.Dtos.Admin;
using BackOnWay.Models;
using BackOnWay.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Admin
{
    // La classe ListeCompteRepo est responsable de la gestion des comptes utilisateurs dans l'application.
    // Elle interagit directement avec la base de données pour récupérer, insérer, modifier et supprimer des utilisateurs.
    public class ListeCompteRepo
    {
        // Déclaration d'un champ privé _connexion pour stocker l'objet de connexion à la base de données.
        private IDbConnection _connexion;

        // Déclaration d'un champ privé _connectionFactory pour faciliter la création de connexions à la base de données.
        private readonly IDbConnectionFactory _connectionFactory;

        // Le constructeur de la classe ListeCompteRepo prend en paramètre une instance d'IDbConnectionFactory.
        // Cela permet d'injecter une dépendance et de garantir que la création des connexions est flexible et testable.
        public ListeCompteRepo(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        // Méthode privée pour établir une connexion à la base de données si elle n'est pas encore ouverte.
        private void DbConnecter()
        {
            _connexion = _connectionFactory.CreateConnection(); // Crée une nouvelle connexion via la factory.
        }

        // Méthode pour récupérer la liste des comptes utilisateurs depuis la base de données.
        public List<Utilisateurs> ListeCompte()
        {
            // Si la connexion est fermée ou n'existe pas, on la crée.
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            // Création d'une liste vide pour stocker les utilisateurs récupérés depuis la base de données.
            List<Utilisateurs> listeUtils = new List<Utilisateurs>();

            // Création d'une commande SQL pour exécuter une requête SELECT.
            SqlCommand cmd = (SqlCommand)_connexion.CreateCommand();

            // Définition de la commande SQL à exécuter : récupère les informations des utilisateurs et des rôles associés.
            cmd.CommandText = "SELECT UtilId, UtilPseudo, UtilNom, UtilSuspendu, UtilEmail, RoleLabel, RoleId FROM utilisateurs INNER JOIN roles ON UtilRole = RoleId";

            // Exécution de la commande SQL et obtention des résultats sous forme d'un SqlDataReader.
            SqlDataReader utils = cmd.ExecuteReader();

            // Parcours de chaque ligne de résultats.
            while (utils.Read())
            {
                // Création d'un objet Utilisateurs et peuplement de ses propriétés à partir des données lues dans le reader.
                Utilisateurs unUtil = new Utilisateurs
                {
                    UtilId = (int)utils["UtilId"],
                    UtilNom = utils["UtilNom"].ToString(),
                    UtilEmail = utils["UtilEmail"].ToString(),
                    UtilPseudo = utils["UtilPseudo"].ToString(),
                    UtilSuspendu = (bool)utils["UtilSuspendu"],
                    // Création et peuplement de l'objet Roles pour chaque utilisateur.
                    Roles = new Roles
                    {
                        RoleId = (int)utils["RoleId"],
                        RoleLabel = utils["RoleLabel"].ToString(),
                    }
                };

                // Ajout de l'utilisateur à la liste des utilisateurs.
                listeUtils.Add(unUtil);
            }

            // Fermeture du SqlDataReader une fois la lecture terminée.
            utils.Close();
            // Fermeture de la connexion à la base de données.
            _connexion.Close();

            // Retourne la liste des utilisateurs récupérés.
            return listeUtils;
        }

        // Méthode pour récupérer un seul compte utilisateur à partir de son identifiant.
        public Utilisateurs SelectCompte(int unId)
        {
            // Si la connexion est fermée ou n'existe pas, on la crée.
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            // Création d'une commande SQL pour récupérer un utilisateur en fonction de son identifiant.
            SqlCommand cmd = (SqlCommand)_connexion.CreateCommand();

            // Définition de la commande SQL pour récupérer un utilisateur spécifique.
            cmd.CommandText = "SELECT UtilId, UtilPseudo, UtilNom, UtilEmail, RoleLabel, RoleId FROM utilisateurs INNER JOIN roles ON UtilRole = RoleId WHERE UtilId = @UtilId";

            // Paramétrage de l'ID utilisateur dans la requête SQL pour éviter les injections SQL.
            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);
            UtilId.Value = unId;

            // Exécution de la commande SQL et récupération des résultats dans un SqlDataReader.
            SqlDataReader reader = cmd.ExecuteReader();

            // Déclaration d'une variable pour stocker le résultat.
            Utilisateurs resultat = null;

            // Si un utilisateur est trouvé (une ligne est lue), on le crée et on le peuple avec les données.
            if (reader.Read())
            {
                resultat = new Utilisateurs
                {
                    UtilId = (int)reader["UtilId"],
                    UtilNom = reader["UtilNom"].ToString(),
                    UtilEmail = reader["UtilEmail"].ToString(),
                    UtilPseudo = reader["UtilPseudo"].ToString(),
                    // Peuplement des informations du rôle de l'utilisateur.
                    Roles = new Roles
                    {
                        RoleId = (int)reader["RoleId"],
                        RoleLabel = reader["RoleLabel"].ToString(),
                    }
                };
            }

            // Fermeture du SqlDataReader une fois la lecture terminée.
            reader.Close();

            // Fermeture de la connexion à la base de données.
            _connexion.Close();

            // Retourne le compte utilisateur trouvé ou null si aucun résultat.
            return resultat;
        }

        // Méthode pour mettre à jour le statut (suspendu ou non) d'un utilisateur.
        public int UpdateStatut(Utilisateurs unUtil)
        {
            // Si la connexion est fermée ou n'existe pas, on la crée.
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            // Création de la commande SQL pour mettre à jour le statut de l'utilisateur.
            SqlCommand cmd = (SqlCommand)_connexion.CreateCommand();

            // Définition de la commande SQL pour mettre à jour le statut.
            cmd.CommandText = "UPDATE utilisateurs SET UtilSuspendu = @Suspendu WHERE UtilId = @UtilId";

            // Paramétrage des paramètres dans la requête SQL.
            SqlParameter suspendu = cmd.Parameters.Add("@Suspendu", SqlDbType.Bit);
            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            suspendu.Value = unUtil.UtilSuspendu;
            UtilId.Value = unUtil.UtilId;

            // Exécution de la commande SQL et récupération du nombre de lignes affectées (1 si l'utilisateur a été mis à jour).
            int resultat = cmd.ExecuteNonQuery();

            // Fermeture de la connexion à la base de données.
            _connexion.Close();

            // Retourne le nombre de lignes affectées par la mise à jour (1 si tout s'est bien passé).
            return resultat;
        }

        // Méthode pour supprimer un utilisateur de la base de données en fonction de son ID.
        public int DeleteCompte(int unId)
        {
            // Si la connexion est fermée ou n'existe pas, on la crée.
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            // Création de la commande SQL pour supprimer l'utilisateur.
            SqlCommand cmd = (SqlCommand)_connexion.CreateCommand();

            // Définition de la commande SQL pour supprimer un utilisateur.
            cmd.CommandText = "DELETE FROM utilisateurs WHERE UtilId = @UtilId AND UtilRole = @UtilRole";

            // Paramétrage des paramètres dans la requête SQL.
            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);
            SqlParameter UtilRole = cmd.Parameters.Add("UtilRole", SqlDbType.Int);

            UtilId.Value = unId;
            UtilRole.Value = 3; // Le rôle de l'utilisateur est 3, ce qui signifie "employé" dans cette logique.

            // Exécution de la commande SQL et récupération du nombre de lignes affectées (1 si l'utilisateur a été supprimé).
            int resultat = cmd.ExecuteNonQuery();

            // Fermeture de la connexion à la base de données.
            _connexion.Close();

            // Retourne le nombre de lignes affectées par la suppression (1 si l'utilisateur a été supprimé).
            return resultat;
        }

        // Méthode pour vérifier si un utilisateur avec un email spécifique existe dans la base de données.
        public int CheckExistEmploye(string unEmail)
        {
            // Si la connexion est fermée ou n'existe pas, on la crée.
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            // Création de la commande SQL pour vérifier l'existence d'un utilisateur avec l'email fourni.
            SqlCommand cmd = (SqlCommand)_connexion.CreateCommand();

            // Définition de la commande SQL pour compter le nombre d'utilisateurs avec l'email donné.
            cmd.CommandText = "SELECT COUNT(*) FROM utilisateurs WHERE UtilEmail = @UtilEmail";

            // Paramétrage de l'email dans la requête SQL.
            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);
            UtilEmail.Value = unEmail;

            // Exécution de la commande SQL et récupération du résultat sous forme d'un entier.
            int resultat = (int)cmd.ExecuteScalar();

            // Fermeture de la connexion à la base de données.
            _connexion.Close();

            // Retourne le nombre d'utilisateurs trouvés (0 ou 1).
            return resultat;
        }

        // Méthode pour insérer un nouvel employé dans la base de données.
        public int InsertEmploye(AddEmployeDto unEmploye)
        {
            // Si la connexion est fermée ou n'existe pas, on la crée.
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            // Création de la commande SQL pour insérer un nouvel employé dans la base de données.
            SqlCommand cmd = (SqlCommand)_connexion.CreateCommand();

            // Définition de la commande SQL pour insérer un nouvel utilisateur.
            cmd.CommandText = "INSERT INTO utilisateurs (UtilNom, UtilPrenom, UtilPseudo, UtilNaissance, UtilCredit, UtilEmail, UtilTelephone, UtilMdp, UtilGenre, UtilRole) VALUES (@UtilNom, @UtilPrenom, @UtilPseudo, @UtilNaissance, @UtilCredit, @UtilEmail, @UtilTelephone, @UtilMdp, @UtilGenre, @UtilRole)";

            // Paramétrage des paramètres dans la requête SQL avec les données de l'objet AddEmployeDto.
            SqlParameter UtilNom = cmd.Parameters.Add("@UtilNom", SqlDbType.VarChar);
            SqlParameter UtilPrenom = cmd.Parameters.Add("@UtilPrenom", SqlDbType.VarChar);
            SqlParameter UtilPseudo = cmd.Parameters.Add("@UtilPseudo", SqlDbType.VarChar);
            SqlParameter UtilNaissance = cmd.Parameters.Add("@UtilNaissance", SqlDbType.Date);
            SqlParameter UtilCredit = cmd.Parameters.Add("@UtilCredit", SqlDbType.SmallInt);
            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);
            SqlParameter UtilTelephone = cmd.Parameters.Add("@UtilTelephone", SqlDbType.VarChar);
            SqlParameter UtilMdp = cmd.Parameters.Add("@UtilMdp", SqlDbType.VarChar);
            SqlParameter UtilGenre = cmd.Parameters.Add("@UtilGenre", SqlDbType.Char);
            SqlParameter UtilRole = cmd.Parameters.Add("@UtilRole", SqlDbType.Int);

            // Attribution des valeurs des paramètres avec les données de l'objet unEmploye.
            UtilNom.Value = unEmploye.UtilNom;
            UtilPrenom.Value = unEmploye.UtilPrenom;
            UtilPseudo.Value = "Employé";
            UtilNaissance.Value = unEmploye.UtilNaissance;
            UtilCredit.Value = 0;
            UtilEmail.Value = unEmploye.UtilEmail;
            UtilTelephone.Value = "0000000000";
            UtilMdp.Value = unEmploye.UtilMdp;
            UtilGenre.Value = unEmploye.UtilGenre;
            UtilRole.Value = 3; // Le rôle de l'employé est 3 (Employé).

            // Exécution de la commande SQL et récupération du nombre de lignes affectées (1 si l'insertion a réussi).
            int resultat = cmd.ExecuteNonQuery();

            // Fermeture de la connexion à la base de données.
            _connexion.Close();

            // Retourne le nombre de lignes affectées par l'insertion (1 si l'employé a été ajouté).
            return resultat;
        }
    }
}
