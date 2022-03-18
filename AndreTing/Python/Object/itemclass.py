import csv

from numpy import true_divide

class Item:
    pay_rate = 0.8 #pay rate after 20% discount
    all = []
    def __init__(self, name: str, price: float, quantity=0):
        #Run validation
        assert price >= 0, f"Price {price} is not greater than Zero"
        assert quantity >= 0, f"Quantity {quantity} is not greater than Zero"

        #Assign to self
        self.name = name
        self.price = price
        self.quantity = quantity

        #Actions to exxecute
        Item.all.append(self)

    def calculate_total_price(self): #en metode modtager altid et objekt nemlig sig selv
        return self.price * self.quantity

    def apply_discount(self):
        self.price = self.price * self.pay_rate

    @classmethod # en classmethod moder ikke en instance af objektet men en instance af klassen
    def instantiate_from_csv(cls):
        with open('items.csv', 'r') as f:
            reader = csv.DictReader(f)
            items = list(reader)
        
        for item in items:
            Item(
                name = item.get('name'),
                price = float(item.get('price')),
                quantity = int(item.get('quantity'))
            )

    def __repr__(self):
        return f"Item('{self.name}', {self.price}, {self.quantity})"

    @staticmethod # statiske metoder modtager ikke et objekt af sig selv men kun det man passer ind i metoden
    def is_integer(num):
        if isinstance(num, float):
            #test om num er en float. tæller antallet af 0 efter ,
            return num.is_integer()
        elif isinstance(num, int):
            return True
        else:
            return False