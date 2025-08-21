using System.Data;
using System.Runtime.Intrinsics.Arm;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur.Conducteur
{
    public class CovoiturageRepo : Connexion, ICovoiturageRepo
    {
     
        public List<Covoiturages> GetAllCovoitByUtilId(int unId)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT Covoitid, CovoitDate, CovoitDep, CovoitArr, dep.AdresseVille AS DepartVille, arr.AdresseVille AS ArriveVille, CovoitStatut FROM utilisateurs " +
                "INNER JOIN conducteurs ON ConducUtil = UtilId " +
                "INNER JOIN voitures ON VoitConduc = ConducId " +
                "INNER JOIN covoiturages ON CovoitVoiture = VoitId " +
                "INNER JOIN adresses AS dep ON DepartAdresse = dep.AdresseId " +
                "INNER JOIN adresses AS arr ON ArriveAdresse = arr.AdresseId " +
                "WHERE UtilId = @UtilId";

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
                    CovoitStatut = reader["CovoitStatut"].ToString(),
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

            DbDeconnecter();

            return listeCovoiturages;
        }

        public int DeleteCovoitById(int unId)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "DELETE FROM covoiturages WHERE CovoitId = @CovoitId";

            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            CovoitId.Value = unId;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }

        public List<Utilisateurs> GetUtilCredit(int unCovoitId)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilCredit, UtilId FROM reservations " +
                "INNER JOIN utilisateurs ON ResaUtil = UtilId " +
                "WHERE ResaCovoit = @CovoitId";

            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            CovoitId.Value = unCovoitId;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Utilisateurs> listeUtilisateurs = new List<Utilisateurs>();

            while (reader.Read())
            {
                Utilisateurs unUtilisateur = new Utilisateurs
                {
                    UtilCredit = Convert.ToInt16(reader["UtilCredit"]),
                    UtilId = (int)reader["UtilId"]
                };
                listeUtilisateurs.Add(unUtilisateur);
            }
            reader.Close();

            DbDeconnecter();

            return listeUtilisateurs;
        }

        public int UpdateCreditUtil(int unId, short unSolde)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE utilisateurs SET UtilCredit = @UtilCredit WHERE UtilId = @UtilId";

            SqlParameter UtilCredit = cmd.Parameters.Add("@UtilCredit", SqlDbType.SmallInt);
            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilCredit.Value = unSolde;
            UtilId.Value = unId;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }
    }
}
