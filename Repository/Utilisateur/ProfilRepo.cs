using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Utilisateur
{
    public class ProfilRepo : Connexion
    {
        public Utilisateurs GetProfilUtil(int unId)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilId, UtilPseudo, UtilPrenom, UtilNom,  UtilNaissance, UtilCredit, UtilEmail, UtilTelephone, UtilGenre FROM utilisateurs WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            Utilisateurs? resultat = null;

            if (reader.Read())
            {
                resultat = new Utilisateurs
                {
                    UtilId = (int)reader["UtilId"],
                    UtilPseudo = reader["UtilPseudo"].ToString(),
                    UtilPrenom = reader["UtilPrenom"].ToString(),
                    UtilNom = reader["UtilNom"].ToString(),
                    UtilNaissance = DateOnly.FromDateTime((DateTime)reader["UtilNaissance"]),
                    UtilCredit = (byte)reader["UtilCredit"],
                    UtilEmail = reader["UtilEmail"].ToString(),
                    UtilTelephone = reader["UtilTelephone"].ToString(),
                    UtilGenre = reader["UtilGenre"].ToString()
                };
            }
            reader.Close();

            DbDeconnecter();

            return resultat;
        }

        public int UpdateProfil(Utilisateurs unProfil)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE utilisateurs SET UtilPseudo = @UtilPseudo, UtilPrenom = @UtilPrenom, UtilNom = @UtilNom,  UtilNaissance = @UtilNaissance,  UtilEmail = @UtilEmail, UtilTelephone = @UtilTelephone, UtilGenre = @UtilGenre WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);
            SqlParameter UtilPseudo = cmd.Parameters.Add("@UtilPseudo", SqlDbType.VarChar);
            SqlParameter UtilPrenom = cmd.Parameters.Add("@UtilPrenom", SqlDbType.VarChar);
            SqlParameter UtilNom = cmd.Parameters.Add("@UtilNom", SqlDbType.VarChar);
            SqlParameter UtilNaissance = cmd.Parameters.Add("@UtilNaissance", SqlDbType.Date);
            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);
            SqlParameter UtilTelephone = cmd.Parameters.Add("@UtilTelephone", SqlDbType.VarChar);
            SqlParameter UtilGenre = cmd.Parameters.Add("@UtilGenre", SqlDbType.Char);

            UtilId.Value = unProfil.UtilId;
            UtilPseudo.Value = unProfil.UtilPseudo;
            UtilPrenom.Value = unProfil.UtilPrenom;
            UtilNom.Value = unProfil.UtilNom;
            UtilNaissance.Value = unProfil.UtilNaissance;
            UtilEmail.Value = unProfil.UtilEmail;
            UtilTelephone.Value = unProfil.UtilTelephone;
            UtilGenre.Value = unProfil.UtilGenre;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }

        public int UpdateMdpUtil(Utilisateurs mdpUtil)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE utilisateurs SET UtilMdp = @UtilMdp WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);
            SqlParameter utilMdp = cmd.Parameters.Add("@UtilMdp", SqlDbType.VarChar);

            UtilId.Value = mdpUtil.UtilId;
            utilMdp.Value = mdpUtil.UtilMdp;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }

        public string GetOldMdpUtil(int unId)
        {
            DbConnecter();

            string resultat = " ";

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilMdp FROM utilisateurs WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                resultat = reader["UtilMdp"].ToString();
            }

            DbDeconnecter();

            return resultat;
        }
        public int DeleteProfilUtil(int unId)
        {
         DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "DELETE FROM utilisateurs WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }
        public int CheckExistEmail(string unEmail)
        {
           DbConnecter() ;

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT COUNT(*) FROM utilisateurs WHERE UtilEmail = @UtilEmail";

            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);

            UtilEmail.Value = unEmail;

            int resultat = (int)cmd.ExecuteScalar();

            DbDeconnecter() ;

            return resultat;
        }
    }
}
