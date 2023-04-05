import cv2

# Capture la vidéo à partir de la webcam
cap = cv2.VideoCapture(0)

# Vérifie si la caméra est ouverte correctement
if not cap.isOpened():
    print("Impossible d'ouvrir la caméra")
    exit()

# Boucle jusqu'à ce que l'utilisateur appuie sur la touche 'q'
while True:
    # Lit un seul frame de la vidéo
    ret, frame = cap.read()

    # Vérifie si le frame est bien lu
    if not ret:
        print("Impossible de lire la vidéo")
        break

    # Affiche le frame dans une fenêtre appelée "Test Webcam"
    cv2.imshow('Test Webcam', frame)

    # Attend 1 milliseconde pour que la fenêtre s'affiche
    # Si l'utilisateur appuie sur la touche 'q', la boucle est interrompue
    if cv2.waitKey(1) == ord('q'):
        break

# Libère la caméra et ferme la fenêtre
cap.release()
cv2.destroyAllWindows()
