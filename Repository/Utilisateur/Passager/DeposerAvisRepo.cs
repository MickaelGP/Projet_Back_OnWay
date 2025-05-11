using System.Data;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public class DeposerAvisRepo : IDeposerAvisRepo
    {
        private readonly SqlConnection _connexion;

        public DeposerAvisRepo()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public Covoiturages GetUtilIdAndStatutCovoit(int unCovoitId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilId, CovoitStatut FROM utilisateurs " +
                "INNER JOIN conducteurs ON ConducUtil = UtilId " +
                "INNER JOIN voitures ON ConducId = VoitConduc " +
                "INNER JOIN covoiturages ON VoitId = CovoitVoiture " +
                "WHERE CovoitId = @CovoitId";

            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            CovoitId.Value = unCovoitId;

            SqlDataReader reader = cmd.ExecuteReader();

            Covoiturages? unCovoit = null;

            while (reader.Read())
            {
                unCovoit = new Covoiturages
                {
                    CovoitStatut = reader["CovoitStatut"].ToString(),
                    Voiture = new Voitures
                    {
                        Conducteurs = new Conducteurs
                        {
                            Utilisateur = new Utilisateurs
                            {
                                UtilId = (int)reader["UtilId"]
                            }
                        }
                    }
                };
            }

            reader.Close();

            this._connexion.Close();

            return unCovoit;
        }

        public int InsertAvis(Avis unAvis)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                _connexion.Open();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO avis VALUES (@AvisTitre, @AvisCom, @AvisNote, @AvisStatut, @AvisDeposer, @AvisRecu)";

            SqlParameter AvisTitre = cmd.Parameters.Add("@AvisTitre", SqlDbType.VarChar);
            SqlParameter AvisCom = cmd.Parameters.Add("@AvisCom", SqlDbType.VarChar);
            SqlParameter AvisNote = cmd.Parameters.Add("@AvisNote", SqlDbType.TinyInt);
            SqlParameter AvisStatut = cmd.Parameters.Add("@AvisStatut", SqlDbType.VarChar);
            SqlParameter AvisDeposer = cmd.Parameters.Add("@AvisDeposer", SqlDbType.Int);
            SqlParameter AvisRecu = cmd.Parameters.Add("@AvisRecu", SqlDbType.Int);

            AvisTitre.Value = unAvis.AvisTitre;
            AvisCom.Value = unAvis.AvisCom;
            AvisNote.Value = unAvis.AvisNote;
            AvisStatut.Value = unAvis.AvisStatut;
            AvisDeposer.Value = unAvis.AvisDeposer;
            AvisRecu.Value = unAvis.AvisRecu;

            int resultat = cmd.ExecuteNonQuery();

            this._connexion.Close();

            return resultat;
        }
    }
}
