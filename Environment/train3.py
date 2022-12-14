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

train_df = pd.read_csv('train.csv', encoding='utf-8')
val_df = pd.read_csv('val.csv', encoding='utf-8')

train_x = train_df.iloc[:,0]
train_y = train_df.iloc[:,[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18]]

val_x = val_df.iloc[:,0]
val_y = val_df.iloc[:,[1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18]]

train_x = np.asarray(train_x)
train_y = np.asarray(train_y)

val_x = np.asarray(val_x)
val_y = np.asarray(val_y)

from keras.layers import Input, Lambda, Dense
from keras.models import Model
import keras.backend as K

def UniversalEmbedding(x):
    return embed(tf.squeeze(tf.cast(x, tf.string)))

input_text = Input(shape=(1,), dtype=tf.string)
embedding = Lambda(UniversalEmbedding, output_shape=(512, ))(input_text)
dense = Dense(256, activation='relu')(embedding)
pred = Dense(18, activation='softmax')(dense)
model = Model(inputs=[input_text], outputs=pred)
model.compile(loss='categorical_crossentropy', optimizer='adam', metrics=['accuracy'])

with tf.Session() as session:
    K.set_session(session)
    session.run(tf.global_variables_initializer())
    session.run(tf.tables_initializer())
    history = model.fit(x=train_x, y=train_y, epochs=5, batch_size=32, validation_data=(val_x, val_y))
    model.save_weights('model.h5')
