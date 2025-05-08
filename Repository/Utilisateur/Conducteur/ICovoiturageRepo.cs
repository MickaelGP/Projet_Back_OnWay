using BackOnWay.Models;

namespace BackOnWay.Repository.Utilisateur.Conducteur
{
    public interface ICovoiturageRepo
    {
        List<Covoiturages> GetAllCovoitByUtilId(int unId);

        int DeleteCovoitById(int unId);

        List<Utilisateurs> GetUtilCredit(int unCovoitId);

        int UpdateCreditUtil(int unId, short unSolde);
    }
}
