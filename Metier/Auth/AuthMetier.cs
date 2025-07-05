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
            if (infoUtil == null)
            {
                return new ConnexionInfoDto { MessageErreur = "Utilisateur introuvable" };
            }

            string hashMdp = infoUtil.UtilMdp;
            ConnexionInfoDto? reponse = null;
            if ( hashMdp == string.Empty)
            {
                return new ConnexionInfoDto { MessageErreur = "Mot de passe incorrect" };
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
                    //Ajouter la session en bdd
                    int createSession = _repo.CreateSession(unSession);
                    if (createSession == 0)
                    {
                        return new ConnexionInfoDto { MessageErreur = "Erreur de session" };
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
                else
                {
                    return new ConnexionInfoDto { MessageErreur = "Mot de passe incorrect" };
                }
            }
            return reponse;
        }

        public int Deconnexion(string unToken)
        {
            int reponse;
            int resultat = _repo.Deconnexion(unToken);

            if (resultat == 0)
            {
                //Un probléme
                reponse = 0;
            }
            else
            {
                reponse = 1;
            }
            return reponse;
        }
    }
}
