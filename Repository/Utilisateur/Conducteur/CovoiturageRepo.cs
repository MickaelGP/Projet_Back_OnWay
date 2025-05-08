using System.Data;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur.Conducteur
{
    public class CovoiturageRepo : ICovoiturageRepo
    {
        private readonly SqlConnection  _connexion;

        public CovoiturageRepo()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public List<Covoiturages> GetAllCovoitByUtilId(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT Covoitid, CovoitDate, CovoitDep, CovoitArr, dep.AdresseVille AS DepartVille, arr.AdresseVille AS ArriveVille FROM covoiturages " +
                "INNER JOIN adresses AS dep ON DepartAdresse = dep.AdresseId " +
                "INNER JOIN adresses AS arr ON ArriveAdresse = arr.AdresseId " +
                "INNER JOIN voitures ON CovoitVoiture = VoitId " +
                "INNER JOIN conducteurs ON VoitConduc = ConducId " +
                "INNER JOIN utilisateurs ON ConducId = UtilId " +
                "WHERE UtilId = @UtilId AND CovoitStatut = 'En attente'";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Covoiturages> listeCovoiturages = new List<Covoiturages>();

            while (reader.Read())
            {
                Covoiturages unCovoit = new Covoiturages
                {
                    CovoitId = (int)reader["CovoitId"],
                    CovoitDate = DateOnly.FromDateTime((DateTime)reader["CovoitDate"]),
                    CovoitDep = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitDep"]),
                    CovoitArr = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitArr"]),
                    DepartAdresse = new Adresses
                    {
                        AdresseVille = reader["DepartVille"].ToString(),
                    },
                    ArriveAdresse = new Adresses
                    {
                        AdresseVille = reader["ArriveVille"].ToString(),
                    }
                };
                listeCovoiturages.Add(unCovoit);
            }
            reader.Close();

            this._connexion.Close();

            return listeCovoiturages;
        }
    }
}
