import itemclass as ic

ic.Item.instantiate_from_csv()

print(ic.Item.all)

print(ic.Item.is_integer(7))

phone1 = ic.Item("jscPhonev10", 500, 5)
phone2 = ic.Item("jscPhonev20", 700, 5)