using BackOnWay.Dtos.Utilisateur.Passager;
using BackOnWay.Metier.Utilisateur.Passager;
using BackOnWay.Models;

namespace BackOnWay.Repository.Utilisateur.Passager
{
    public class DeposerAvisMetier : IDeposerAvisMetier
    {
        private readonly IDeposerAvisRepo _repo;

        public DeposerAvisMetier(IDeposerAvisRepo repo)
        {
            _repo = repo;
        }

        public int InsertAvis(DeposerAvisDto unAvis)
        {
            int resultat;
            Covoiturages unCoVoit = _repo.GetUtilIdAndStatutCovoit(unAvis.CovoitId);
            if (unCoVoit.CovoitStatut != "Terminé")
            {
                //Covoit non terminé
                resultat = 1;
            }
            else
            {
                if (unAvis.AvisNote < 1)
                {
                    unAvis.AvisNote = 0;
                }
                Avis avis = new Avis
                {
                    AvisTitre = unAvis.AvisTitre,
                    AvisCom = unAvis.AvisCom,
                    AvisNote = unAvis.AvisNote,
                    AvisDeposer = unAvis.AvisDeposer,
                    AvisStatut = "En attente",
                    AvisRecu = unCoVoit.Voiture.Conducteurs.Utilisateur.UtilId
                };

                int reponse = _repo.InsertAvis(avis);
                if (reponse < 1)
                {
                    //Probléme d'ajout d'avis
                    resultat = 2;
                }
                else
                {
                    //Avis Ajouté
                    resultat = 3;
                }
            }
            return resultat;
        }
    }
}