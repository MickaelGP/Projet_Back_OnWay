using BackOnWay.Dtos.Utilisateur.Passager;

namespace BackOnWay.Metier.Utilisateur.Passager
{
    public interface IHistoriqueCovoitMetier
    {
        List<HistoriqueCovoitDto> GetAllCovoitByUtilId(int unId);
    }
}
