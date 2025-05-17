using BackOnWay.Dtos.Auth;
using BackOnWay.Models;
using BackOnWay.Repository.Auth;

namespace BackOnWay.Middlewares
{
    // Le middleware SessionMiddleware est utilisé pour intercepter toutes les requêtes HTTP
    // et vérifier si un utilisateur est bien authentifié via un token.
    public class SessionMiddleware
    {
        // Delegate représentant la prochaine étape dans le pipeline HTTP (ex: le contrôleur)
        private readonly RequestDelegate _next;

        // Constructeur qui reçoit et stocke le "next" delegate
        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Méthode principale appelée à chaque requête entrante
        public async Task Invoke(HttpContext context)
        {
            // Récupère le chemin de la requête (ex: /connexion, /profil)
            var path = context.Request.Path.Value?.ToLower();

            // Si la requête est pour /connexion ou /inscription, on ne vérifie pas le token
            // car ce sont des routes publiques
            if (path == "/connexion" || path == "/inscription")
            {
                await _next(context);
                return;
            }

            // On tente de récupérer le token d'authentification dans  dans les cookies
            string token = context.Request.Cookies["session_token"];

            // Si aucun token n'a été trouvé, on bloque la requête
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;  // Erreur 401 : non autorisé
                await context.Response.WriteAsync("Token manquant."); // Message d'erreur retourné au client
                return;
            }

            // On crée une instance du repository pour accéder à la base de données
            AuthRepo repo = new AuthRepo();

            // On récupère les informations de l'utilisateur liées à ce token
            ConnexionInfoDto infoUtilisateur = repo.GetUtilByToken(token);

            // Si aucune session valide n'est trouvée, on refuse l'accès
            if (infoUtilisateur == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized; // Erreur 401
                await context.Response.WriteAsync("Token invalide."); // Message explicite
                return;
            }

            // Si tout est bon, on stocke les informations utilisateur dans le contexte
            // Cela permet aux contrôleurs d'y accéder plus tard dans la requête
            context.Items["Utilisateur"] = infoUtilisateur;

            // On passe la main à l’étape suivante du pipeline (ex: contrôleur)
            await _next(context);
        }
    }
}