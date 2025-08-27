# OnWay – Application de Covoiturage
## Présentation
**OnWay** est une application web de covoiturage développée dans le cadre d’un projet pour l'obtention du tire de CDA(concepteur dévellopeur d'aplication).
Elle permet aux utilisateurs de créer un compte, de proposer ou de réserver des trajets, et de gérer leurs interactions de manière sécurisée.

Le projet repose sur une architecture client-serveur :

- Backend : développé en C#/.NET, expose une API RESTful pour gérer l’ensemble des échanges.
- Frontend : conçu avec Next.js 15 et React.js, utilise Bootstrap et du CSS personnalisé pour l’interface utilisateur. 
  - Le code source du frontend est disponible dans un dépôt séparé : `https://github.com/MickaelGP/Projet_Front_OnWay`
## Architecture
### Backend (.NET / C#)
- **Repository** : gestion des accès à la base de données via T-SQL.

- **Modèles** : représentation des entités (ex. : Conducteur, Utilisateur, Réservation).

- **DTO** : objets de transfert pour sécuriser et valider les données échangées.

- **Logique métier** : règles de gestion (réservations, solde utilisateur, disponibilité des places).

- **Contrôleurs** : points d’entrée de l’API RESTful.

- **Sécurité** :
    - Hashage des mots de passe avec BCrypt.

    - Authentification par token de session.

    - Middleware de vérification des accès (HTTP 401 en cas d’accès non autorisé).

    - Requêtes SQL préparées (paramétrées) pour éviter les injections.
## Fonctionnalités principales
- Inscription et connexion sécurisées.

- Création de trajets par les conducteurs.

- Réservation de places par les passagers.

- Gestion des comptes par l'administrateur
## Technologies utilisées
### Backend
- C# (.NET)

- T-SQL (SQL Server)

- BCrypt (sécurité des mots de passe)
## Sécurité
- Hashage des mots de passe **(BCrypt)**.

- Authentification par tokens.

- Middleware d’autorisation.

- Cookies sécurisés **(HTTP-only)**.

- Protection contre les attaques **XSS** et injections **SQL**.
## Prérequis
Avant de lancer l'application, assurez-vous d’avoir installé les dépendances suivantes via NuGet :
- `Microsoft.Data.SqlClient`
- `Microsoft.Extensions.Configuration.EnvironmentVariables`
- `Microsoft.Extensions.Configuration.Json`
- `Microsoft.Extensions.Configuration.Binder`
- `BCrypt.Net-Next`
- `MailKit `(pour l’envoi d’emails lors de la création de compte ou via le formulaire de contact)
## Installation
Suivez les étapes ci-dessous pour installer et exécuter l’application localement :

1.  Cloner le dépôt :
```bash
git clone https://github.com/MickaelGP/Projet_Back_OnWay
cd Projet_Back_OnWay
```
2.  Créer la base de données :
```plaintext
 Utilisez le script SQL fourni dans le dépôt (script.sql) pour créer la base de données.
```
3.  Configurer l'application :
    - Créez un fichier appsettings.json à la racine du projet avec le contenu suivant :
```json
 {
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Settings": {
    "DbConnectionString": "Vôtre chaine de connexion"
  },
  "SmtpSettings": {
    "Host": "Votre hôte",
    "Port": Le port du serveur,
    "Username": "nom d'utilisateur",
    "Password": "mot de passe",
    "FromName": "Nom",
    "FromEmail": "adresse@email.com"
  },
}
```
## Lancer l'application
```bash
dotnet run
```
## Auteur
- MickaelGP - https://github.com/MickaelGP

