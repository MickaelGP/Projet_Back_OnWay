using BackOnWay.Dtos.Employe;
using BackOnWay.Models;
using BackOnWay.Repository.Employe;

namespace BackOnWay.Metier.Employe
{
    public class ValidAvisMetier
    {
        private ValidAvisRepo _repo = new ValidAvisRepo();

        public List<AvisDto> GetAllAvis()
        {
            List<Avis> avis = _repo.GetAllAvis();
            List<AvisDto> listeAvis = new List<AvisDto>();
            foreach (Avis item in avis)
            {
                AvisDto avisDto = new AvisDto
                {
                    AvisId = item.AvisId,
                    AvisStatut = item.AvisStatut,
                    AvisTitre = item.AvisTitre,
                };
                listeAvis.Add(avisDto);
            }
            return listeAvis;
        }

        public AvisByIdDto GetAvisById(int unId)
        {
            Avis avis = _repo.SelectAvisById(unId);
            AvisByIdDto unAvis = null;
            if (avis != null)
            {
                unAvis = new AvisByIdDto
                {
                    AvisId = avis.AvisId,
                    AvisCom = avis.AvisCom,
                    AvisNote = avis.AvisNote,
                    AvisStatut = avis.AvisStatut,
                    AvisTitre = avis.AvisTitre,
                    NomDestinataire = avis.UtilisateurRecu.UtilNom,
                    NomExpediteur = avis.UtilisateurDepo.UtilNom,
                };
            }
            return unAvis;
        }

        public bool UpdateStatutAvis(AvisUpdateDto unAvis)
        {
            Avis avis = new Avis
            {
                AvisId = unAvis.AvisId,
                AvisStatut = unAvis.AvisStatut,
            };
            int resultatModif = _repo.UpdateStatutAvis(avis);
            bool resultat;

            if (resultatModif > 0)
            {
                resultat = true;
            }
            else
            {
                resultat = false;
            }
            return resultat;
        }
    }
}
