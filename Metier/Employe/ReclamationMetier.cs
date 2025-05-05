using BackOnWay.Dtos.Employe;
using BackOnWay.Models;
using BackOnWay.Repository.Employe;

namespace BackOnWay.Metier.Employe
{
    public class ReclamationMetier
    {
        private ReclamationRepo _repo = new ReclamationRepo();

        public List<PlaintesDto> GetAllPlaintes()
        {
            List<Plaintes> plaintes = _repo.GetAllPlaintes();
            List<PlaintesDto> listePlaintes = new List<PlaintesDto>();
            foreach (var item in plaintes)
            {
               PlaintesDto plainteDto = new PlaintesDto
                {
                    PlainteId= item.PlainteId,
                    PlainteDescription = item.PlainteDescription,
                    PlainteSatut = item.PlainteSatut
                };
                listePlaintes.Add(plainteDto);
            }

            return listePlaintes;
        }

        public PlainteByIdDto GetPlainteById(int id)
        {
            Plaintes plainte = _repo.GetPlainteById(id);
            if (plainte == null)
            {
                return null;
            }
            else
            {
                PlainteByIdDto unePlainte = new PlainteByIdDto
                {
                    PlainteId = plainte.PlainteId,
                    PlainteDescription = plainte.PlainteDescription,
                    PlainteSatut = plainte.PlainteSatut,
                    UtilEmail = plainte.Reservations?.Utilisateurs.UtilEmail,
                    UtilNom = plainte.Reservations?.Utilisateurs.UtilNom,
                    ResaDate = plainte.Reservations.ResaDate
                };

                return unePlainte;

            }

        }

        public bool UpdateStatutPlainte(PlainteUpdateDto unePlainte)
        {
            Plaintes plainte = new Plaintes
            {
                PlainteId = unePlainte.PlainteId,
                PlainteSatut = unePlainte.PlainteSatut,
            };
            int resultatModif = _repo.UpdateStatutPlainte(plainte);
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
