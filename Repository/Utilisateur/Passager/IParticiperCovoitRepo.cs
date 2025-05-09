using BackOnWay.Models;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public interface IParticiperCovoitRepo
    {
        int GetNbSiegeRestant(int unCovoitId);

        int GetSoldeUtil(int unId);

        int UpdateSoleCreditUtil(int unId, int unSolde);

        int InsertReservationCovoit(Reservations uneReservation);
    }
}
