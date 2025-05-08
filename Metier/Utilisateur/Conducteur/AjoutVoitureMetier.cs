using BackOnWay.Dtos.Utilisateur.Conducteur;
using BackOnWay.Models;
using BackOnWay.Repository.Utilisateur.Conducteur;

namespace BackOnWay.Metier.Utilisateur.Conducteur
{
    public class AjoutVoitureMetier
    {
        private AjoutVoitureRepo _repo = new AjoutVoitureRepo();

        /// <summary>
        /// Gère la récupération de tous les modèles de voitures.
        /// </summary>
        /// <returns>Liste de modéles de voitures</returns>
        public List<Modeles> GetAllModeles()
        {
            List<Modeles> listModeles = _repo.GetAllModeles();

            return listModeles;
        }

        /// <summary>
        /// Gère la récupération de toutes les couleurs.
        /// </summary>
        /// <returns>Liste de couleurs</returns>
        public List<Couleurs> GetAllCouleurs()
        {
            List<Couleurs> listeCouleurs = _repo.GetAllCouleurs();

            return listeCouleurs;
        }

        /// <summary>
        /// Gère la logique métier liée à l'ajout d'une voiture pour un utilisateur.
        /// </summary>
        /// <param name="unVoiture">Modèle Voitures</param>
        /// <returns>True ou False</returns>
        public bool InsertVoiture (InsertVoitureDto unVoiture)
        {
            bool resultat;

            int isConducteur = _repo.CheckUtilIsConduc(unVoiture.UtilId);
            if (isConducteur < 0)
            {
                int insertConduc = _repo.InsertConduc(unVoiture.UtilId);
                if(insertConduc < 0)
                {
                    resultat = false;
                }
                else
                {
                    Voitures voitures = new Voitures
                    {
                        VoitDateImat = unVoiture.VoitDateImat,
                        VoitCouleur = unVoiture.VoitCouleur,
                        VoitEnergie = unVoiture.VoitEnergie,
                        VoitPlaque = unVoiture.VoitPlaque,
                        VoitModele = unVoiture.VoitModele,
                        VoitNbSiege = Convert.ToByte(unVoiture.VoitNbSiege),
                        VoitConduc = insertConduc,
                    };
                    int insertVoiture = _repo.InsertVoiture(voitures);

                    if(insertVoiture < 0)
                    {
                        resultat = false;
                    }
                    resultat = true;
                }
            }
            else
            {
                Voitures voitures = new Voitures
                {
                    VoitDateImat = unVoiture.VoitDateImat,
                    VoitCouleur = unVoiture.VoitCouleur,
                    VoitEnergie = unVoiture.VoitEnergie,
                    VoitPlaque = unVoiture.VoitPlaque,
                    VoitModele = unVoiture.VoitModele,
                    VoitNbSiege = Convert.ToByte(unVoiture.VoitNbSiege),
                    VoitConduc = isConducteur
                };
                int insertVoiture = _repo.InsertVoiture(voitures);

                if (insertVoiture < 0)
                {
                    resultat = false;
                }
                resultat = true;
            }
            return resultat;
        }
    }
}
