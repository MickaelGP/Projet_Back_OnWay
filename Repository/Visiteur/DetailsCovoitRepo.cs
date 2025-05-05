using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Visiteur
{
    public class DetailsCovoitRepo
    {
        private SqlConnection _connexion;

        public DetailsCovoitRepo()
        {
            DbConnecter();
        }

        private void DbConnecter()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public Covoiturages GetInfoCovoit(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT  AvisStatut, AvisNote, UtilId,  CovoitMusique, CovoitFumeur, CovoitAnimaux, UtilPseudo, VoitEnergie, ModeleNom, MarqueNom, CouleurNom, CovoitId, CovoitArr, CovoitPrix, CovoitDep, CovoitDate, depart.AdresseNum AS DepartNum, depart.AdresseRue AS DepartAdr, depart.AdresseVille AS DepartVille, arriver.AdresseNum AS ArriveNum, arriver.AdresseRue AS ArriveAdr, arriver.AdresseVille AS ArriveVille FROM covoiturages " +
                "INNER JOIN adresses AS depart ON depart.AdresseId = DepartAdresse " +
                "INNER JOIN adresses AS arriver ON arriver.AdresseId = ArriveAdresse " +
                "INNER JOIN voitures ON VoitId = CovoitVoiture " +
                "INNER JOIN modeles ON ModeleId = VoitModele " +
                "INNER JOIN marques ON MarqueId = ModeleMarque " +
                "INNER JOIN couleurs ON CouleurId = VoitCouleur " +
                "INNER JOIN conducteurs ON ConducId = VoitConduc " +
                "LEFT JOIN utilisateurs ON UtilId = ConducUtil " +
                "LEFT JOIN avis ON AvisRecu = UtilId " +
                "WHERE CovoitId = @CovoitId ";

            SqlParameter CovoitId = cmd.Parameters.Add("@CovoitId", SqlDbType.Int);

            CovoitId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            double totalNote = 0;
            int nombreCommentaires = 0;

            Covoiturages? unCovoiturage = null;

            while (reader.Read())
            {
                if (reader["AvisStatut"] != DBNull.Value && reader["AvisStatut"].ToString() == "Validé")
                {
                    if (reader["AvisNote"] != DBNull.Value)
                    {
                        totalNote += Convert.ToDouble(reader["AvisNote"]);
                        nombreCommentaires++;
                    }
                }
                if (unCovoiturage == null)
                {
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
                        DepartAdresse = new Adresses
                        {
                            AdresseNum = Convert.ToByte(reader["DepartNum"]),
                            AdresseRue = reader["DepartAdr"].ToString(),
                            AdresseVille = reader["DepartVille"].ToString()
                        },
                        ArriveAdresse = new Adresses
                        {
                            AdresseNum = Convert.ToByte(reader["ArriveNum"]),
                            AdresseRue = reader["ArriveAdr"].ToString(),
                            AdresseVille = reader["ArriveVille"].ToString()
                        },
                        Voiture = new Voitures
                        {
                            VoitEnergie = reader["VoitEnergie"].ToString(),
                            Couleurs = new Couleurs
                            {
                                CouleurNom = reader["CouleurNom"].ToString()
                            },
                            Modeles = new Modeles
                            {
                                ModeleNom = reader["ModeleNom"].ToString(),
                                Marque = new Marques
                                {
                                    MarqueNom = reader["MarqueNom"].ToString()
                                }
                            },
                            Conducteurs = new Conducteurs
                            {
                                Utilisateur = new Utilisateurs
                                {
                                    UtilId = (int)reader["UtilId"],
                                    UtilPseudo = reader["UtilPseudo"].ToString()
                                }
                            }
                        }
                    };
                }
            }

            // Ajout des notes après la lecture de toutes les lignes
            if (unCovoiturage?.Voiture?.Conducteurs?.Utilisateur != null)
            {
                unCovoiturage.Voiture.Conducteurs.Utilisateur.Note = totalNote;
                unCovoiturage.Voiture.Conducteurs.Utilisateur.NombreCom = nombreCommentaires;
            }

            this._connexion.Close();

            return unCovoiturage;
        }

        public List<Avis> GetAvisByConducId(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT AvisTitre, AvisCom, AvisNote, UtilPseudo FROM avis " +
                "INNER JOIN utilisateurs ON UtilId = AvisDeposer " +
                " WHERE AvisRecu = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Avis> listeAvis = new List<Avis>();


            while (reader.Read())
            {
                Avis unAvis = new Avis
                {
                    AvisCom = reader["AvisCom"].ToString(),
                    AvisNote = Convert.ToByte(reader["AvisNote"]),
                    AvisTitre = reader["AvisTitre"].ToString(),
                    UtilisateurDepo = new Utilisateurs
                    {
                        UtilPseudo = reader["UtilPseudo"].ToString()
                    }
                };
                listeAvis.Add(unAvis);
            }

            this._connexion.Close();

            return listeAvis;
        }
    }
}
