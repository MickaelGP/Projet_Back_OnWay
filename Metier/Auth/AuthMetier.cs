using BackOnWay.Dtos.Auth;
using BackOnWay.Models;
using BackOnWay.Repository.Auth;
using BackOnWay.Utils;

namespace BackOnWay.Metier.Auth
{
    public class AuthMetier : IAuthMetier
    {
        private readonly IAuthRepo _repo;
        private GenerateTokens _tokenGenerator;
        public AuthMetier(IAuthRepo repo, GenerateTokens tokenGenerator)
        {
            _repo = repo;
            _tokenGenerator = tokenGenerator;
        }

        public ResultatAuthDto Connexion(ConnexionDto info)
        {
            string unEmail = info.UtilEmail;
            string unMdp = info.UtilMdp;
            string hashMdp = _repo.CheckMdpByEmailUtil(unEmail);
            ResultatAuthDto? infoUtil = null;
            if (hashMdp == string.Empty)
            {
                //Email ou mdp invalide
                infoUtil = null;
            }
            else
            {
                bool verifMdp = BCrypt.Net.BCrypt.Verify(unMdp, hashMdp);
                if (verifMdp)
                {
                    Utilisateurs unUtil = _repo.GetRoleAndUtilId(unEmail);
                    if (unUtil != null)
                    {
                        string token = _tokenGenerator.GenerateToken(unUtil.UtilId, unUtil.Roles.RoleLabel);
                        infoUtil = new ResultatAuthDto { token = token };
                    }
                }
            }
            return infoUtil;
        }

        //public int Authentifier(ConnexionDto uneConnexion)
        //{
        //    string unEmail = uneConnexion.UtilEmail;
        //    string unMdp = uneConnexion.UtilMdp;
        //    string hashMdp = _repo.Connexion(unEmail);
        //    int reponse;
        //    if (hashMdp == string.Empty)
        //    {
        //        reponse = 0;
        //    }
        //    else
        //    {
        //        bool verifMdp = BCrypt.Net.BCrypt.Verify(unMdp, hashMdp);
        //        if (verifMdp)
        //        {
        //            reponse = 1;
        //        }
        //        else
        //        {
        //            reponse = 0;
        //        }
        //    }
        //    return reponse;
        //}
    }
}
