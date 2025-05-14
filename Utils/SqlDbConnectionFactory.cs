// On inclut le namespace BackOnWay.Config, où l'on trouve la classe Settings pour la gestion des paramètres,
// et Microsoft.Data.SqlClient, qui contient la classe SqlConnection pour gérer la connexion à une base de données SQL Server.
// Enfin, on inclut System.Data qui permet d'utiliser l'interface IDbConnection.
using BackOnWay.Config;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BackOnWay.Utils
{
    // Déclaration de la classe SqlDbConnectionFactory, qui implémente l'interface IDbConnectionFactory.
    // Cette classe est responsable de la création d'une connexion à une base de données SQL Server.
    public class SqlDbConnectionFactory : IDbConnectionFactory
    {
        // Le champ privé _connectionString va contenir la chaîne de connexion à la base de données.
        // Cette chaîne est généralement l'URL de la base de données, le nom d'utilisateur, le mot de passe, etc.
        private readonly string _connectionString;

        // Le constructeur de la classe SqlDbConnectionFactory.
        // Il initialise _connectionString en récupérant la chaîne de connexion à la base de données
        // à partir de la classe Settings, qui lit les configurations du fichier de configuration (généralement appsettings.json).
        public SqlDbConnectionFactory()
        {
            // Appel à la méthode GetDbString de la classe Settings pour obtenir la chaîne de connexion à la base de données.
            // La classe Settings est censée être configurée pour récupérer la chaîne de connexion depuis le fichier de configuration.
            _connectionString = new Settings().GetDbString();
        }

        // Méthode CreateConnection qui implémente la méthode de l'interface IDbConnectionFactory.
        // Elle crée et retourne une nouvelle connexion à la base de données SQL Server.
        public IDbConnection CreateConnection()
        {
            // Création d'une instance de SqlConnection en utilisant la chaîne de connexion récupérée plus tôt.
            var connection = new SqlConnection(_connectionString);

            // Ouverture de la connexion à la base de données.
            // La méthode Open() établit la connexion avec le serveur de base de données.
            connection.Open();

            // La méthode retourne la connexion ouverte sous forme d'IDbConnection,
            // ce qui permet à d'autres parties du code d'interagir avec la base de données.
            return connection;
        }
    }
}
