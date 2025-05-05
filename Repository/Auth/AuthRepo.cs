using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Auth
{
    public class AuthRepo
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

        public string Connexion(string unEmail)
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

            return resultat;

        }
    }
}
