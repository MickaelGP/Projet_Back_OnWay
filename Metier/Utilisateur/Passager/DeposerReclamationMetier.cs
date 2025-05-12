using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Models;
using BackOnWay.Repository.Utilisateur.Passager;

namespace BackOnWay.Metier.Utilisateur.Passager
{
    public class DeposerReclamationMetier : IDeposerReclamationMetier
    {
        private readonly IDeposerReclamationRepo _repo;

        public DeposerReclamationMetier(IDeposerReclamationRepo repo)
        {
            _repo = repo;
        }

        public int InsertPlaintes(DeposerReclamationDto unReclamation)
        {
            Reservations unReservation = _repo.GetInfoResa(unReclamation.UtilId, unReclamation.CovoitId);
            int reponse;
            if(unReservation.Covoiturages.CovoitStatut != "Terminé")
            {
                //Impossible de faire une réclamation covoit non terminer
                reponse = 1;
            }
            else
            {
                int plainteExist = _repo.CheckIfPlainteExist(unReclamation.UtilId, unReclamation.CovoitId);
                if(plainteExist > 0)
                {
                    //Impossible une plainte est dèjà déposée
                    reponse = 2;
                }
                else
                {
                    Plaintes unPlainte = new Plaintes
                    {
                        PlainteDescription = unReclamation.PlainteDescription,
                        PlainteSatut = "En attente",
                        PlainteResa = unReservation.ResaId,
                    };
                    int resulat = _repo.InsertPlaintes(unPlainte);
                    if (resulat > 0) {
                        //Plainte ajouté
                        reponse = 3;
                    }
                    else
                    {
                        reponse = 4;
                    }
                }
            }
            return reponse;
        }
    }
}
