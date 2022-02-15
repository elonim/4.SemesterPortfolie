import numpy as np
import pandas as pd
import seaborn as sns
import matplotlib.pyplot as plt

def header(msg):
    print('-' * 50)
    print('[' + msg + ']')

header("Load Data fra CSV")

filename = 'pima-indians-diabetes.csv'
df = pd.read_csv(filename)


header("print Data Frame")
print(df)

header("Print Statistik")
print(df.describe())

header("Print Shape")
print(df.shape)