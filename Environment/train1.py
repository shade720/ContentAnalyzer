import os
import matplotlib.pyplot as plt
import tensorflow.compat.v1 as tf
import tensorflow_hub as hub
import tensorflow_text
import pandas as pd
from sklearn import preprocessing
from sklearn import metrics
import numpy as np
import codecs
import seaborn as sns
from tensorflow.python.keras.callbacks import TensorBoard


os.environ['TF_CPP_MIN_LOG_LEVEL'] = '2'
tf.disable_v2_behavior()

embed = hub.load("https://tfhub.dev/google/universal-sentence-encoder-multilingual-large/3")

data_list = []
with codecs.open("E:\Projects\Prototype\ContentAnalyzer\DataAnalysisService\M-USE\Model1\dataset.txt", "r", "utf_8_sig" ) as file:
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

x_train = np.asarray(x_enc[:200000])
y_train = np.asarray(y_enc[:200000])

x_test = np.asarray(x_enc[200000:240000])
y_test = np.asarray(y_enc[200000:240000])

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

SENTIMENT_LABELS = [
    "Normal", "Insult", "Threat", "Obscenity"
]

with tf.Session() as session:
    K.set_session(session)
    session.run(tf.global_variables_initializer())
    session.run(tf.tables_initializer())
    callback = TensorBoard(log_dir='logs', write_images=True, write_graph=True, update_freq='batch')
    history = model.fit(x_train, y_train, epochs=3, batch_size=128, callbacks=[callback])
    model.save_weights('./model.h5')
    #model.load_weights('./model.h5')
    predicts = model.predict(x_test, batch_size=128)
    predicts = np.argmax(predicts, axis=-1)
    y_test_arg = np.argmax(y_test,axis=1)
    cm = metrics.confusion_matrix(y_test_arg, predicts)
    cm = cm/cm.sum(axis=1)[:, tf.newaxis]
    sns.heatmap(
        cm, annot=True,
        xticklabels=SENTIMENT_LABELS,
        yticklabels=SENTIMENT_LABELS)
    plt.xlabel("Predicted")
    plt.ylabel("True")
    input()
    