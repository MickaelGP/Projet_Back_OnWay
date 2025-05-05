using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Employe
{
    public class ReclamationRepo
    {
        private SqlConnection _connexion;

        public ReclamationRepo()
        {
            DbConnecter();
        }

        private void DbConnecter()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public List<Plaintes> GetAllPlaintes()
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            List<Plaintes> listePlaintes = new List<Plaintes>();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT * FROM plaintes";

            SqlDataReader plaintes = cmd.ExecuteReader();

            while (plaintes.Read())
            {
                Plaintes unePlainte = new Plaintes
                {
                    PlainteId = (int)plaintes["PlainteId"],
                    PlainteDescription = plaintes["PlainteDescription"].ToString(),
                    PlainteSatut = plaintes["PlainteStatut"].ToString(),
                };
                listePlaintes.Add(unePlainte);
            }
            this._connexion.Close();

            return listePlaintes;
        }
        public Plaintes GetPlainteById(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT PlainteDescription, PlainteId, PlainteStatut, PlainteResa, ResaDate, UtilNom, UtilEmail FROM plaintes INNER JOIN reservations ON PlainteResa = ResaId INNER JOIN utilisateurs ON UtilId = ResaUtil WHERE PlainteId = @PlainteId";

            SqlParameter PlainteId = cmd.Parameters.Add("@PlainteId", SqlDbType.Int);

            PlainteId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            Plaintes unePlainte = null;

            if (reader.Read())
            {
                unePlainte = new Plaintes
                {
                    PlainteId = (int)reader["PlainteId"],
                    PlainteDescription = reader["PlainteDescription"].ToString(),
                    PlainteSatut = reader["PlainteStatut"].ToString(),
                    PlainteResa = (int)reader["PlainteResa"],
                    Reservations = new Reservations
                    {
                        ResaDate = DateOnly.FromDateTime((DateTime)reader["ResaDate"]),
                        Utilisateurs = new Utilisateurs
                        {
                            UtilNom = reader["UtilNom"].ToString(),
                            UtilEmail = reader["UtilEmail"].ToString()
                        }
                    }
                };
            }
            this._connexion.Close();

            return unePlainte;
        }
        public int UpdateStatutPlainte(Plaintes unePlainte)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE plaintes SET PlainteStatut = @PlainteStatut WHERE PlainteId = @PlainteId";

            SqlParameter PlainteId = cmd.Parameters.Add("@PlainteId", SqlDbType.Int);
            SqlParameter PlainteStatut = cmd.Parameters.Add("@PlainteStatut", SqlDbType.VarChar);

            PlainteId.Value = unePlainte.PlainteId;
            PlainteStatut.Value = unePlainte.PlainteSatut;

            int resultat = cmd.ExecuteNonQuery();

            this._connexion.Close();

            return resultat;
        }
    }
}
