import cv2
from tensorflow.keras.models import load_model
import numpy as np
import socket
import struct
import time

# Load the model
model = load_model('emotion_model.h5')

host, port = "127.0.0.1", 25001
sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect((host, port))

# Define the list of label names (should match what you used during training)
label_names = ['angry', 'happy', 'sad', 'surprise', 'neutral'] # added 'angry' and 'sad'

# Start the webcam feed
cap = cv2.VideoCapture(0)
predicted_label = ''
while True:
    # Read a frame from the webcam
    ret, frame = cap.read()

    if not ret:
        break

    # Convert the image to grayscale
    gray = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

    # Detect a face in the image
    face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')
    faces = face_cascade.detectMultiScale(gray, 1.1, 4)

    for (x, y, w, h) in faces:
        # Extract the region of interest
        roi_gray = gray[y:y + h, x:x + w]
        roi_gray = cv2.resize(roi_gray, (48, 48))
        roi_gray = roi_gray.astype('float32') / 255
        roi_gray = np.expand_dims(roi_gray, axis=0)
        roi_gray = np.expand_dims(roi_gray, axis=-1)

        # Make a prediction on the ROI
        prediction = model.predict(roi_gray)[0]
        predicted_label = label_names[np.argmax(prediction)]

        # Draw a rectangle around the face and write the predicted label
        cv2.rectangle(frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
        cv2.putText(frame, predicted_label, (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.9, (0, 255, 0), 2)


        # Convert the frame to byte array
        resized_frame = cv2.resize(frame, (640, 480))  # Resize to a smaller resolution
        _, img_encoded = cv2.imencode('.jpg', resized_frame)
        data = np.array(img_encoded)
        string_data = data.tostring()

        # Send the frame data
        size = struct.pack('!i', len(string_data))
        sock.sendall(size)
        sock.sendall(string_data)

        # Send the predicted label
        label_data = predicted_label.encode("UTF-8")
        size = struct.pack('!i', len(label_data))
        sock.sendall(size)
        sock.sendall(label_data)


        # Introduce a delay to limit the frame rate (e.g., 0.1 seconds)
        time.sleep(0.1)

    # Converting string to Byte, and sending it to C#
    #sock.sendall(predicted_label.encode("UTF-8"))
    # receiveing data in Byte from C#, and converting it to String
    #receivedData = sock.recv(1024).decode("UTF-8")
    #print(receivedData)

    # Display the frame
    cv2.imshow('Video', frame)

    if cv2.waitKey(1) & 0xFF == ord('q'):
        break

cap.release()
cv2.destroyAllWindows()