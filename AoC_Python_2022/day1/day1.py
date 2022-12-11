
inventories = []
curInv = []
with open("./day1/input", "r") as f:
    lines = f.read().splitlines()

for line in lines:
    if len(line) == 0:
        inventories.append(curInv)
        curInv = []
    else:
        curInv.append(int(line))

inventoryCalories = map(sum, inventories)

pt1Answer = max(inventoryCalories)

print(sum(sorted(inventoryCalories, reverse=True)[0:3]))
