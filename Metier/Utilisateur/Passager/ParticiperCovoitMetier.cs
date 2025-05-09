using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Models;
using BackOnWay.Repository.Utilisateur.Passager;

namespace BackOnWay.Metier.Utilisateur.Passager
{
    public class ParticiperCovoitMetier : IParticiperCovoitMetier
    {
        private readonly IParticiperCovoitRepo _repo;

        public ParticiperCovoitMetier(IParticiperCovoitRepo repo)
        {
            _repo = repo;
        }
        public int InsertReservationCovoit(ParticiperCovoitDto unReservation)
        {
            int nbSiegeRestant = _repo.GetNbSiegeRestant(unReservation.ResaCovoit);
            int nbSiegeDesirer = unReservation.ResaNbSiege;
            int soldeRestant;
            int reponse = 0;
            if (nbSiegeDesirer > nbSiegeRestant)
            {
                //Reservation imposible nombre de place insufisante.
                reponse = 1;
            }
            else
            {
                soldeRestant = _repo.GetSoldeUtil(unReservation.ResaUtil);
                if (soldeRestant < 1)
                {
                    //Solde insufisant.
                    reponse = 2;
                }
                else
                {
                    Reservations uneResa = new Reservations
                    {
                        ResaUtil = unReservation.ResaUtil,
                        ResaCovoit = unReservation.ResaCovoit,
                        ResaDate = unReservation.ResaDate,
                        ResaNbSiege = unReservation.ResaNbSiege,
                        ResaStatut = "En attente"
                    };
                    int resultat = _repo.InsertReservationCovoit(uneResa);
                    if (resultat > 0)
                    {
                        int nouveauSolde = soldeRestant - 1;
                        int updateSolde = _repo.UpdateSoleCreditUtil(unReservation.ResaUtil, nouveauSolde);
                        if (updateSolde > 0)
                        {
                            // Ajout reussi.
                            reponse = 3;
                        }
                        //Erreur lors de l'ajout
                        reponse = 4;
                    }
                }
            }
            return reponse;
        }
    }
}
