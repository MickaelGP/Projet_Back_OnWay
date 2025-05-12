using BackOnWay.Dtos.Utilisateur.Passager;

namespace BackOnWay.Metier.Utilisateur.Passager
{
    public interface IDeposerReclamationMetier
    {
        int InsertPlaintes(DeposerReclamationDto unReclamation);
    }
}
