
with open("./day6/input", "r") as f:
    lines = f.read().splitlines()


line = lines[0]

last4 = line[:4]

for i in range(4, len(line)):
    last4 = line[i-4:i]
    
    if len(set(last4)) == 4:

        print(last4)
        print(i)
        break

# pt2
count = 14
last = line[:count]

for i in range(count, len(line)):
    last = line[i-count:i]
    
    if len(set(last)) == count:

        print(last)
        print(i)
        break
