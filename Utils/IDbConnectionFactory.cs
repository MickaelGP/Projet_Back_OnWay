// On inclut l'espace de noms System.Data qui contient des classes et interfaces liées
// à la gestion des connexions de base de données, des commandes SQL, et des transactions.
using System.Data;

namespace BackOnWay.Utils
{
    // Définition de l'interface IDbConnectionFactory
    // Une interface est un contrat qui définit les méthodes qu'une classe doit implémenter,
    // mais sans fournir l'implémentation des méthodes.
    public interface IDbConnectionFactory
    {
        // Méthode CreateConnection : Elle sera implémentée par des classes concrètes
        // pour créer une connexion à une base de données.
        // Elle retourne un objet de type IDbConnection, qui est une interface générique
        // représentant une connexion à une base de données.
        IDbConnection CreateConnection();
    }
}
