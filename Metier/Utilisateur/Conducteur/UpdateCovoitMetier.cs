using BackOnWay.Dtos.Utilisateur.Conducteur;
using BackOnWay.Models;
using BackOnWay.Repository.Utilisateur.Conducteur;

namespace BackOnWay.Metier.Utilisateur.Conducteur
{
    public class UpdateCovoitMetier : IUpdateCovoitMetier
    {
        private readonly IUpdateCovoitRepo _repo;

        public UpdateCovoitMetier(IUpdateCovoitRepo repo)
        {
            _repo = repo;
        }

        public int UpdateCovoit(UpdateCovoitDto infoCovoit)
        {
            int resultat;
            string covoitStatut = _repo.GetStatutCovoit(infoCovoit.CovoitId);
            if (covoitStatut == "N/C" || covoitStatut == null || covoitStatut != "En attente")
            {
                // Impossible covoit en cours
                resultat = 1;
            }
            else
            {
                Covoiturages unCovoit = new Covoiturages
                {
                    CovoitId = infoCovoit.CovoitId,
                    CovoitDate = infoCovoit.CovoitDate,
                    CovoitDep = infoCovoit.CovoitDep,
                    CovoitArr = infoCovoit.CovoitArr,
                    CovoitPrix = infoCovoit.CovoitPrix,
                    CovoitFumeur = infoCovoit.CovoitFumeur,
                    CovoitAnimaux = infoCovoit.CovoitAnimaux,
                    CovoitMusique = infoCovoit.CovoitMusique,
                };
                int updateCovoit = _repo.UpdateCovoit(unCovoit);
                if (updateCovoit >= 1)
                {
                    //Covoit modifié
                    resultat = 2;
                }
                else
                {
                    //Erreur lors de la mise à jour
                    resultat = 3;
                }
            }
            return resultat;
        }
    }
}
