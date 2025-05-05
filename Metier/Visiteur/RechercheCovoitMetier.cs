using BackOnWay.Dtos.Visiteur;
using BackOnWay.Models;
using BackOnWay.Repository.Visiteur;

namespace BackOnWay.Metier.Visiteur
{
    public class RechercheCovoitMetier
    {
        private RechercheCovoitRepo _repo = new RechercheCovoitRepo();

        public RechercheCovoitDto GetExactCovoiturages(RechercheCovoitDto recherche)
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
            Covoiturages resultatRecherche = _repo.GetExactCovoiturages(covoiturages);
            RechercheCovoitDto unCovoit = null;
            if (resultatRecherche != null)
            {
                unCovoit = new RechercheCovoitDto
                {
                    CovoitId = resultatRecherche.CovoitId,
                    CovoitPrix = resultatRecherche.CovoitPrix,
                    CovoitDate = resultatRecherche.CovoitDate,
                    VilleDepart = resultatRecherche.DepartAdresse.AdresseVille,
                    VilleArriver = resultatRecherche.ArriveAdresse.AdresseVille,
                    CovoitArriver = resultatRecherche.CovoitArr,
                    CovoitDepart = resultatRecherche.CovoitDep
                };
            }
            else
            {
                string codePostal = _repo.GetCodePostal(covoiturages.DepartAdresse.AdresseVille);
                if (codePostal != null)
                {
                    Covoiturages resultatAlternatif = _repo.GetAlternatifCovoit(covoiturages, "06");
                    if (resultatAlternatif != null)
                    {
                        unCovoit = new RechercheCovoitDto
                        {
                            CovoitId = resultatAlternatif.CovoitId,
                            CovoitPrix = resultatAlternatif.CovoitPrix,
                            CovoitDate = resultatAlternatif.CovoitDate,
                            VilleDepart = resultatAlternatif.DepartAdresse.AdresseVille,
                            VilleArriver = resultatAlternatif.ArriveAdresse.AdresseVille,
                            CovoitArriver = resultatAlternatif.CovoitArr,
                            CovoitDepart = resultatAlternatif.CovoitDep
                        };
                    }
                }
            }
            return unCovoit;
        }
    }
}
