# Importation des bibliothèques nécessaires
import cv2
import mediapipe as mp
import numpy as np
import time

# Initialisation de l'objet face_mesh de MediaPipe
mp_face_mesh = mp.solutions.face_mesh
face_mesh = mp_face_mesh.FaceMesh(
    min_detection_confidence=0.5, min_tracking_confidence=0.5)

# Initialisation de l'objet pour le dessin
mp_drawing = mp.solutions.drawing_utils
drawing_spec = mp_drawing.DrawingSpec(thickness=1, circle_radius=1)

# Initialisation de la capture vidéo (0 = webcam principale)
cap = cv2.VideoCapture(0)

# Boucle principale
while cap.isOpened():
    # Lecture de l'image à partir de la webcam
    success, image = cap.read()
    start = time.time()

    # Conversion de l'image en RGB
    image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)

    # On rend l'image non modifiable pour améliorer les performances
    image.flags.writeable = False

    # Exécution de la détection des visages
    results = face_mesh.process(image)

    # On rend l'image à nouveau modifiable
    image.flags.writeable = True
    image = cv2.cvtColor(image, cv2.COLOR_RGB2BGR)

    # Initialisation des listes pour stocker les coordonnées 2D et 3D du visage
    img_h, img_w, img_c = image.shape
    face_2d = []
    face_3d = []

    # Si on détecte au moins un visage
    if results.multi_face_landmarks:
        # Pour chaque visage détecté
        for face_landmarks in results.multi_face_landmarks:
            # Pour chaque point de repère sur le visage
            for idx, lm in enumerate(face_landmarks.landmark):
                # Si l'index du point de repère correspond à ceux que nous voulons
                # if idx in [33, 263, 1, 61, 291, 199]:
                if idx == 33 or idx == 263 or idx == 1 or idx == 61 or idx == 291 or idx == 199:
                    # Si l'index est 1, il s'agit du nez
                    if idx == 1:
                        nose_2d = (lm.x * img_w, lm.y * img_h)
                        nose_3d = (lm.x * img_w, lm.y * img_h, lm.z * 3000)

                    # Conversion des coordonnées du point de repère en pixels
                    x, y = int(lm.x * img_w), int(lm.y * img_h)

                    # Ajout des coordonnées 2D à la liste face_2d
                    face_2d.append([x, y])

                    # Ajout des coordonnées 3D à la liste face_3d
                    face_3d.append([x, y, lm.z])

            # Conversion des listes en arrays numpy pour les manipulations ultérieures
            face_2d = np.array(face_2d, dtype=np.float64)
            face_3d = np.array(face_3d, dtype=np.float64)

            # Calcul de la matrice de la caméra
            focal_length = 1 * img_w
            cam_matrix = np.array(
                [[focal_length, 0, img_h/2], [0, focal_length, img_w / 2], [0, 0, 1]])

            # Matrice de distorsion
            dist_matrix = np.zeros((4, 1), dtype=np.float64)

            # Résolution de PnP pour obtenir
            success, rot_vec, trans_vec = cv2.solvePnP(
                face_3d, face_2d, cam_matrix, dist_matrix)

            # Obtenir la matrice de rotation
            rmat, jac = cv2.Rodrigues(rot_vec)

            # Obtenir les angles de rotation
            angles, mtxR, mtxQ, Qx, Qy, Qz = cv2.RQDecomp3x3(rmat)

            # Obtenir le degré de rotation en y
            x = angles[0] * 360
            y = angles[1] * 360
            z = angles[2] * 360

            # Voir où l'utilisateur penche la tête
            if y < -10:
                text = "Regarde à gauche"
            elif y > 10:
                text = "Regarde à droite"
            elif x < -10:
                text = "Regarde en bas"
            elif x > 10:
                text = "Regarde en haut"
            else:
                text = "En avant"

            # Affiche la direction du nez
            nose_3d_projection, jacobian = cv2.projectPoints(
                nose_3d, rot_vec, trans_vec, cam_matrix, dist_matrix)

            p1 = (int(nose_2d[0]), int(nose_2d[1]))
            p2 = (int(nose_2d[0] + y * 10), int(nose_2d[1] - x * 10))

            cv2.line(image, p1, p2, (255, 0, 0), 3)

            # Ajouter le texte sur les images
            cv2.putText(image, text, (20, 50),
                        cv2.FONT_HERSHEY_SIMPLEX, 2, (0, 255, 0), 2)
            cv2.putText(image, "x: " + str(np.round(x, 2)), (500, 50),
                        cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 0, 255), 2)
            cv2.putText(image, "y: " + str(np.round(y, 2)), (500, 100),
                        cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 0, 255), 2)
            cv2.putText(image, "z: " + str(np.round(z, 2)), (500, 150),
                        cv2.FONT_HERSHEY_SIMPLEX, 1, (0, 0, 255), 2)

        # Calcul du FPS
        end = time.time()
        totalTime = end - start
        fps = 1 / totalTime

        # Affichage du FPS
        cv2.putText(image, f'FPS: {int(fps)}', (20, 450),
                    cv2.FONT_HERSHEY_SIMPLEX, 1.5, (0, 255, 0), 2)

        # Dessin des points de repère sur l'image
        mp_drawing.draw_landmarks(image=image, landmark_list=face_landmarks, connections=mp_face_mesh.FACEMESH_CONTOURS,
                                  landmark_drawing_spec=drawing_spec, connection_drawing_spec=drawing_spec)

    # Affichage de l'image
    cv2.imshow('Estimation de la pose de la tête', image)

    # Si l'utilisateur appuie sur la touche ESC, on arrête la boucle
    if cv2.waitKey(5) & 0xFF == 27:
        break

cap.release()