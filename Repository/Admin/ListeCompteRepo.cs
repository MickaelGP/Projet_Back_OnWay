using BackOnWay.Dtos.Admin;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Admin
{
    public class ListeCompteRepo : Connexion
    {
        public List<Utilisateurs> ListeCompte()
        {
            DbConnecter();

            List<Utilisateurs> listeUtils = new List<Utilisateurs>();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilId, UtilPseudo, UtilNom, UtilSuspendu, UtilEmail, RoleLabel, RoleId FROM utilisateurs INNER JOIN roles ON UtilRole = RoleId";

            SqlDataReader utils = cmd.ExecuteReader();

            while (utils.Read())
            {
                Utilisateurs unUtil = new Utilisateurs
                {
                    UtilId = (int)utils["UtilId"],
                    UtilNom = utils["UtilNom"].ToString(),
                    UtilEmail = utils["UtilEmail"].ToString(),
                    UtilPseudo = utils["UtilPseudo"].ToString(),
                    UtilSuspendu = (bool)utils["UtilSuspendu"],
                    Roles = new Roles
                    {
                        RoleId = (int)utils["RoleId"],
                        RoleLabel = utils["RoleLabel"].ToString(),
                    }
                };
                listeUtils.Add(unUtil);
            }
           
            DbDeconnecter();

            return listeUtils;
        }

        public Utilisateurs SelectCompte(int unId)
        {
                DbConnecter();
            
            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilId,  UtilPseudo, UtilNom, UtilEmail, RoleLabel, RoleId, UtilSuspendu FROM utilisateurs INNER JOIN roles ON  UtilRole = RoleId  WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            Utilisateurs resultat = null;

            if (reader.Read())
            {
                resultat = new Utilisateurs
                {
                    UtilId = (int)reader["UtilId"],
                    UtilNom = reader["UtilNom"].ToString(),
                    UtilEmail = reader["UtilEmail"].ToString(),
                    UtilPseudo = reader["UtilPseudo"].ToString(),
                    UtilSuspendu = (bool)reader["UtilSuspendu"],
                    Roles = new Roles
                    {
                        RoleId = (int)reader["RoleId"],
                        RoleLabel = reader["RoleLabel"].ToString(),
                    }

                };
            }

            DbDeconnecter();

            return resultat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="unUtil"></param>
        /// <returns></returns>
        public int UpdateStatut(Utilisateurs unUtil)
        {
                DbConnecter();
    
            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE utilisateurs SET UtilSuspendu = @Suspendu WHERE UtilId = @UtilId ";

            SqlParameter suspendu = cmd.Parameters.Add("@Suspendu", SqlDbType.Bit);
            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            suspendu.Value = unUtil.UtilSuspendu;
            UtilId.Value = unUtil.UtilId;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }

        public int DeleteCompte(int unId)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "DELETE FROM utilisateurs WHERE UtilId = @UtilId AND UtilRole = @UtilRole";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);
            SqlParameter UtilRole = cmd.Parameters.Add("UtilRole", SqlDbType.Int);

            UtilId.Value = unId;
            UtilRole.Value = 3;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }

        public int CheckExistEmploye(string unEmail)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT COUNT(*) FROM utilisateurs WHERE UtilEmail = @UtilEmail";

            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);

            UtilEmail.Value = unEmail;

            int resultat = (int)cmd.ExecuteScalar();

            DbDeconnecter();

            return resultat;
        }

        public int InsertEmploye(AddEmployeDto unEmploye)
        {
                DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO utilisateurs (UtilNom, UtilPrenom, UtilPseudo, UtilNaissance, UtilCredit, UtilEmail, UtilTelephone, UtilMdp, UtilGenre, UtilRole) VALUES (@UtilNom, @UtilPrenom, @UtilPseudo, @UtilNaissance, @UtilCredit, @UtilEmail, @UtilTelephone, @UtilMdp, @UtilGenre, @UtilRole) ";

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

            UtilNom.Value = unEmploye.UtilNom;
            UtilPrenom.Value = unEmploye.UtilPrenom;
            UtilPseudo.Value = "Employé";
            UtilNaissance.Value = unEmploye.UtilNaissance;
            UtilCredit.Value = 0;
            UtilEmail.Value = unEmploye.UtilEmail;
            UtilTelephone.Value = "0000000000";
            UtilMdp.Value = unEmploye.UtilMdp;
            UtilGenre.Value = unEmploye.UtilGenre;
            UtilRole.Value = 3;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }
    }
}
