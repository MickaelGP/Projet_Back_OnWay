using System.Data;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public class DeposerReclamationRepo : IDeposerReclamationRepo
    {
        private readonly SqlConnection _connexion;

        public DeposerReclamationRepo()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public int CheckIfPlainteExist(int unUtilId, int unCovoitId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT COUNT(PlainteId) FROM plaintes INNER JOIN reservations ON ResaId = PlainteResa WHERE ResaUtil = @UtilId AND ResaCovoit = @CovoitId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);
            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            UtilId.Value = unUtilId;
            CovoitId.Value = unCovoitId;

            int resultat = (int)cmd.ExecuteScalar();

            this._connexion.Close();

            return resultat;
        }

        public Reservations GetInfoResa(int unUtilId, int unCovoitId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT ResaId, CovoitStatut FROM reservations " +
                "INNER JOIN utilisateurs ON UtilId = ResaUtil " +
                "INNER JOIN covoiturages ON ResaCovoit = CovoitId " +
                "WHERE ResaUtil = @UtilId AND ResaCovoit = @CovoitId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);
            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            UtilId.Value = unUtilId;
            CovoitId.Value = unCovoitId;

            Reservations? reservation = null;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                reservation = new Reservations
                {
                    ResaId = (int)reader["ResaId"],
                    Covoiturages = new Covoiturages
                    {
                        CovoitStatut = reader["CovoitStatut"].ToString()
                    }
                };
            }
            reader.Close();

            this._connexion.Close();

            return reservation;
        }

        public int InsertPlaintes(Plaintes unPlainte)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO plaintes VALUES (@PlainteDescription, @PlainteStatut, @PlainteResa)";

            SqlParameter PlainteDescription = cmd.Parameters.Add("@PlainteDescription", SqlDbType.VarChar);
            SqlParameter PlainteStatut = cmd.Parameters.Add("@PlainteStatut", SqlDbType.VarChar);
            SqlParameter PlainteResa = cmd.Parameters.Add("@PlainteResa", SqlDbType.Int);

            PlainteDescription.Value = unPlainte.PlainteDescription;
            PlainteStatut.Value = unPlainte.PlainteSatut;
            PlainteResa.Value = unPlainte.PlainteResa;

            int resultat = cmd.ExecuteNonQuery();

            return resultat;
        }
    }
}
