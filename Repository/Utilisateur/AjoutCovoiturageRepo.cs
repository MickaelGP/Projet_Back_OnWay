using System.Data;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;

namespace BackOnWay.Repository.Utilisateur
{
    public class AjoutCovoiturageRepo
    {
        private SqlConnection _connexion;

        public AjoutCovoiturageRepo()
        {
            DbConnecter();
        }

        private void DbConnecter()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public int InsertCovoiturage(Covoiturages unCovoiturage)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO covoiturages VALUES " +
                "(@CovoitPrix, @CovoitDate, @CovoitDep, @CovoitArr, @CovoitStatut, @CovoitFumeur, @CovoitAnimaux, @CovoitMusique, @CovoitVoiture, @DepartAdresse, @ArriveAdresse)";

            SqlParameter CovoitPrix = cmd.Parameters.Add("@CovoitPrix", SqlDbType.Money);
            SqlParameter CovoitDate = cmd.Parameters.Add("@CovoitDate", SqlDbType.Date);
            SqlParameter CovoitDep = cmd.Parameters.Add("@CovoitDep", SqlDbType.Time);
            SqlParameter CovoitArr = cmd.Parameters.Add("@CovoitArr", SqlDbType.Time);
            SqlParameter CovoitStatut = cmd.Parameters.Add("@CovoitStatut", SqlDbType.VarChar);
            SqlParameter CovoitFumeur = cmd.Parameters.Add("@CovoitFumeur", SqlDbType.Bit);
            SqlParameter CovoitAnimaux = cmd.Parameters.Add("@CovoitAnimaux", SqlDbType.Bit);
            SqlParameter CovoitMusique = cmd.Parameters.Add("@CovoitMusique", SqlDbType.Bit);
            SqlParameter CovoitVoiture = cmd.Parameters.Add("@CovoitVoiture", SqlDbType.Int);
            SqlParameter DepartAdresse = cmd.Parameters.Add("@DepartAdresse", SqlDbType.Int);
            SqlParameter ArriveAdresse = cmd.Parameters.Add("@ArriveAdresse", SqlDbType.Int);

            CovoitPrix.Value = unCovoiturage.CovoitPrix;
            CovoitDate.Value = unCovoiturage.CovoitDate;
            CovoitDep.Value = unCovoiturage.CovoitDep;
            CovoitArr.Value = unCovoiturage.CovoitArr;
            CovoitStatut.Value = unCovoiturage.CovoitStatut;
            CovoitFumeur.Value = unCovoiturage.CovoitFumeur;
            CovoitAnimaux.Value = unCovoiturage.CovoitAnimaux;
            CovoitMusique.Value = unCovoiturage.CovoitMusique;
            CovoitVoiture.Value = unCovoiturage.CovoitVoiture;
            DepartAdresse.Value = unCovoiturage.DepAdresse;
            ArriveAdresse.Value = unCovoiturage.ArrAdresse;

            int reponse = cmd.ExecuteNonQuery();

            this._connexion.Close();

            return reponse;
        }

        public int UpdateSoldeCredit(int unId, int unSolde)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
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

        public int InsertAdresse(Adresses unAdresse)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO adresses OUTPUT inserted.AdresseId VALUES (@AdresseNum, @AdresseRue, @AdresseCp, @AdresseVille)";

            SqlParameter AdresseNum = cmd.Parameters.Add("@AdresseNum", SqlDbType.SmallInt);
            SqlParameter AdresseRue = cmd.Parameters.Add("@AdresseRue", SqlDbType.VarChar);
            SqlParameter AdresseCp = cmd.Parameters.Add("@AdresseCp", SqlDbType.Char);
            SqlParameter AdresseVille = cmd.Parameters.Add("@AdresseVille", SqlDbType.VarChar);

            AdresseNum.Value = unAdresse.AdresseNum;
            AdresseRue.Value = unAdresse.AdresseRue;
            AdresseCp.Value = unAdresse.AdresseCp;
            AdresseVille.Value = unAdresse.AdresseVille;

            int resultat = (int)cmd.ExecuteScalar();

            this._connexion.Close();

            return resultat;
        }
        public int CheckSoldeCredit(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilCredit FROM utilisateurs WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            int resultat = Convert.ToInt32(cmd.ExecuteScalar());

            this._connexion.Close();

            return resultat;
        }

        public int CheckIfVoitureIsInCovoiturage(int voitId, DateOnly covoitDate)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT CovoitId FROM covoiturages " +
                "WHERE CovoitVoiture = @VoitId AND CovoitDate = @CovoitDate AND (CovoitStatut = 'En attente' OR CovoitStatut = 'Démarré')";

            SqlParameter VoitId = cmd.Parameters.Add("@VoitId", SqlDbType.Int);
            SqlParameter CovoitDate = cmd.Parameters.Add("@CovoitDate", SqlDbType.Date);

            VoitId.Value = voitId;
            CovoitDate.Value = covoitDate;

            object resultat = cmd.ExecuteScalar();
            int reponse;
            if (resultat == null)
            {
                reponse = 0;
            }
            else
            {
                reponse = (int)resultat;
            }

            this._connexion.Close();

            return reponse;
        }
        public List<Voitures> GetUtilVoitures(int unId)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT VoitId, ModeleNom FROM voitures " +
                "INNER JOIN modeles ON ModeleId = VoitModele " +
                "INNER JOIN conducteurs ON ConducId = VoitConduc " +
                "INNER JOIN utilisateurs ON UtilId = ConducUtil " +
                "WHERE UtilId = @UtilId";

            SqlParameter UtilId = cmd.Parameters.Add("@UtilId", SqlDbType.Int);

            UtilId.Value = unId;

            SqlDataReader reader = cmd.ExecuteReader();

            List<Voitures> listeVoitures = new List<Voitures>();

            while (reader.Read())
            {
                Voitures unVoiture = new Voitures
                {
                    VoitId = (int)reader["VoitId"],
                    Modeles = new Modeles
                    {
                        ModeleNom = reader["ModeleNom"].ToString()
                    }
                };
                listeVoitures.Add(unVoiture);
            }

            reader.Close();

            this._connexion.Close();

            return listeVoitures;
        }
    }
}
