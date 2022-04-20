import tensorflow.compat.v1 as tf
import numpy as np
import tensorflow_hub as hub
import tensorflow_text
from keras.layers import Input, Lambda, Dense
from keras.models import Model
import keras.backend as K
tf.disable_v2_behavior()

embed = hub.load("https://tfhub.dev/google/universal-sentence-encoder-multilingual-large/3")
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
    model_path = input()
    model.load_weights(model_path)

    while True:
        try:
            expression = input()
            predict = model.predict(np.asarray([expression]))
            a = list(predict)
            print(a[0])
        except Exception:
            print(0)

