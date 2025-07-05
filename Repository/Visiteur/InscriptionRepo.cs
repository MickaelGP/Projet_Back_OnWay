using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Visiteur
{
    public class InscriptionRepo
    {
        private SqlConnection _connexion;

        public InscriptionRepo()
        {
            DbConnecter();
        }

        private void DbConnecter()
        {
            Connexion maConnexion = new Connexion();
            _connexion = maConnexion.GetConnection();
        }

        public int CreateCompte(Utilisateurs unUtilisateur)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO utilisateurs (UtilPseudo,  UtilPrenom,  UtilNom, UtilNaissance, UtilCredit, UtilEmail, UtilTelephone, UtilMdp,  UtilGenre, UtilRole) VALUES " +
                "(@UtilPseudo, @UtilPrenom, @UtilNom, @UtilNaissance, @UtilCredit, @UtilEmail, @UtilTelephone, @UtilMdp, @UtilGenre, @UtilRole)";

            SqlParameter UtilPseudo = cmd.Parameters.Add("@UtilPseudo", SqlDbType.VarChar);
            SqlParameter UtilPrenom = cmd.Parameters.Add("@UtilPrenom", SqlDbType.VarChar);
            SqlParameter UtilNom = cmd.Parameters.Add("@UtilNom", SqlDbType.VarChar);
            SqlParameter UtilNaissance = cmd.Parameters.Add("@UtilNaissance", SqlDbType.Date);
            SqlParameter UtilCredit = cmd.Parameters.Add("@UtilCredit", SqlDbType.SmallInt);
            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);
            SqlParameter UtilTelephone = cmd.Parameters.Add("@UtilTelephone", SqlDbType.VarChar);
            SqlParameter UtilMdp = cmd.Parameters.Add("@UtilMdp", SqlDbType.VarChar);
            SqlParameter UtilGenre = cmd.Parameters.Add("@UtilGenre", SqlDbType.Char);
            SqlParameter UtilRole = cmd.Parameters.Add("@UtilRole", SqlDbType.Int);

            UtilPseudo.Value = unUtilisateur.UtilPseudo;
            UtilPrenom.Value = unUtilisateur.UtilPrenom;
            UtilNom.Value = unUtilisateur.UtilNom;
            UtilNaissance.Value = unUtilisateur.UtilNaissance;
            UtilCredit.Value = unUtilisateur.UtilCredit;
            UtilEmail.Value = unUtilisateur.UtilEmail;
            UtilTelephone.Value = unUtilisateur.UtilTelephone;
            UtilMdp.Value = unUtilisateur.UtilMdp;
            UtilGenre.Value = unUtilisateur.UtilGenre;
            UtilRole.Value = unUtilisateur.UtilRole;

            int resultat = cmd.ExecuteNonQuery();

            this._connexion.Close();

            return resultat;
        }

        public int CheckExistEmail(string unEmail)
        {
            if (_connexion == null || _connexion.State == ConnectionState.Closed)
            {
                DbConnecter();
            }

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT COUNT(*) FROM utilisateurs WHERE UtilEmail = @UtilEmail";

            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);

            UtilEmail.Value = unEmail;

            int resultat = (int)cmd.ExecuteScalar();

            _connexion.Close();

            return resultat;
        }
    }
}
