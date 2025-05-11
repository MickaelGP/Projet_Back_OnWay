

using BackOnWay.Dtos.Utilisateur.Passager;

namespace BackOnWay.Metier.Utilisateur.Passager
{
    public interface IDeposerAvisMetier
    {
        int InsertAvis(DeposerAvisDto unAvis);
    }
}
