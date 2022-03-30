from numpy import split
from numpy import array
from pandas import read_csv

# split a univariate dataset into train/test sets


def split_dataset(data):
    # split into standard weeks
    train, test = data[1:-328], data[-328:-6]
    # restructure into windows of weekly data
    train = array(split(train, len(train)/7))
    test = array(split(test, len(test)/7))
    return train, test


def evaluate(csvdays):
    # load the new file
    print(' ')
    print('Evaluation of data')
    dataset = read_csv(csvdays, header=0, infer_datetime_format=True, parse_dates=[
                       'datetime'], index_col=['datetime'])
    train, test = split_dataset(dataset.values)
    # validate train data
    print(train.shape)
    print(train[0, 0, 0], train[-1, -1, 0])
    # validate test
    print(test.shape)
    print(test[0, 0, 0], test[-1, -1, 0])
