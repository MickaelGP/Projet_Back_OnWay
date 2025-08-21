using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Employe
{
    public class ValidAvisRepo : Connexion
    {
        public List<Avis> GetAllAvis()
        {
            DbConnecter();

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
            DbDeconnecter();

            return listeAvis;
        }
        public Avis SelectAvisById(int unId)
        {
            DbConnecter();

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

            DbDeconnecter();

            return unAvis;
        }

        public int UpdateStatutAvis(Avis unAvis)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE  avis SET AvisStatut = @AvisStatut WHERE AvisId = @AvisId";

            SqlParameter AvisStatut = cmd.Parameters.Add("@AvisStatut", SqlDbType.VarChar);
            SqlParameter AvisId = cmd.Parameters.Add("@AvisId", SqlDbType.Int);

            AvisId.Value = unAvis.AvisId;
            AvisStatut.Value = unAvis.AvisStatut;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }
    }
}
