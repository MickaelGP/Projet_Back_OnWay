using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Auth
{
    public class AuthRepo : IAuthRepo
    {
        private SqlConnection _connexion;

        public AuthRepo()
        {
            DbConnecter();
        }
        private void DbConnecter()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public string CheckMdpByEmailUtil(string unEmail)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilMdp FROM utilisateurs WHERE UtilEmail = @UtilEmail";

            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);

            UtilEmail.Value = unEmail;

            SqlDataReader reader = cmd.ExecuteReader();
            string resultat = "";
            if (reader.Read())
            {
                resultat = reader.GetString(0);
            }
            reader.Close();

            this._connexion.Close();
            
            return resultat;
        }

        public Utilisateurs GetRoleAndUtilId(string unEmail)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilId, RoleLabel FROM utilisateurs " +
                "INNER JOIN roles ON RoleId = UtilRole " +
                "WHERE UtilEmail = @UtilEmail";

            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);

            UtilEmail.Value = unEmail;

            SqlDataReader reader = cmd.ExecuteReader();

            Utilisateurs? unUtilisateur = null;

            while (reader.Read())
            {
                unUtilisateur = new Utilisateurs
                {
                    UtilId = (int)reader["UtilId"],
                    Roles = new Roles()
                    {
                        RoleLabel = reader["RoleLabel"].ToString()
                    }
                };
            }
            reader.Close();

            this._connexion.Close();
            return unUtilisateur;
        }


        //public Utilisateurs Connexion(string unEmail)
        //{
        //    if (_connexion == null || _connexion.State == ConnectionState.Closed)
        //    {
        //        DbConnecter();
        //    }

        //    SqlCommand cmd = _connexion.CreateCommand();

        //    cmd.CommandText = "SELECT UtilMdp FROM utilisateurs WHERE UtilEmail = @UtilEmail";

        //    SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);

        //    UtilEmail.Value = unEmail;

        //    SqlDataReader reader = cmd.ExecuteReader();
        //    string resultat = "";
        //    if (reader.Read())
        //    {
        //        resultat = reader.GetString(0);
        //    }

        //    return resultat;

        //}
    }
}
