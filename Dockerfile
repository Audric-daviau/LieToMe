# Utilise l'image TensorFlow de base avec Python 3.10
FROM tensorflow/tensorflow:latest-gpu-jupyter

# Installation des packages Python nécessaires à l'IA
RUN apt-get update && apt-get install -y --no-install-recommends \
    build-essential \
    git \
    curl \
    ca-certificates \
    libjpeg-dev \
    libpng-dev \
    libsm6 \
    libxext6 \
    libxrender-dev \
    sudo \
    wget \
    && rm -rf /var/lib/apt/lists/*

# Create new user to run as non-root user
# RUN useradd -d /home/userlambda -m -s /bin/bash userlambda 
RUN useradd -d /home/userlambda -m -s /bin/bash userlambda --uid 11324

USER userlambda

# Définition du répertoire de travail
WORKDIR /app

# Copie du requirements.txt dans l'image
COPY --chown=userlambda:userlambda requirements.txt .
# Installation des packages Python requis
RUN pip install --no-cache-dir -r requirements.txt

# COPY --chown=userlambda:userlambda pythonPackages/src/OpenFace/install.sh install.sh
# RUN pythonPackages/src/OpenFace/install.sh 


# Copy le dossier pythonPackages/src/ dans le containeur au chemin app/src

COPY --chown=userlambda:userlambda pythonPackages/src/ src/

CMD ["jupyter", "notebook", "--ip=0.0.0.0", "--port=8888", "--no-browser", "--NotebookApp.token=''", "--notebook-dir=/app"]


# ce docker contient la liste des packages python suivant : 
# python3-dev : c'est le package contenant les fichiers de développement pour Python 3, il est nécessaire pour installer d'autres packages qui ont des dépendances en C.
# python3-pip : c'est le gestionnaire de paquets pour Python 3, il permet d'installer des packages Python à partir du Python Package Index (PyPI) et d'autres sources.
# build-essential : ce package installe les outils nécessaires pour compiler des programmes sur le système d'exploitation.
# git : c'est un système de contrôle de version distribué qui permet de suivre les modifications apportées à un code source et de collaborer avec d'autres développeurs.
# vim : c'est un éditeur de texte en ligne de commande, souvent utilisé pour modifier des fichiers de configuration.
# nano : c'est un éditeur de texte simple en ligne de commande, utile pour éditer des fichiers de configuration.
# wget : c'est un utilitaire en ligne de commande permettant de télécharger des fichiers depuis des serveurs Web.
# curl : c'est un utilitaire en ligne de commande permettant de transférer des données vers ou depuis des serveurs Web, souvent utilisé pour tester des API.
# unzip : c'est un utilitaire en ligne de commande permettant de décompresser des fichiers compressés au format ZIP.
# libsm6 et libxext6 : ces deux packages sont des dépendances système pour OpenCV.
# libxrender-dev et libfontconfig1-dev : ces deux packages sont des dépendances système pour Matplotlib.
# libgl1-mesa-glx : c'est une bibliothèque pour les graphiques 3D, nécessaire pour faire fonctionner TensorFlow avec une carte graphique NVIDIA.
# nvidia-utils-465 : c'est un driver pour les cartes graphiques NVIDIA, permettant de bénéficier de toute la puissance de calcul de la carte graphique.
# python3-opencv : c'est une bibliothèque de traitement d'images open-source, souvent utilisée en vision par ordinateur et en apprentissage automatique.
# matplotlib : c'est une bibliothèque de visualisation de données en Python, souvent utilisée pour créer des graphiques.
# scipy : c'est une bibliothèque pour les mathématiques, les sciences et l'ingénierie en Python, souvent utilisée pour les calculs numériques.
# numpy : c'est une bibliothèque pour les calculs numériques en Python, souvent utilisée pour manipuler des tableaux multidimensionnels.
# pandas : c'est une bibliothèque pour la manipulation et l'analyse de données en Python, souvent utilisée pour le traitement de données en apprentissage automatique.
# tensorflow : c'est un framework open-source pour l'apprentissage automatique, souvent utilisé pour construire des réseaux de neurones profonds. Dans cette image, nous utilisons la version GPU de TensorFlow, qui permet d'utiliser toute la puissance de calcul de la carte graphique.
