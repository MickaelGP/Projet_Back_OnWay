using System.Data;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public class ParticiperCovoitRepo : IParticiperCovoitRepo
    {
        private readonly SqlConnection _connexion;

        public ParticiperCovoitRepo()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public int GetNbSiegeRestant(int unCovoitId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT (VoitNbSiege - ISNULL(SUM(ResaNbSiege),0)) AS SiegeRestant FROM voitures " +
                "INNER JOIN covoiturages ON CovoitVoiture = VoitId " +
                "LEFT JOIN reservations ON ResaCovoit = CovoitId " +
                "WHERE CovoitId = @CovoitId " +
                "GROUP BY VoitNbSiege";

            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            CovoitId.Value = unCovoitId;

            SqlDataReader reader = cmd.ExecuteReader();
            int resultat = -1;
            while (reader.Read())
            {
                resultat = Convert.ToInt32(reader["SiegeRestant"]);
            }

            reader.Close();

            this._connexion.Close();

            return resultat;
        }

        public int GetSoldeUtil(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilCredit FROM utilisateurs WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();
            int resultat = -1;

            while (reader.Read())
            {
                resultat = Convert.ToInt32(reader["UtilCredit"]);
            }

            reader.Close();

            this._connexion.Close();

            return resultat;
        }

        public int InsertReservationCovoit(Reservations uneReservation)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO reservations (ResaStatut, ResaNbSiege, ResaDate, ResaUtil, ResaCovoit) VALUES " +
                "(@ResaStatut, @ResaNbSiege, @ResaDate, @UtilId, @CovoitId);";

            SqlParameter ResaStatut = cmd.Parameters.Add("@ResaStatut", SqlDbType.VarChar);
            SqlParameter ResaNbSiege = cmd.Parameters.Add("@ResaNbSiege", SqlDbType.TinyInt);
            SqlParameter ResaDate = cmd.Parameters.Add("@ResaDate", SqlDbType.Date);
            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);
            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            ResaStatut.Value = uneReservation.ResaStatut;
            ResaNbSiege.Value = uneReservation.ResaNbSiege;
            ResaDate.Value = uneReservation.ResaDate;
            UtilId.Value = uneReservation.ResaUtil;
            CovoitId.Value = uneReservation.ResaCovoit;

            int resultat = cmd.ExecuteNonQuery();

            this._connexion.Close();

            return resultat;
        }

        public int UpdateSoleCreditUtil(int unId, int unSolde)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE utilisateurs SET UtilCredit = @UtilCredit WHERE UtilId = @UtilId";

            SqlParameter UtilCredit = cmd.Parameters.Add("@UtilCredit", SqlDbType.SmallInt);
            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilCredit.Value = unSolde;
            UtilId.Value = unId;

            int resultat = cmd.ExecuteNonQuery();

            this._connexion.Close();

            return resultat;
        }
    }
}
