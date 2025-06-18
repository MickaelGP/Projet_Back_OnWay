using BackOnWay.Dtos.Utilisateur.Conducteur;

namespace BackOnWay.Metier.Utilisateur.Conducteur
{
    public interface IUpdateCovoitMetier
    {
        int UpdateCovoit(UpdateCovoitDto infoCovoit);

        UpdateCovoitDto GetInfoCovoitById(int unId);
    }
}
