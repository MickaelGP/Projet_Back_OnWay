using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Visiteur
{
    public class RechercheCovoitRepo : Connexion
    {
        public List<Covoiturages> GetExactCovoiturages(Covoiturages covoiturages)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT CovoitPrix, CovoitId, CovoitDate, dep.AdresseVille AS VilleDepart, arr.AdresseVille AS VilleArriver, CovoitDep, CovoitArr, dep.AdresseCp AS Cp from covoiturages " +
                "INNER JOIN adresses AS dep ON dep.AdresseId = DepartAdresse " +
                "INNER JOIN adresses AS arr ON arr.AdresseId = ArriveAdresse " +
                "WHERE dep.AdresseVille = @VilleDepart " +
                "AND  arr.AdresseVille =  @VilleArriver " +
                "AND CovoitDate = @Date AND CovoitStatut = @CovoitStatut";

            SqlParameter VilleDepart = cmd.Parameters.Add("@VilleDepart", SqlDbType.VarChar);
            SqlParameter VilleArriver = cmd.Parameters.Add("@VilleArriver", SqlDbType.VarChar);
            SqlParameter Date = cmd.Parameters.Add("@Date", SqlDbType.Date);
            SqlParameter CovoitStatut = cmd.Parameters.Add("@CovoitStatut", SqlDbType.VarChar);

            VilleDepart.Value = covoiturages.DepartAdresse.AdresseVille;
            VilleArriver.Value = covoiturages.ArriveAdresse.AdresseVille;
            Date.Value = covoiturages.CovoitDate;
            CovoitStatut.Value = "En attente";

            SqlDataReader reader = cmd.ExecuteReader();
            List<Covoiturages> Covoiturages = new List<Covoiturages>();
            while (reader.Read())
            {
               Covoiturages unCovoiturage = new Covoiturages
                {
                    CovoitId = (int)reader["CovoitId"],
                    CovoitPrix = Convert.ToDouble(reader["CovoitPrix"]),
                    CovoitDate = DateOnly.FromDateTime((DateTime)reader["CovoitDate"]),
                    CovoitDep = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitDep"]),
                    CovoitArr = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitArr"]),
                    ArriveAdresse = new Adresses
                    {
                        AdresseVille = reader["VilleArriver"].ToString()
                    },
                    DepartAdresse = new Adresses
                    {
                        AdresseVille = reader["VilleDepart"].ToString(),
                        AdresseCp = reader["Cp"].ToString()
                    }

                };
                Covoiturages.Add(unCovoiturage);
            }
            reader.Close();

            DbDeconnecter();

            return Covoiturages;
        }
        public string? GetCodePostal(string unVille)
        {
         DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT  DISTINCT AdresseCP FROM adresses WHERE AdresseVille = @AdresseVille";

            SqlParameter AdresseVille = cmd.Parameters.Add("@AdresseVille", SqlDbType.Char);

            AdresseVille.Value = unVille;

            SqlDataReader reader = cmd.ExecuteReader();

            string? resultat = null;
            while (reader.Read())
            {
                resultat = reader["AdresseCP"].ToString();
            }

            DbDeconnecter();

            return resultat;
        }
        public List<Covoiturages> GetAlternatifCovoit(Covoiturages covoiturage, string unCp)
        {
            DbConnecter() ;

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT CovoitPrix, CovoitId, CovoitDate, dep.AdresseVille AS VilleDepart, arr.AdresseVille AS VilleArriver, CovoitDep, CovoitArr FROM covoiturages INNER JOIN adresses AS dep ON dep.AdresseId = DepartAdresse INNER JOIN adresses AS arr ON arr.AdresseId = ArriveAdresse WHERE dep.AdresseCP LIKE @AdresseCP  AND  arr.AdresseVille =  @VilleArriver AND CovoitDate = @Date AND CovoitStatut = @CovoitStatut";

            SqlParameter AdresseCP = cmd.Parameters.Add("@AdresseCP", SqlDbType.Char);
            SqlParameter VilleArriver = cmd.Parameters.Add("@VilleArriver", SqlDbType.VarChar);
            SqlParameter Date = cmd.Parameters.Add("@Date", SqlDbType.Date);
            SqlParameter CovoitStatut = cmd.Parameters.Add("@CovoitStatut", SqlDbType.VarChar);


            AdresseCP.Value = unCp + "%";
            VilleArriver.Value = covoiturage.ArriveAdresse.AdresseVille;
            Date.Value = covoiturage.CovoitDate;
            CovoitStatut.Value = "En attente";

            SqlDataReader reader = cmd.ExecuteReader();
            List<Covoiturages> covoiturages = new List<Covoiturages>();

            while (reader.Read())
            {
                Covoiturages unCovoiturage = new Covoiturages
                {
                    CovoitId = (int)reader["CovoitId"],
                    CovoitPrix = Convert.ToDouble(reader["CovoitPrix"]),
                    CovoitDate = DateOnly.FromDateTime((DateTime)reader["CovoitDate"]),
                    CovoitDep = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitDep"]),
                    CovoitArr = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitArr"]),
                    ArriveAdresse = new Adresses
                    {
                        AdresseVille = reader["VilleArriver"].ToString()
                    },
                    DepartAdresse = new Adresses
                    {
                        AdresseVille = reader["VilleDepart"].ToString()
                    }

                };
                covoiturages.Add(unCovoiturage);
            }
            reader.Close();

            DbDeconnecter();

            return covoiturages;
        }
    }
}
