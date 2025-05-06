using BackOnWay.Dtos.Utilisateur;
using BackOnWay.Models;
using BackOnWay.Repository.Utilisateur;

namespace BackOnWay.Metier.Utilisateur
{
    public class AjoutCovoiturageMetier
    {
        private AjoutCovoiturageRepo _repo = new AjoutCovoiturageRepo();

        public List<GetListeVoitureDto> GetUtilVoitures(int unId)
        {
            List<Voitures> voitures = _repo.GetUtilVoitures(unId);
            List<GetListeVoitureDto> listeVoitures = new List<GetListeVoitureDto>();

            foreach (Voitures voiture in voitures)
            {
                GetListeVoitureDto unVoiture = new GetListeVoitureDto
                {
                    VoitId = voiture.VoitId,
                    ModeleNom = voiture.Modeles.ModeleNom,
                };
                listeVoitures.Add(unVoiture);
            }

            return listeVoitures;
        }

        public int InsertCovoiturage(InsertCovoiturageDto covoiturage)
        {
            int resultat =0;
            int adresseDepart;
            int adresseArriver;
            int creditUtil;
            int voitureExiste = _repo.CheckIfVoitureIsInCovoiturage(covoiturage.VoitId, covoiturage.CovoitDate);
            if (voitureExiste > 0)
            {
                //Voiture est deja prise pour un covoit à cette date
                resultat = 1;
            }
            else
            {
                creditUtil = _repo.CheckSoldeCredit(covoiturage.UtilId);
                if (creditUtil < 1)
                {
                    //Credit insufisant
                    resultat = 2;
                }
                else
                {
                    Adresses depart = new Adresses
                    {
                        AdresseNum = covoiturage.AdresseNumDepart,
                        AdresseRue = covoiturage.AdresseRueDepart,
                        AdresseCp = covoiturage.AdresseCpDepart,
                        AdresseVille = covoiturage.AdresseVilleDepart
                    };
                    Adresses arriver = new Adresses
                    {
                        AdresseNum = covoiturage.AdresseNumArriver,
                        AdresseRue = covoiturage.AdresseRueArriver,
                        AdresseCp = covoiturage.AdresseCpArriver,
                        AdresseVille = covoiturage.AdresseVilleArriver,
                    };

                    adresseDepart = _repo.InsertAdresse(depart);
                    adresseArriver = _repo.InsertAdresse(arriver);
                    Covoiturages unCovoit = new Covoiturages
                    {
                        CovoitPrix = covoiturage.CovoitPrix,
                        CovoitDate = covoiturage.CovoitDate,
                        CovoitDep = covoiturage.CovoitDep,
                        CovoitArr = covoiturage.CovoitArr,
                        CovoitStatut = "En attente",
                        CovoitFumeur = covoiturage.CovoitFumeur,
                        CovoitAnimaux = covoiturage.CovoitAnimaux,
                        CovoitMusique = covoiturage.CovoitMusique,
                        CovoitVoiture = covoiturage.VoitId,
                        DepAdresse = adresseDepart,
                        ArrAdresse = adresseArriver
                    };
                    int ajoutCovoit = _repo.InsertCovoiturage(unCovoit);
                    if (ajoutCovoit >= 1)
                    {
                        int soldeCredit = creditUtil - 1;
                        int updateCredit = _repo.UpdateSoldeCredit(covoiturage.UtilId, soldeCredit);
                        if (updateCredit > 0)
                        {
                            // Ajout covoit ok
                            resultat = 3;
                        }
                    }
                    else
                    {
                        // Erreur lors de l'ajout du covoiturage
                        resultat = 4;
                    }
                }
            }
            return resultat;
        }
    }
}
