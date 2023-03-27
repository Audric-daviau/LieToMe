# Utilisation du Dockerfile LieToMeIa

## Prérequis

- Docker doit être installé sur votre machine
- Le Dockerfile LieToMeIa doit être présent sur votre ordinateur

## Instructions

1. Placez-vous dans le dossier contenant le Dockerfile LieToMeIa
2. Ouvrez un terminal dans ce dossier
3. Construisez l'image Docker en utilisant la commande suivante :

```bash
docker build -t LieToMeIa .
```

4. Une fois l'image construite, vous pouvez la lancer en utilisant la commande suivante :

```bash
docker run -it --gpus all --mount type=bind,source=/path/to/your/folder,target=/src LieToMeIa
```

Assurez-vous de remplacer "/path/to/your/folder" par le chemin absolu du dossier que vous souhaitez monter dans le conteneur. Cette commande lancera le conteneur et vous donnera un accès interactif au shell.

## Configuration avancée

Si vous souhaitez personnaliser la configuration de votre conteneur, vous pouvez ajouter des options supplémentaires à la commande "docker run". Par exemple, pour utiliser plus de ressources CPU, vous pouvez ajouter l'option "--cpus":


```bash
docker run -it --cpus=4 --mount type=bind,source=/path/to/your/folder,target=/src LieToMeIa
```

Pour utiliser le GPU de votre ordinateur utiliser l'opion "--gpus all":

```bash
docker run -it --gpus all --mount type=bind,source=/path/to/your/folder,target=/src LieToMeIa
```


## Conclusion

En utilisant ce Dockerfile, vous pouvez créer un conteneur Docker prêt à l'emploi pour développer des applications d'intelligence artificielle avec TensorFlow. Assurez-vous de modifier les options de la commande "docker run" en fonction de vos besoins spécifiques.

La commande la plus optimisé dans notre cas pour lancer notre image docker est : 

```bash
docker run --gpus all -it -p 8888:8888 --mount type=bind,source=/home/audric/audric/ESIEE-S4/S4/LieToMe/LieToMe/pythonPackages/src/,target=/src,readonly=false tensorflow/tensorflow:latest-gpu-jupyter
```