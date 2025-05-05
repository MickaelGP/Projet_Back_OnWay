using BackOnWay.Config;
using BackOnWay.Dtos.Admin;
using BackOnWay.Dtos.Visiteur;
using BackOnWay.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace BackOnWay.Utils
{
    public class SendMailUtils
    {
        private SmtpSettings _smtpSettings = new Settings().GetSmtpSettings();

        private MimeMessage message = new MimeMessage();

        public void SendEmailContact(ContactDto infoContact)
        {
            message.From.Add(new MailboxAddress(this._smtpSettings.FromName, this._smtpSettings.FromEmail));
            message.ReplyTo.Add(new MailboxAddress(infoContact.Nom, infoContact.AdresseEmail));
            message.Subject = infoContact.Titre;
            message.To.Add(new MailboxAddress("", ""));

            message.Body = new TextPart("html")
            {
                Text = $@"
                        <html>
                            <body>
                                <h1>Message de :  {infoContact.Nom},</h1>
                                 <h3>Le message :</h3>
                                <p>{infoContact.Message}</p>
                                 <h3>Numéro de téléphone :</h3>
                                 <p>{infoContact.Telephone}</p>
                                 <h3>L'adresse email :</h3>
                                 <p>{infoContact.AdresseEmail}</p>
                            </body>
                        </html>
                    "
            };

            using (var client = new SmtpClient())
            {
                client.Connect(this._smtpSettings.Host, this._smtpSettings.Port, true);

                client.Authenticate(this._smtpSettings.Username, _smtpSettings.Password);

                client.Send(message);

                client.Disconnect(true);
            }
            ;
        }

        public void SendEmailnewCompteUtil(CreateCompteDto infoMail)
        {
            message.From.Add(new MailboxAddress(this._smtpSettings.FromName, this._smtpSettings.FromEmail));
            message.Subject = "Nouveau compte créer";
            message.To.Add(new MailboxAddress(infoMail.UtilPseudo, infoMail.UtilEmail));

            message.Body = new TextPart("html")
            {
                Text = $@"
                        <html>
                            <body>
                                <h1>Bonjour :  {infoMail.UtilPseudo},</h1>
                                <p> Votre compte à bien été créer.</p>
                                <p>Merci de votre confiance</p>
                            </body>
                        </html>
                    "
            };

            using (var client = new SmtpClient())
            {
                client.Connect(this._smtpSettings.Host, this._smtpSettings.Port, true);

                client.Authenticate(this._smtpSettings.Username, _smtpSettings.Password);

                client.Send(message);

                client.Disconnect(true);
            }
            ;
        }

        public void SendEmailNewEmploye(AddEmployeDto infoEmploye)
        {
            message.From.Add(new MailboxAddress(this._smtpSettings.FromName, this._smtpSettings.FromEmail));
            message.Subject = "Nouveau compte créer";
            message.To.Add(new MailboxAddress(infoEmploye.UtilNom, infoEmploye.UtilEmail));

            message.Body = new TextPart("html")
            {
                Text = $@"
                        <html>
                            <body>
                                <h1>Bonjour :  {infoEmploye.UtilNom}, {infoEmploye.UtilPrenom}</h1>
                                <p> Votre compte vient d'être créer.</p>
                                <p>Merci de vous rapprocher de l'administrateur pour obtenir votre mot de passe de connexion.</p>
                            </body>
                        </html>
                    "
            };

            using (var client = new SmtpClient())
            {
                client.Connect(this._smtpSettings.Host, this._smtpSettings.Port, true);

                client.Authenticate(this._smtpSettings.Username, _smtpSettings.Password);

                client.Send(message);

                client.Disconnect(true);
            }
            ;
        }
    }
}
