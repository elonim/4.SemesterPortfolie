import pandas as pd
import matplotlib.pyplot as plt


def header(msg):
    print('-' * 50)
    print('[' + msg + ']')
    print('-' * 50)


header("Load Data fra CSV")

filename = 'pima-indians-diabetes.csv'
df = pd.read_csv(filename)

header("print Data Frame")
print(df)

header("Print Statistik")
print(df.describe())

header("Print Shape")
print(df.shape)

sm = pd.plotting.scatter_matrix(df, alpha=0.2, figsize=(6, 6), diagonal='kde')

# Change label rotation
[s.xaxis.label.set_rotation(45) for s in sm.reshape(-1)]
[s.yaxis.label.set_rotation(0) for s in sm.reshape(-1)]

# May need to offset label when rotating to prevent overlap of figure
[s.get_yaxis().set_label_coords(-0.3, 0.5) for s in sm.reshape(-1)]

# Hide all ticks
[s.set_xticks(()) for s in sm.reshape(-1)]
[s.set_yticks(()) for s in sm.reshape(-1)]


plt.savefig('fig1.png')
