# Guide d'installation de Docker sur Mac

Docker est une plateforme de conteneurisation qui permet d'exécuter des applications dans des environnements isolés. Cette solution est devenue très populaire pour le développement et le déploiement d'applications modernes. Dans ce guide, nous allons voir comment installer Docker sur un Mac.

## Étape 1 : Télécharger Docker Desktop

La première étape consiste à télécharger Docker Desktop sur le site officiel de Docker :

1. Ouvrir un navigateur web
2. Aller sur le site officiel de Docker : https://www.docker.com/products/docker-desktop
3. Cliquer sur le bouton de téléchargement

## Étape 2 : Installer Docker Desktop

Une fois le fichier d'installation téléchargé, il suffit de double-cliquer dessus et de suivre les instructions à l'écran pour installer Docker Desktop.

## Étape 3 : Vérifier l'installation

Après l'installation, il est important de vérifier que Docker fonctionne correctement :

1. Ouvrir un terminal
2. Entrer la commande suivante : docker version
3. Vérifier que la version de Docker est affichée

## Étape 4 : Utiliser Docker

Une fois Docker installé, vous pouvez commencer à utiliser les conteneurs Docker. Vous pouvez par exemple télécharger une image Docker existante et la lancer :

1. Ouvrir un terminal
2. Entrer la commande suivante : docker run hello-world
3. Attendre que l'image soit téléchargée et exécutée
4. Vérifier que le message "Hello from Docker!" est affiché dans le terminal

## Étape 5 : Utiliser Docker Compose

Docker Compose est un outil qui permet de définir et de lancer des applications multi-conteneurs. Pour utiliser Docker Compose, vous devez d'abord le télécharger :

1. Ouvrir un terminal
2. Entrer la commande suivante : curl -L "https://github.com/docker/compose/releases/download/1.29.2/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
3. Rendre le fichier exécutable en entrant la commande suivante : sudo chmod +x /usr/local/bin/docker-compose

Après avoir installé Docker Compose, vous pouvez créer un fichier docker-compose.yml pour définir vos services et les lancer en utilisant la commande suivante :

```bash
docker-compose up
```

## Conclusion

Dans ce guide, nous avons vu comment installer Docker sur un Mac. Vous pouvez maintenant commencer à utiliser Docker pour isoler vos applications et simplifier leur déploiement.