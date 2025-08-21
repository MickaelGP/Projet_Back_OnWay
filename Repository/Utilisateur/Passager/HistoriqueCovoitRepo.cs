using System.Data;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public class HistoriqueCovoitRepo : Connexion,  IHistoriqueCovoitRepo
    {
        public List<Covoiturages> GetAllCovoitByUtilId(int unId)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT CovoitId, CovoitDate, CovoitDep ,CovoitArr, dep.AdresseVille AS Depart, arr.AdresseVille AS Arriver, CovoitStatut  FROM reservations " +
                "INNER JOIN covoiturages ON ResaCovoit = CovoitId " +
                "INNER JOIN adresses AS dep ON dep.AdresseId = DepartAdresse " +
                "INNER JOIN adresses AS arr ON arr.AdresseId = ArriveAdresse " +
                "INNER JOIN utilisateurs ON UtilId = ResaUtil " +
                "WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Covoiturages> listeCovoits = new List<Covoiturages>();

            while (reader.Read())
            {
                Covoiturages unCovoit = new Covoiturages
                {
                    CovoitId = (int)reader["CovoitId"],
                    CovoitDate = DateOnly.FromDateTime((DateTime)reader["CovoitDate"]),
                    CovoitDep = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitDep"]),
                    CovoitArr = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitArr"]),
                    CovoitStatut = reader["CovoitStatut"].ToString(),
                    DepartAdresse = new Adresses
                    {
                        AdresseVille = reader["Depart"].ToString(),
                    },
                    ArriveAdresse = new Adresses
                    {
                        AdresseVille = reader["Arriver"].ToString(),
                    }
                };
                listeCovoits.Add(unCovoit);
            }
            reader.Close();

            DbDeconnecter();

            return listeCovoits;
        }
    }
}
