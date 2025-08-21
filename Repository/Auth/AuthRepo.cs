using BackOnWay.Dtos.Auth;
using BackOnWay.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Repository.Auth
{
    public class AuthRepo : Connexion
    {
        public Utilisateurs Connexion(string unEmail)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilId, UtilEmail, UtilMdp, RoleLabel FROM utilisateurs " +
                "INNER JOIN roles ON UtilRole = RoleId WHERE UtilEmail = @UtilEmail";

            SqlParameter UtilEmail = cmd.Parameters.Add("@UtilEmail", SqlDbType.VarChar);

            UtilEmail.Value = unEmail;

            SqlDataReader reader = cmd.ExecuteReader();
            Utilisateurs? infoUtilisateur = null;
            while (reader.Read())
            {
                infoUtilisateur = new Utilisateurs
                {
                    UtilId = (int)reader["UtilId"],
                    UtilEmail = reader["UtilEmail"].ToString(),
                    UtilMdp = reader["UtilMdp"].ToString(),
                    Roles = new Roles()
                    {
                        RoleLabel = reader["RoleLabel"].ToString()
                    }
                };
            }
            reader.Close();

            DbDeconnecter();

            return infoUtilisateur;
        }

        public int CreateSession(Sessions unSession)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "INSERT INTO Sessions (SessionCreer, SessionFin, SessionToken, SessionUtil) VALUES (@SessionCreer, @SessionFin, @SessionToken, @SessionUtil)";

            SqlParameter SessionCreer = cmd.Parameters.Add("@SessionCreer", SqlDbType.DateTime);
            SqlParameter SessionFin = cmd.Parameters.Add("@SessionFin", SqlDbType.DateTime);
            SqlParameter SessionToken = cmd.Parameters.Add("@SessionToken", SqlDbType.NVarChar);
            SqlParameter SessionUtil = cmd.Parameters.Add("@SessionUtil", SqlDbType.Int);

            SessionCreer.Value = unSession.SessionCreer;
            SessionFin.Value = unSession.SessionFin;
            SessionToken.Value = unSession.SessionToken;
            SessionUtil.Value = unSession.SessionUtil;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter();

            return resultat;
        }
        public int Deconnexion(string unToken)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "UPDATE Sessions SET SessionActive = 0 WHERE SessionToken = @Token";

            SqlParameter Token = cmd.Parameters.Add("@Token", SqlDbType.NVarChar);

            Token.Value = unToken;

            int resultat = cmd.ExecuteNonQuery();

            DbDeconnecter() ;   

            return resultat;
        }

        public ConnexionInfoDto? GetUtilByToken(string token)
        {
            DbConnecter();

            SqlCommand cmd = _connexion.CreateCommand();

            cmd.CommandText = "SELECT UtilId, RoleLabel FROM utilisateurs " +
                "INNER JOIN roles ON RoleId = UtilRole " +
                "INNER JOIN sessions ON UtilId = SessionUtil " +
                "WHERE SessionToken = @Token AND SessionActive = 1 AND SessionFin > GETDATE()";

            SqlParameter Token = cmd.Parameters.Add("@Token", SqlDbType.NVarChar);

            Token.Value = token;

            ConnexionInfoDto? infoUtil = null;

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                infoUtil = new ConnexionInfoDto
                {
                    UtilId = (int)reader["UtilId"],
                    RoleLabel = reader["RoleLabel"].ToString()
                };
            }
            reader.Close();

            DbDeconnecter();

            return infoUtil;
        }
    }
}