
with open("./day3/input", "r") as f:
    lines = f.read().splitlines()

def findShared(left, right) -> str:
    leftSet = set(left)
    rightSet = set(right)
    for el in leftSet:
        if el in rightSet:
            return el
    raise Exception("No overlap")

def getPriority(char):
    oVal = ord(char)
    if oVal < 97:
        return oVal - 65 + 27 
    return oVal - 97 + 1

sharedChars = []

for line in lines:
    midpoint = int(len(line) / 2)
    left = line[:midpoint]
    right = line[midpoint:]
    sharedChar = findShared(left, right)
    sharedChars.append(sharedChar)

total = sum(map(getPriority, sharedChars))
print(total)

# pt2

def findShared2(itemLists: list[str]) -> str:
    sets = [set(lst) for lst in itemLists]

    shared = list(sets[0])
    for setIdx in range(1, len(sets)):
        shared = list(filter(lambda el: el in sets[setIdx], shared))

    if len(shared) == 1:
        return shared.pop()
    raise Exception("failed to identify a single shared element between all of the provided lists")

def batch(iterable, n=1):
    l = len(iterable)
    for ndx in range(0, l, n):
        yield iterable[ndx:min(ndx + n, l)]

groupedLines = batch(lines, 3)

sharedChars2 = [findShared2(linesGroup) for linesGroup in groupedLines]

print (sum(map(getPriority, sharedChars2)))
