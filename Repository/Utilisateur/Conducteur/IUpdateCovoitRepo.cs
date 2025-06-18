using BackOnWay.Models;

namespace BackOnWay.Repository.Utilisateur.Conducteur
{
    public interface IUpdateCovoitRepo
    {
        string GetStatutCovoit(int unCovoitId);

        int UpdateCovoit(Covoiturages infoCovoit);

       Covoiturages GetInfoCovoitById(int unId);
    }
}
