
import re


import re

with open("./day5/input", "r") as f:
    lines = f.read().splitlines()

stacks = [
    [],
    [],
    [],
    [],
    [],
    [],
    [],
    [],
    [],
]

for line in lines:
    for i in range(0, len(line), 4):
        if line[i + 1] != ' ':
            stacks[int(i/4)].append(line[i + 1])

for i in range(len(stacks)):
    stacks[i] = list(reversed(stacks[i]))

print (stacks)

with open("./day5/input2", "r") as f:
    lines2 = f.read().splitlines()

for line in lines2:
    ign0, amount, ign1, source, ign2, destination = line.split(' ')
    source = int(source) - 1
    destination = int(destination) - 1
    amount = int(amount)

    # print (amount, source, destination)
    moving = stacks[source][-amount:]
    for i in range(amount):
        stacks[source].pop()
        stacks[destination].append(moving[i])

o = ''
for i in range(len(stacks)):
    o += stacks[i].pop()
print(stacks)

print(o)

