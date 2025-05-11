using BackOnWay.Models;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public interface IDeposerAvisRepo
    {
        Covoiturages GetUtilIdAndStatutCovoit(int unCovoitId);

        int InsertAvis(Avis unAvis);
    }
}
