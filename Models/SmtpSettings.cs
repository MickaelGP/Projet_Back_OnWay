namespace BackOnWay.Models
{
    public class SmtpSettings
    {
        /// <summary>
        /// Adresse du serveur SMTP
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Port du serveur SMTP
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Nom utilisé pour la connexion
        /// </summary>
        public string Username { get; set; }
        
        /// <summary>
        ///Mot de passe de connexion
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Nom pour envoie
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Adresse email de destination
        /// </summary>
        public string FromEmail { get; set; }
    }
}
