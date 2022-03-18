import os
import tensorflow.compat.v1 as tf
import tensorflow_hub as hub
import tensorflow_text
import pandas as pd
from sklearn import preprocessing
import numpy as np

os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'
tf.disable_v2_behavior()

embed = hub.load("https://tfhub.dev/google/universal-sentence-encoder-multilingual-large/3")

data = pd.read_csv('labeled.csv', encoding='utf-8')

y = list(data['toxic'])
x = list(data['comment'])

le = preprocessing.LabelEncoder()
le.fit(y)

def encode(le, labels):
    enc = le.transform(labels)
    return tf.keras.utils.to_categorical(enc)

def decode(le, one_hot):
    dec = np.argmax(one_hot, axis=1)
    return le.inverse_transform(dec)

x_enc = x
y_enc = encode(le, y)

x_train = np.asarray(x_enc[:5000])
y_train = np.asarray(y_enc[:5000])

x_test = np.asarray(x_enc[5000:])
y_test = np.asarray(y_enc[5000:])

from keras.layers import Input, Lambda, Dense
from keras.models import Model
import keras.backend as K

def UniversalEmbedding(x):
    return embed(tf.squeeze(tf.cast(x, tf.string)))

input_text = Input(shape=(1,), dtype=tf.string)
embedding = Lambda(UniversalEmbedding, output_shape=(512, ))(input_text)
dense = Dense(256, activation='relu')(embedding)
pred = Dense(2, activation='softmax')(dense)
model = Model(inputs=[input_text], outputs=pred)
model.compile(loss='categorical_crossentropy', optimizer='adam', metrics=['accuracy'])

with tf.Session() as session:
    K.set_session(session)
    session.run(tf.global_variables_initializer())
    session.run(tf.tables_initializer())
    history = model.fit(x_train, y_train, epochs=3, batch_size=32)
    model.save_weights('./Models/model.h5')