# Comment installer Docker sous Windows

## Introductio

Docker est un outil très populaire pour créer et exécuter des applications dans des conteneurs. Dans ce guide, nous allons vous montrer comment installer Docker sous Windows. Il existe plusieurs façons d'installer Docker sous Windows, mais nous allons nous concentrer sur la méthode recommandée par Docker.

## Configuration requise

- Windows 10 64-bit: Pro, Enterprise ou Education (version 15063 ou ultérieure)
- Système compatible avec la virtualisation
- Au moins 4 Go de RAM

## Étapes d'installation

1. Tout d'abord, téléchargez l'installateur Docker pour Windows à partir du site officiel de Docker: https://www.docker.com/products/docker-desktop

2. Exécutez l'installateur téléchargé. Si une fenêtre de contrôle de compte d'utilisateur apparaît, cliquez sur "Oui" pour permettre l'installation de Docker.

3. Lorsque l'installation est terminée, Docker doit être lancé automatiquement. Si ce n'est pas le cas, recherchez Docker dans le menu Démarrer et lancez-le.

4. Si Docker est correctement installé, vous devriez voir une icône Docker dans la barre d'état système en bas à droite de votre écran. Cliquez dessus pour afficher le menu Docker.

5. Vous pouvez maintenant commencer à utiliser Docker en créant des conteneurs et en exécutant des images.

## Vérification de l'installation

Pour vérifier si Docker est correctement installé et fonctionne, ouvrez l'invite de commandes ou PowerShell et exécutez la commande suivante:

```bash
docker run hello-world
```

Si Docker est correctement installé, il devrait télécharger et exécuter une image appelée "hello-world" dans un conteneur. Vous devriez voir le message "Hello from Docker!" dans votre fenêtre de console.

## Conclusion

Félicitations! Vous avez maintenant installé Docker sur votre machine Windows. Vous pouvez maintenant commencer à utiliser Docker pour créer des conteneurs et exécuter des images.