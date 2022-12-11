
with open("./day4/input", "r") as f:
    lines = f.read().splitlines()

def rangeStrToTuple(s: str) -> tuple[int, int]:
    lower, upper = s.split('-')
    return (int(lower), int(upper))

def lineToRangePair(line: str) -> tuple[tuple[int, int], tuple[int, int]]:
    left, right = line.split(',')
    return (rangeStrToTuple(left), rangeStrToTuple(right))

rangePairs = list(map(lineToRangePair, lines))

def rangeContains(outer, inner) -> bool:
    return outer[0] <= inner[0] and outer[1] >= inner[1]

def existsFullOverlap(a: tuple[int, int], b: tuple[int, int]):
    return rangeContains(a, b) or rangeContains(b, a)

numFullOverlap = 0

for el in rangePairs:
    if existsFullOverlap(el[0], el[1]):
        numFullOverlap += 1

print(numFullOverlap)

# pt2

def existsAnyOverlap(a: tuple[int, int], b: tuple[int, int]):
    return a[0] >= b[0] and a[0] <= b[1] or a[1] <= b[1] and a[1] >= b[0] or rangeContains(a, b) or rangeContains(b, a)

numAnyOverlap = 0

for el in rangePairs:
    if existsAnyOverlap(el[0], el[1]):
        numAnyOverlap += 1

print(numAnyOverlap)
