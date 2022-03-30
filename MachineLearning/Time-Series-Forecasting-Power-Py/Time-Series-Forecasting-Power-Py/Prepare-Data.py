# tutorial fra https://machinelearningmastery.com/how-to-develop-lstm-models-for-multi-step-time-series-forecasting-of-household-power-consumption/
from numpy import nan
from numpy import isnan
from pandas import read_csv
import TimeFrame
import Evaluate
import zipfile

filename = 'household_power_consumption'
zip = filename + '.zip'
txt = filename + '.txt'
csv = filename + '.csv'
csvdays = filename + '_days.csv'


print('Extracting .Zip')
with zipfile.ZipFile(zip, 'r') as zip_ref:
    zip_ref.extractall()

dataset = read_csv(txt, sep=';', header=0, low_memory=False,
                   infer_datetime_format=True, parse_dates={'datetime': [0, 1]}, index_col=['datetime'])

# mark all missing values
dataset.replace('?', nan, inplace=True)
# make dataset numeric
dataset = dataset.astype('float32')

# fill missing values with a value at the same time one day ago


def fill_missing(values):
    one_day = 60 * 24
    for row in range(values.shape[0]):
        for col in range(values.shape[1]):
            if isnan(values[row, col]):
                values[row, col] = values[row - one_day, col]


# fill missing
fill_missing(dataset.values)

# add a column for for the remainder of sub metering
values = dataset.values
dataset['sub_metering_4'] = (
    values[:, 0] * 1000 / 60) - (values[:, 4] + values[:, 5] + values[:, 6])

# save updated dataset
dataset.to_csv(csv)


TimeFrame.TimeframeToDays(csv, csvdays)
Evaluate.evaluate(csvdays)
