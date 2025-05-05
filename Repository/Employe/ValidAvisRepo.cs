using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Employe
{
    public class ValidAvisRepo
    {
        private SqlConnection _connexion;

        public ValidAvisRepo()
        {
            DbConnecter();
        }

        private void DbConnecter()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public List<Avis> GetAllAvis()
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            List<Avis> listeAvis = new List<Avis>();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT * FROM avis WHERE AvisStatut = @AvisStatut";
            SqlParameter AvisStatut = cmd.Parameters.Add("@AvisStatut", SqlDbType.VarChar);

            AvisStatut.Value = "En attente";

            SqlDataReader avis = cmd.ExecuteReader();

            while (avis.Read())
            {
                Avis unAvis = new Avis
                {
                    AvisId = (int)avis["AvisId"],
                    AvisTitre = avis["AvisTitre"].ToString(),
                    AvisStatut = avis["AvisStatut"].ToString(),
                };
                listeAvis.Add(unAvis);
            }
            _connexion.Close();

            return listeAvis;
        }
        public Avis SelectAvisById(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT AvisId, AvisTitre, AvisCom, AvisNote, AvisStatut, depo.UtilNom AS NomExpediteur , recu.UtilNom AS NomDestinataire  FROM avis INNER JOIN utilisateurs as depo ON AvisDeposer = depo.UtilId INNER JOIN utilisateurs as recu ON AvisRecu = recu.UtilId WHERE AvisId = @AvisId";

            SqlParameter AvisId = cmd.Parameters.Add("@AvisId", SqlDbType.Int);

            AvisId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            Avis unAvis = null;

            if (reader.Read())
            {
                unAvis = new Avis
                {
                    AvisId = (int)reader["AvisId"],
                    AvisTitre = reader["AvisTitre"].ToString(),
                    AvisCom = reader["AvisCom"].ToString(),
                    AvisNote = (byte)reader["AvisNote"],
                    AvisStatut = reader["AvisStatut"].ToString(),
                    UtilisateurDepo = new Utilisateurs
                    {
                        UtilNom = reader["NomExpediteur"].ToString()
                    },
                    UtilisateurRecu = new Utilisateurs
                    {
                        UtilNom = reader["NomDestinataire"].ToString()
                    }

                };

            }
            reader.Close();

            this._connexion.Close();

            return unAvis;
        }

        public int UpdateStatutAvis(Avis unAvis)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE  avis SET AvisStatut = @AvisStatut WHERE AvisId = @AvisId";

            SqlParameter AvisStatut = cmd.Parameters.Add("@AvisStatut", SqlDbType.VarChar);
            SqlParameter AvisId = cmd.Parameters.Add("@AvisId", SqlDbType.Int);

            AvisId.Value = unAvis.AvisId;
            AvisStatut.Value = unAvis.AvisStatut;

            int resultat = cmd.ExecuteNonQuery();

            _connexion.Close();

            return resultat;
        }
    }
}
