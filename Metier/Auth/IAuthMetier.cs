using BackOnWay.Dtos.Auth;
using BackOnWay.Models;

namespace BackOnWay.Metier.Auth
{
    public interface IAuthMetier
    {
        ResultatAuthDto Connexion(ConnexionDto info);
    }
}
