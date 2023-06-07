import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
from tensorflow.keras.utils import to_categorical
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import Conv2D, MaxPooling2D, Dropout, Flatten, Dense

# Load the dataset
data = pd.read_csv('fer2013.csv')

# Keep only the classes of interest
data = data[(data.emotion == 0) | (data.emotion == 3) | (data.emotion == 4) | (data.emotion == 5) | (data.emotion == 6)]

# Reduce the labels range to 0-4 instead of 0,3,4,5,6
emotion_mapping = {0: 0, 3: 1, 4: 2, 5: 3, 6: 4}
data['emotion'] = data['emotion'].map(emotion_mapping)

# Split data into training and test sets
train = data[data['Usage'] == 'Training']
test = data[data['Usage'] == 'PublicTest']

# Process the data
img_array = train.pixels.apply(lambda x: np.array(x.split(' ')).reshape(48, 48, 1).astype('float32'))
train_images = np.stack(img_array, axis=0)
train_labels = to_categorical(train.emotion.values)

img_array_test = test.pixels.apply(lambda x: np.array(x.split(' ')).reshape(48, 48, 1).astype('float32'))
test_images = np.stack(img_array_test, axis=0)
test_labels = to_categorical(test.emotion.values)

# Normalize the images
train_images /= 255
test_images /= 255

# Build the model
model = Sequential([
    Conv2D(32, (3, 3), activation='relu', input_shape=(48, 48, 1)),
    MaxPooling2D((2, 2)),
    Conv2D(64, (3, 3), activation='relu'),
    MaxPooling2D((2, 2)),
    Flatten(),
    Dense(64, activation='relu'),
    Dense(5, activation='softmax'), # changed from 3 to 5
])

# Compile the model
model.compile(optimizer='adam', loss='categorical_crossentropy', metrics=['accuracy'])

# Train the model
model.fit(train_images, train_labels, epochs=10, batch_size=64, validation_data=(test_images, test_labels))

# Save the model
model.save('emotion_model.h5')