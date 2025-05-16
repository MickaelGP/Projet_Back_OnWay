using BackOnWay.Dtos.Auth;
using BackOnWay.Models;
using BackOnWay.Repository.Auth;

namespace BackOnWay.Metier.Auth
{
    public class AuthMetier
    {
        private AuthRepo _repo = new AuthRepo();

        public ConnexionInfoDto? Authentifier(ConnexionDto uneConnexion)
        {
            string unEmail = uneConnexion.UtilEmail;
            string unMdp = uneConnexion.UtilMdp;
            Utilisateurs infoUtil = _repo.Connexion(uneConnexion.UtilEmail);
            string hashMdp = infoUtil.UtilMdp;
            ConnexionInfoDto? reponse = null;
            if ( hashMdp == string.Empty)
            {
                reponse = null;
            }
            else
            {
                bool verifMdp = BCrypt.Net.BCrypt.Verify(unMdp, infoUtil.UtilMdp);
                if (verifMdp)
                {
                    string token = Guid.NewGuid().ToString();
                    //Ajouter les donnes au modele sessions
                    Sessions unSession = new Sessions
                    {
                        SessionActive = true,
                        SessionToken = token,
                        SessionUtil = infoUtil.UtilId,
                        SessionCreer = DateTime.Now,
                        SessionFin = DateTime.Now.AddHours(1),
                    };
                    //Ajouter la session en dbb
                    int createSession = _repo.CreateSession(unSession);
                    if (createSession == 0)
                    {
                        reponse = new ConnexionInfoDto();
                    }
                    else
                    {
                        reponse = new ConnexionInfoDto
                        {
                            RoleLabel = infoUtil.Roles.RoleLabel,
                            UtilId = infoUtil.UtilId,
                            Token = token,
                        };
                    }
                }
            }
            return reponse;
        }
    }
}
