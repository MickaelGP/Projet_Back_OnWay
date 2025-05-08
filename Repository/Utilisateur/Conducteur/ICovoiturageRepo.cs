using BackOnWay.Models;

namespace BackOnWay.Repository.Utilisateur.Conducteur
{
    public interface ICovoiturageRepo
    {
        List<Covoiturages> GetAllCovoitByUtilId(int unId);
    }
}
