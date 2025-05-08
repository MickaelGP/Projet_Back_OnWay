using BackOnWay.Dtos.Utilisateur.Conducteur;
using BackOnWay.Models;
using BackOnWay.Repository.Utilisateur.Conducteur;


namespace BackOnWay.Metier.Utilisateur.Conducteur
{
    public class CovoiturageMetier : ICovoiturageMetier 
    {
        private readonly ICovoiturageRepo _repo;

        public CovoiturageMetier(ICovoiturageRepo repo)
        {
            _repo = repo;
        }

        public List<GetListeCovoitByUtilIdDto> GetAllCovoitByUtilId(int unId)
        {
            List<Covoiturages> covoiturages = _repo.GetAllCovoitByUtilId(unId);
            List<GetListeCovoitByUtilIdDto> listeCovoits = new List<GetListeCovoitByUtilIdDto>();

            foreach (Covoiturages unCovoit in covoiturages)
            {
                GetListeCovoitByUtilIdDto unUtilCovoit = new GetListeCovoitByUtilIdDto
                {
                    CovoitId = unCovoit.CovoitId,
                    CovoitArr = unCovoit.CovoitArr,
                    CovoitDate = unCovoit.CovoitDate,
                    CovoitDep = unCovoit.CovoitDep,
                    ArriveVille = unCovoit.ArriveAdresse.AdresseVille,
                    DepartVille = unCovoit.DepartAdresse.AdresseVille,
                };
                listeCovoits.Add(unUtilCovoit);
            }

            return listeCovoits;
        }
    }
}
