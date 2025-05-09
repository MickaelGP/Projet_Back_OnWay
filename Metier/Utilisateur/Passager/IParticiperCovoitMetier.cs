using BackOnWay.Dtos.Utilisateur.Passager;

namespace BackOnWay.Metier.Utilisateur.Passager
{
    public interface IParticiperCovoitMetier
    {
        int InsertReservationCovoit(ParticiperCovoitDto unReservation);
    }
}
