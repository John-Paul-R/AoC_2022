
with open("./day2/input", "r") as f:
    lines = f.read().splitlines()

moveToScore = {
    "R": 1,
    "P": 2,
    "S": 3,
}

otherToRps = {
    "A": "R",
    "B": "P",
    "C": "S",
}

selfToRps = {
    "X": "R",
    "Y": "P",
    "Z": "S",
}


def rpsComparer(a, b) -> int:
    if a == b:
        return 0
    elif a == "R":
        if b == "P":
            return -1
        elif b == "S":
            return 1
    elif a == "P":
        if b == "R":
            return 1
        elif b == "S":
            return -1
    elif a == "S":
        if b == "R":
            return -1
        elif b == "P":
            return 1
    raise Exception("invalid input " + a + ' ' + b)

def compareResultToRoundScore(compareResult) -> int:
    if compareResult == -1:
        return 0
    if compareResult == 0:
        return 3
    if compareResult == 1:
        return 6
    raise Exception("invalid compareResult: " + compareResult)

def calculateRoundScore(otherPlayerMove, selfMove):
    score = moveToScore[selfMove]
    score += compareResultToRoundScore(rpsComparer(selfMove, otherPlayerMove))
    return score

def parseLine(line):
    other, selfMove = line.split()
    return [otherToRps[other], selfToRps[selfMove]]

scores = map(lambda line: calculateRoundScore(*parseLine(line)), lines)

print(sum(scores))

# pt2

def getWinMove(otherMove):
    if otherMove == "R": return "P"
    if otherMove == "P": return "S"
    if otherMove == "S": return "R"

def getDrawMove(otherMove):
    return otherMove

def getLoseMove(otherMove):
    if otherMove == "R": return "S"
    if otherMove == "P": return "R"
    if otherMove == "S": return "P"

commandToMoveFn = {
    "X": getLoseMove,
    "Y": getDrawMove,
    "Z": getWinMove,
}

def parseLinePt2(line):
    other, command = line.split()
    otherMove = otherToRps[other]
    selfMoveCalculatorFn = commandToMoveFn[command]
    return [otherMove, selfMoveCalculatorFn(otherMove)]

scores = map(lambda line: calculateRoundScore(*parseLinePt2(line)), lines)

print(sum(scores))
