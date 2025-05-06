using System.Data;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur
{
    public class AjoutVoitureRepo
    {
        private SqlConnection _connexion;

        /// <summary>
        /// Initialise la connexion à la base de données.
        /// </summary>
        public AjoutVoitureRepo()
        {
            DbConnecter();
        }

        /// <summary>
        /// Initialise ou renouvelle la connexion SQL.
        /// </summary>
        private void DbConnecter()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        /// <summary>
        /// Insère un véhicule dans la base de données.
        /// </summary>
        /// <param name="unVoiture">Objet contenant les informations du véhicule.</param>
        /// <returns>Nombre de lignes insérées. -1 si erreur.</returns>
        public int InsertVoiture(Voitures unVoiture)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO voitures VALUES  (@VoitDateImat,  @VoitEnergie, @VoitPlaque,  @VoitNbSiege, @VoitModele,  @VoitCouleur,  @ConducId)";

            SqlParameter VoitDateImat = cmd.Parameters.Add("@VoitDateImat", SqlDbType.Date);
            SqlParameter VoitEnergie = cmd.Parameters.Add("@VoitEnergie", SqlDbType.VarChar);
            SqlParameter VoitPlaque = cmd.Parameters.Add("@VoitPlaque", SqlDbType.Char);
            SqlParameter VoitNbSiege = cmd.Parameters.Add("@VoitNbSiege", SqlDbType.TinyInt);
            SqlParameter VoitModele = cmd.Parameters.Add("@VoitModele", SqlDbType.Int);
            SqlParameter VoitCouleur = cmd.Parameters.Add("@VoitCouleur", SqlDbType.Int);
            SqlParameter ConducId = cmd.Parameters.Add("@ConducId", SqlDbType.Int);

            VoitDateImat.Value = unVoiture.VoitDateImat;
            VoitEnergie.Value = unVoiture.VoitEnergie;
            VoitPlaque.Value = unVoiture.VoitPlaque;
            VoitNbSiege.Value = unVoiture.VoitNbSiege;
            VoitModele.Value = unVoiture.VoitModele;
            VoitCouleur.Value = unVoiture.VoitCouleur;
            ConducId.Value = unVoiture.VoitConduc;

            int resultat = cmd.ExecuteNonQuery();

            this._connexion.Close();

            return resultat;
        }

        /// <summary>
        /// Ajoute un conducteur
        /// </summary>
        /// <param name="unId">L'identifiant de l'utilisateur</param>
        /// <returns>L'identifiant du conducteur créer</returns>
        public int InsertConduc(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO conducteurs OUTPUT inserted.ConducId VALUES  (@ConducDate, @UtilId)";

            SqlParameter ConducDate = cmd.Parameters.Add("@ConducDate", SqlDbType.DateTime);
            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            ConducDate.Value = DateTime.Now;
            UtilId.Value = unId;

            int resultat = (int)cmd.ExecuteScalar();

            this._connexion.Close();

            return resultat;

        }

        /// <summary>
        /// Vérifie si un utilisateur est déjà enregistré comme conducteur.
        /// </summary>
        /// <param name="unId">Identifiant de l'utilisateur.</param>
        /// <returns>Identifiant du conducteur s'il existe, -1 sinon.</returns>
        public int CheckUtilIsConduc(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT ConducId FROM conducteurs WHERE ConducUtil = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();
            int resultat = -1;
            if (reader.Read())
            {
                resultat = (int)reader["ConducId"];
            }
            reader.Close();

            this._connexion.Close();

            return resultat;
        }
    }
}
