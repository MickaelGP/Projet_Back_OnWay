using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Models;
using BackOnWay.Repository.Utilisateur.Passager;

namespace BackOnWay.Metier.Utilisateur.Passager
{
    public class HistoriqueCovoitMetier : IHistoriqueCovoitMetier
    {
        private readonly IHistoriqueCovoitRepo _repo;

        public HistoriqueCovoitMetier(IHistoriqueCovoitRepo repo)
        {
            _repo = repo;
        }

        public List<HistoriqueCovoitDto> GetAllCovoitByUtilId(int unId)
        {
            List<Covoiturages> covoiturages = _repo.GetAllCovoitByUtilId(unId);
            List<HistoriqueCovoitDto> listeCovoits = new List<HistoriqueCovoitDto>();

            foreach (Covoiturages unCovoit in covoiturages)
            {
                HistoriqueCovoitDto unUtilCovoit = new HistoriqueCovoitDto
                {
                    CovoitId = unCovoit.CovoitId,
                    CovoitArr = unCovoit.CovoitArr,
                    CovoitDate = unCovoit.CovoitDate,
                    CovoitDep = unCovoit.CovoitDep,
                    Arriver = unCovoit.ArriveAdresse.AdresseVille,
                    Depart = unCovoit.DepartAdresse.AdresseVille,
                    CovoitStatut = unCovoit.CovoitStatut,
                };
                listeCovoits.Add(unUtilCovoit);
            }

            return listeCovoits;
        }
    }
}
