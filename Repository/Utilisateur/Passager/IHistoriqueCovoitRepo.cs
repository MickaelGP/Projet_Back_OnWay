using BackOnWay.Models;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public interface IHistoriqueCovoitRepo
    {
        List<Covoiturages> GetAllCovoitByUtilId(int unId);
    }
}
