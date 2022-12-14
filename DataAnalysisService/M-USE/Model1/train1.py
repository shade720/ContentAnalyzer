import os
import tensorflow.compat.v1 as tf
import tensorflow_hub as hub
import tensorflow_text
import pandas as pd
from sklearn import preprocessing
import numpy as np
import codecs

os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'
tf.disable_v2_behavior()

embed = hub.load("https://tfhub.dev/google/universal-sentence-encoder-multilingual-large/3")

data_list = []
with codecs.open('dataset.txt', "r", "utf_8_sig" ) as file:
    for line in file:
        labels = line.split()[0]
        text = line[len(labels)+1:].strip()
        labels = labels.split(",")
        mask = [1 if "__label__NORMAL" in labels else 0,
                1 if "__label__INSULT" in labels else 0,
                1 if "__label__THREAT" in labels else 0,
                1 if "__label__OBSCENITY" in labels else 0]
        data_list.append((text, *mask))

data = pd.DataFrame(data_list, columns=["text", "normal", "insult", "threat", "obscenity"])
x = data.iloc[:,0]
y = data.iloc[:,[1,2,3,4]]

x_enc = x
y_enc = y

x_train = np.asarray(x_enc[:75000])
y_train = np.asarray(y_enc[:75000])

x_test = np.asarray(x_enc[75000:100000])
y_test = np.asarray(y_enc[75000:100000])


from keras.layers import Input, Lambda, Dense
from keras.models import Model
import keras.backend as K

def UniversalEmbedding(x):
    return embed(tf.squeeze(tf.cast(x, tf.string)))

input_text = Input(shape=(1,), dtype=tf.string)
embedding = Lambda(UniversalEmbedding, output_shape=(512, ))(input_text)
dense = Dense(256, activation='relu')(embedding)
pred = Dense(4, activation='softmax')(dense)
model = Model(inputs=[input_text], outputs=pred)
model.compile(loss='categorical_crossentropy', optimizer='adam', metrics=['accuracy'])

with tf.Session() as session:
    K.set_session(session)
    session.run(tf.global_variables_initializer())
    session.run(tf.tables_initializer())
    history = model.fit(x_train, y_train, epochs=3, batch_size=32)
    model.save_weights('./model.h5')