using BackOnWay.Config;
using Microsoft.Data.SqlClient;

namespace BackOnWay
{
    public class Connexion
    {

        private string ConnexionString = new Settings().GetDbString();

        public SqlConnection GetConnection()
        {
            SqlConnection connexion = new SqlConnection(ConnexionString);

            connexion.Open();

            return connexion;
        }
    }
}
