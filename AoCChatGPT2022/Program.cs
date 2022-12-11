using System.IO;
using System.Linq;
using System.Collections.Generic;

Dictionary<string, int> moveToScore = new Dictionary<string, int>{
    {"R", 1},
    {"P", 2},
    {"S", 3},
};

Dictionary<string, string> otherToRps = new Dictionary<string, string>{
    {"A", "R"},
    {"B", "P"},
    {"C", "S"},
};

Dictionary<string, string> selfToRps = new Dictionary<string, string>{
    {"X", "R"},
    {"Y", "P"},
    {"Z", "S"},
};

int rpsComparer(string a, string b) {
    if (a == b) {
        return 0;
    }
    else if (a == "R") {
        if (b == "P") {
            return -1;
        }
        else if (b == "S") {
            return 1;
        }
    }
    else if (a == "P") {
        if (b == "R") {
            return 1;
        }
        else if (b == "S") {
            return -1;
        }
    }
    else if (a == "S") {
        if (b == "R") {
            return -1;
        }
        else if (b == "P") {
            return 1;
        }
    }
    throw new System.Exception("invalid input " + a + ' ' + b);
}

int compareResultToRoundScore(int compareResult) {
    if (compareResult == -1) {
        return 0;
    }
    if (compareResult == 0) {
        return 3;
    }
    if (compareResult == 1) {
        return 6;
    }
    throw new System.Exception("invalid compareResult: " + compareResult);
}

int calculateRoundScore(string otherPlayerMove, string selfMove) {
    int score = moveToScore[selfMove];
    score += compareResultToRoundScore(rpsComparer(selfMove, otherPlayerMove));
    return score;
}

string[] parseLine(string line)
{
    string[] splitLine = line.Split();
    return new string[] { otherToRps[splitLine[0]], selfToRps[splitLine[1]] };
}

int[] scores;
using (StreamReader f = new StreamReader("input")) {
    string[] lines = f.ReadToEnd().Split('\n');
    scores = lines.Where(l => l != "").Select(line => calculateRoundScore(parseLine(line)[0], parseLine(line)[1])).ToArray();
}

int totalScore = scores.Sum();
System.Console.WriteLine(totalScore);
