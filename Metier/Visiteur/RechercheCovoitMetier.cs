using BackOnWay.Dtos.Visiteur;
using BackOnWay.Models;
using BackOnWay.Repository.Visiteur;

namespace BackOnWay.Metier.Visiteur
{
    public class RechercheCovoitMetier
    {
        private RechercheCovoitRepo _repo = new RechercheCovoitRepo();

        public List<RechercheCovoitDto> GetExactCovoiturages(RechercheCovoitDto recherche)
        {
            Covoiturages covoiturages = new Covoiturages
            {
                CovoitDate = recherche.CovoitDate,
                DepartAdresse = new Adresses
                {
                    AdresseVille = recherche.VilleDepart
                },
                ArriveAdresse = new Adresses
                {
                    AdresseVille = recherche.VilleArriver
                }
            };
            List<Covoiturages> resultatRecherche = _repo.GetExactCovoiturages(covoiturages);
            List<RechercheCovoitDto> Covoits = new List<RechercheCovoitDto>();
            if (resultatRecherche.Count > 0)
            {
                foreach (var covoit in resultatRecherche)
                {
                    RechercheCovoitDto unCovoit = new RechercheCovoitDto
                    {
                        CovoitId = covoit.CovoitId,
                        CovoitPrix = covoit.CovoitPrix,
                        CovoitDate = covoit.CovoitDate,
                        VilleDepart = covoit.DepartAdresse.AdresseVille,
                        VilleArriver = covoit.ArriveAdresse.AdresseVille,
                        CovoitArriver = covoit.CovoitArr,
                        CovoitDepart = covoit.CovoitDep
                    };
                    Covoits.Add(unCovoit);
                }
            }
            else
            {
                string codePostal = _repo.GetCodePostal(covoiturages.DepartAdresse.AdresseVille);
                if (codePostal != null)
                {
                    List<Covoiturages> resultatAlternatif = _repo.GetAlternatifCovoit(covoiturages, codePostal);

                    if (resultatAlternatif.Count > 0)
                    {
                        foreach (var covoitAlternatif in resultatAlternatif)
                        {
                            RechercheCovoitDto unCovoit = new RechercheCovoitDto
                            {
                                CovoitId = covoitAlternatif.CovoitId,
                                CovoitPrix = covoitAlternatif.CovoitPrix,
                                CovoitDate = covoitAlternatif.CovoitDate,
                                VilleDepart = covoitAlternatif.DepartAdresse.AdresseVille,
                                VilleArriver = covoitAlternatif.ArriveAdresse.AdresseVille,
                                CovoitArriver = covoitAlternatif.CovoitArr,
                                CovoitDepart = covoitAlternatif.CovoitDep
                            };
                            Covoits.Add(unCovoit);
                        }
                    }
                }
            }
            return Covoits;
        }
    }
}
