import json

with open("./day7/input", "r") as f:
    lines = f.read().splitlines()


lines[2:]


curPath = []
curPathObjs = []
dirs = {
    '/': {}
}
curDir = dirs

def cd(target):
    global curPath
    global curDir
    global curPathObjs
    if target == '/':
        curPath = ['/']
        curPathObjs = [dirs['/']]
        curDir = curPathObjs[-1]
    elif target == '..':
        curPath.pop()
        curPathObjs.pop()
        curDir = curPathObjs[-1]
    else:
        curPath.append(target)
        curDir = curDir[target]
        curPathObjs.append(curDir)

for line in lines:
    parts = line.split(' ')
    if parts[0] == '$':
        if parts[1] == 'cd':
            cd(parts[2])
        elif parts[1] == 'ls':
            continue
    else:
        pre, name = line.split(' ')
        if (pre == 'dir'):
            curDir[name] = {}
        else:
            fileSize = int(pre)
            curDir[name] = fileSize # type: ignore


print(json.dumps(dirs))

max_dir_size = 100000

# def mapDir(dirLines):
#     curDir = {}
#     for line in dirLines:
#         pre, name = line.split(' ')
#         if (pre == 'dir'):
#             curDir[name] = {}
#         else:
#             fileSize = int(pre)
#             curDir[name] = fileSize
#     return curDir


appropriate_sized_dirs = []
all_dirs = []
def get_dir_size(directory: dict) -> int:
    global appropriate_sized_dirs
    amt = 0
    for el in directory.values():
        if isinstance(el, int):
            amt += el
        else:
            amt += get_dir_size(el)
    directory["the_directory_size"] = amt
    if amt < max_dir_size:
        appropriate_sized_dirs.append(directory)
    all_dirs.append(directory)
    return amt

total_used_space = get_dir_size(dirs)
print (appropriate_sized_dirs)
print(sum(map(lambda d: d["the_directory_size"], appropriate_sized_dirs)))

#pt2

total_disk_space = 70000000
required_space   = 30000000

available_space = (total_disk_space - total_used_space)
required_delete_amt = required_space - available_space

print(min(filter(lambda d: d > required_delete_amt, map(lambda d: d["the_directory_size"], all_dirs))))

# Edit: just found cool lib.
# Makes write/reading some of this less painful.
# Also has some LINQ-inspired methods
from functional import seq

print(
    seq(all_dirs)
        .map(lambda d: d["the_directory_size"])
        .filter(lambda d: d > required_delete_amt)
        .min()
)
