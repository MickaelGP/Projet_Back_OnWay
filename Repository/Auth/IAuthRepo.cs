using BackOnWay.Models;

namespace BackOnWay.Repository.Auth
{
    public interface IAuthRepo
    {
        string CheckMdpByEmailUtil(string email);

        Utilisateurs GetRoleAndUtilId(string email);
    }
}
