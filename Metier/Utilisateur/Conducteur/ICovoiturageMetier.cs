using BackOnWay.Dtos.Utilisateur.Conducteur;

namespace BackOnWay.Metier.Utilisateur.Conducteur
{
    public interface ICovoiturageMetier
    {
        List<GetListeCovoitByUtilIdDto> GetAllCovoitByUtilId(int unId);
    }
}
