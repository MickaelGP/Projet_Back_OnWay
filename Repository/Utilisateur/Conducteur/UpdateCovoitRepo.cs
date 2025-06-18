using System.Data;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur.Conducteur
{
    public class UpdateCovoitRepo : IUpdateCovoitRepo
    {
        private readonly SqlConnection _connexion;

        public UpdateCovoitRepo()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }
        public Covoiturages GetInfoCovoitById(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT CovoitId, CovoitPrix, CovoitDate, CovoitDep, CovoitArr, CovoitFumeur, CovoitAnimaux, CovoitMusique, CovoitStatut FROM covoiturages WHERE CovoitId = @CovoitId ";

            SqlParameter CovoitId = cmd.Parameters.Add("CovoitId", SqlDbType.Int);
            
            CovoitId.Value = unId;

            Covoiturages unCovoiturage = null;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) {
                unCovoiturage = new Covoiturages
                {
                    CovoitId = (int)reader["CovoitId"],
                    CovoitPrix = Convert.ToDouble(reader["CovoitPrix"]),
                    CovoitDate = DateOnly.FromDateTime((DateTime)reader["CovoitDate"]),
                    CovoitDep = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitDep"]),
                    CovoitArr = TimeOnly.FromTimeSpan((TimeSpan)reader["CovoitArr"]),
                    CovoitAnimaux = (bool)reader["CovoitAnimaux"],
                    CovoitFumeur = (bool)reader["CovoitFumeur"],
                    CovoitMusique = (bool)reader["CovoitMusique"],
                    CovoitStatut = reader["CovoitStatut"].ToString()
                };
            }
            this._connexion.Close();

            return unCovoiturage;
        }
        public string GetStatutCovoit(int unCovoitId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT CovoitStatut FROM covoiturages WHERE CovoitId = @CovoitId";

            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            CovoitId.Value = unCovoitId;

            SqlDataReader reader = cmd.ExecuteReader();

            string? resultat = "N/C";

            if (reader.Read())
            {
                resultat = reader["CovoitStatut"].ToString();
            }

            reader.Close();

            this._connexion.Close();

            return resultat;
        }

        public int UpdateCovoit(Covoiturages infoCovoit)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE covoiturages SET CovoitPrix = @CovoitPrix, CovoitDate = @CovoitDate, CovoitDep = @CovoitDep, CovoitArr = @CovoitArr, CovoitFumeur = @CovoitFumeur, CovoitAnimaux = @CovoitAnimaux, CovoitMusique = @CovoitMusique WHERE CovoitId = @CovoitId";

            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);
            SqlParameter CovoitPrix = cmd.Parameters.Add("@CovoitPrix", SqlDbType.Money);
            SqlParameter CovoitDate = cmd.Parameters.Add("@CovoitDate", SqlDbType.Date);
            SqlParameter CovoitDep = cmd.Parameters.Add("@CovoitDep", SqlDbType.Time);
            SqlParameter CovoitArr = cmd.Parameters.Add("@CovoitArr", SqlDbType.Time);
            SqlParameter CovoitFumeur = cmd.Parameters.Add("@CovoitFumeur", SqlDbType.Bit);
            SqlParameter CovoitAnimaux = cmd.Parameters.Add("@CovoitAnimaux", SqlDbType.Bit);
            SqlParameter CovoitMusique = cmd.Parameters.Add("@CovoitMusique", SqlDbType.Bit);

            CovoitId.Value = infoCovoit.CovoitId;
            CovoitPrix.Value = infoCovoit.CovoitPrix;
            CovoitDate.Value = infoCovoit.CovoitDate;
            CovoitDep.Value = infoCovoit.CovoitDep;
            CovoitArr.Value = infoCovoit.CovoitArr;
            CovoitFumeur.Value = infoCovoit.CovoitFumeur;
            CovoitMusique.Value = infoCovoit.CovoitMusique;
            CovoitAnimaux.Value = infoCovoit.CovoitAnimaux;

            int resultat = cmd.ExecuteNonQuery();

            this._connexion.Close();

            return resultat;
        }
    }
}
