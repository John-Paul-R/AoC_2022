from functional import seq
import json
# print(
#     seq(all_dirs)
#         .map(lambda d: d["the_directory_size"])
#         .filter(lambda d: d > required_delete_amt)
#         .min()
# )


with open("./day8/input", "r") as f:
    lines = f.read().splitlines()

# with open("./day8/temp", "r") as f:
#     lines = f.read().splitlines()


# def transpose(arr2d):
#     rowsCount = len(arr2d)
#     colsCount = len(arr2d[0])

#     cols = [[0]*rowsCount]*colsCount
#     for i in range(rowsCount):
#         for j in range(colsCount):
#             cols[i][j] = arr2d[i][j]
#     return cols

def transpose(l1):
    l2=[]
    # iterate over list l1 to the length of an item
    for i in range(len(l1[0])):
        # print(i)
        row =[]
        for item in l1:
            # appending to new list with values and index positions
            # i contains index position and item contains values
            row.append(item[i])
        l2.append(row)
    return l2


rows = [[int(col) for col in line] for line in lines]
cols = transpose(rows)


def findVisible(row):
    maxHeight = -1
    for i in range(len(row)):
        height, isVisible, ign = row[i]
        if height > maxHeight:
            maxHeight = height
            row[i][1] = True # type: ignore
    
    maxHeight = -1
    for i in range(len(row)-1, 0, -1):
        height, isVisible, ign = row[i]
        if height > maxHeight:
            maxHeight = height
            row[i][1] = True # type: ignore

rowsData = [[[height, False, 1] for height in row] for row in rows]
for row in rowsData:
    findVisible(row)
for col in transpose(rowsData):
    findVisible(col)

c = 0
for row in rowsData:
    for col in row:
        height, isVisible, ign = col # type: ignore
        if isVisible:
            c += 1

# print(json.dumps(rowsData))
print(c)

def applyAxisScore(row, idx):
    height0, isVisible0, curScore0 = row[idx]
    if idx == 0 or idx == len(row) - 1:
        row[idx][2] = 0
    if row[idx][2] == 0:
        return

    c1 = 0
    for i in range(idx -1, -1, -1):
        height1, isVisible1, curScore1 = row[i]
        c1 +=1
        if height1 >= height0: break

    c2 = 0
    for i in range(idx+1, len(row), 1):
        height1, isVisible1, curScore1 = row[i]
        c2 += 1
        if height1 >= height0: break
    row[idx][2] = row[idx][2] * c1 * c2

for row in rowsData:
    for i in range(len(row)):
        applyAxisScore(row, i)
for col in transpose(rowsData):
    for i in range(len(col)):
        applyAxisScore(col, i)

maxScore = 0
for i in range(len(rowsData)):
    for j in range(len(rowsData[0])):
        if rowsData[i][j][2] > maxScore:
            maxScore = rowsData[i][j][2]
print(json.dumps(rowsData))
print (maxScore)