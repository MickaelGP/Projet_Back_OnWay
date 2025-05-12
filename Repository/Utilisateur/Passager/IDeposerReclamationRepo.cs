using BackOnWay.Models;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public interface IDeposerReclamationRepo
    {
        Reservations GetInfoResa(int unUtilId, int unCovoitId);

        int CheckIfPlainteExist(int unUtilId, int unCovoitId);

        int InsertPlaintes(Plaintes unPlainte);
    }
}
