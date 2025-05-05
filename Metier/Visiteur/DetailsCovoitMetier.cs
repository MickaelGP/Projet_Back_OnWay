using BackOnWay.Dtos.Visiteur;
using BackOnWay.Models;
using BackOnWay.Repository.Visiteur;

namespace BackOnWay.Metier.Visiteur
{
    public class DetailsCovoitMetier
    {
        private DetailsCovoitRepo _repo = new DetailsCovoitRepo();

        public DetailsCovoitDto GetInfoCovoit(int unId)
        {
            Covoiturages covoiturages = _repo.GetInfoCovoit(unId);
            DetailsCovoitDto infoCovoit = null;

            if (covoiturages != null)
            {
                infoCovoit = new DetailsCovoitDto
                {
                    CovoitId = covoiturages.CovoitId,
                    CovoitPrix = covoiturages.CovoitPrix,
                    CovoitDate = covoiturages.CovoitDate,
                    CovoitDep = covoiturages.CovoitDep,
                    CovoitArr = covoiturages.CovoitArr,
                    CovoitAnimaux = covoiturages.CovoitAnimaux,
                    CovoitFumeur = covoiturages.CovoitFumeur,
                    CovoitMusique = covoiturages.CovoitMusique,
                    DepartNum = covoiturages.DepartAdresse.AdresseNum,
                    DepartRue = covoiturages.DepartAdresse.AdresseRue,
                    DepartVille = covoiturages.DepartAdresse.AdresseVille,
                    ArriverNum = covoiturages.ArriveAdresse.AdresseNum,
                    ArriveRue = covoiturages.ArriveAdresse.AdresseRue,
                    ArriveVille = covoiturages.ArriveAdresse.AdresseVille,
                    VoitEnergie = covoiturages.Voiture.VoitEnergie,
                    VoitCouleur = covoiturages.Voiture.Couleurs.CouleurNom,
                    VoitModele = covoiturages.Voiture.Modeles.ModeleNom,
                    VoitMarque = covoiturages.Voiture.Modeles.Marque.MarqueNom,
                    ConducId = covoiturages.Voiture.Conducteurs.Utilisateur.UtilId,
                    ConducPseudo = covoiturages.Voiture.Conducteurs.Utilisateur.UtilPseudo,
                    Note = covoiturages.Voiture.Conducteurs.Utilisateur.Note,
                    NbCom = covoiturages.Voiture.Conducteurs.Utilisateur.NombreCom
                };

            }
            return infoCovoit;
        }
        public List<AvisRecuDto> GetAvisByConducId(int unId)
        {
            List<Avis> reponse = _repo.GetAvisByConducId(unId);
            List<AvisRecuDto> listeAvis = new List<AvisRecuDto>();
            foreach (Avis recu in reponse)
            {
                AvisRecuDto avisRecuDto = new AvisRecuDto
                {
                    AvisCom = recu.AvisCom,
                    AvisNote = recu.AvisNote,
                    AvisTitre = recu.AvisTitre,
                    UtilPseudo = recu.UtilisateurDepo.UtilPseudo
                };
                listeAvis.Add(avisRecuDto);
            }
            return listeAvis;
        }
    }
}
