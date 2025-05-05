using BackOnWay.Dtos.Auth;
using BackOnWay.Repository.Auth;

namespace BackOnWay.Metier.Auth
{
    public class AuthMetier
    {
        private AuthRepo _repo = new AuthRepo();

        public int Authentifier(ConnexionDto uneConnexion)
        {
            string unEmail = uneConnexion.UtilEmail;
            string unMdp = uneConnexion.UtilMdp;
            string hashMdp = _repo.Connexion(unEmail);
            int reponse;
            if (hashMdp == string.Empty)
            {
                reponse = 0;
            }
            else
            {
                bool verifMdp = BCrypt.Net.BCrypt.Verify(unMdp, hashMdp);
                if (verifMdp)
                {
                    reponse = 1;
                }
                else
                {
                    reponse = 0;
                }
            }
            return reponse;
        }
    }
}
