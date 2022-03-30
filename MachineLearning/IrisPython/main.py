from sklearn.ensemble import GradientBoostingClassifier
from sklearn.ensemble import RandomForestClassifier
from sklearn import datasets
from sklearn.model_selection import train_test_split

iris = datasets.load_iris()

x = iris.data
y = iris.target

x_train, x_test, y_train, y_test = train_test_split(x, y, test_size=0.5)

classifier = RandomForestClassifier(
    n_estimators=1000, criterion='gini', random_state=0)
classifier.fit(x_train, y_train)
y_pred = classifier.predict(x_test)

print('Score, Random Forest:', classifier.score(x_test, y_test))

clf = GradientBoostingClassifier(
    n_estimators=1000, learning_rate=1.0, max_depth=3, random_state=0)
clf = clf.fit(x_train, y_train)

print('Score, Gradient Boosting: ', clf.score(x_test, y_test))