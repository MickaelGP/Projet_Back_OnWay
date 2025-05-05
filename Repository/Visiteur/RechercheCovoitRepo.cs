using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Visiteur
{
    public class RechercheCovoitRepo
    {
        private SqlConnection _connexion;

        public RechercheCovoitRepo()
        {
            DbConnecter();
        }

        private void DbConnecter()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }
        public Covoiturages GetExactCovoiturages(Covoiturages covoiturages)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT CovoitPrix, CovoitId, CovoitDate, dep.AdresseVille AS VilleDepart, arr.AdresseVille AS VilleArriver, CovoitDep, CovoitArr, dep.AdresseCp AS Cp from covoiturages INNER JOIN adresses AS dep ON dep.AdresseId = DepartAdresse INNER JOIN adresses AS arr ON arr.AdresseId = ArriveAdresse WHERE dep.AdresseVille = @VilleDepart AND  arr.AdresseVille =  @VilleArriver AND CovoitDate = @Date";

            SqlParameter VilleDepart = cmd.Parameters.Add("@VilleDepart", SqlDbType.VarChar);
            SqlParameter VilleArriver = cmd.Parameters.Add("@VilleArriver", SqlDbType.VarChar);
            SqlParameter Date = cmd.Parameters.Add("@Date", SqlDbType.Date);

            VilleDepart.Value = covoiturages.DepartAdresse.AdresseVille;
            VilleArriver.Value = covoiturages.ArriveAdresse.AdresseVille;
            Date.Value = covoiturages.CovoitDate;

            SqlDataReader reader = cmd.ExecuteReader();
            Covoiturages unCovoiturage = null;
            if (reader.Read())
            {
                unCovoiturage = new Covoiturages
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
            }
            reader.Close();

            this._connexion.Close();

            return unCovoiturage;
        }
        public string GetCodePostal(string unVille)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT  DISTINCT AdresseCP FROM adresses WHERE AdresseVille = @AdresseVille";

            SqlParameter AdresseVille = cmd.Parameters.Add("@AdresseVille", SqlDbType.Char);

            AdresseVille.Value = unVille;

            SqlDataReader reader = cmd.ExecuteReader();

            string resultat = null;
            while (reader.Read())
            {
                resultat = reader["AdresseCP"].ToString();
            }

            this._connexion.Close();

            return resultat;
        }
        public Covoiturages GetAlternatifCovoit(Covoiturages covoiturage, string unCp)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT CovoitPrix, CovoitId, CovoitDate, dep.AdresseVille AS VilleDepart, arr.AdresseVille AS VilleArriver, CovoitDep, CovoitArr FROM covoiturages INNER JOIN adresses AS dep ON dep.AdresseId = DepartAdresse INNER JOIN adresses AS arr ON arr.AdresseId = ArriveAdresse WHERE dep.AdresseCP LIKE @AdresseCP  AND  arr.AdresseVille =  @VilleArriver AND CovoitDate = @Date";

            SqlParameter AdresseCP = cmd.Parameters.Add("@AdresseCP", SqlDbType.Char);
            SqlParameter VilleArriver = cmd.Parameters.Add("@VilleArriver", SqlDbType.VarChar);
            SqlParameter Date = cmd.Parameters.Add("@Date", SqlDbType.Date);

            AdresseCP.Value = unCp + "%";
            VilleArriver.Value = covoiturage.ArriveAdresse.AdresseVille;
            Date.Value = covoiturage.CovoitDate;

            SqlDataReader reader = cmd.ExecuteReader();
            Covoiturages unCovoiturage = null;

            if (reader.Read())
            {
                unCovoiturage = new Covoiturages
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
            }
            reader.Close();

            this._connexion.Close();

            return unCovoiturage;
        }
    }
}
