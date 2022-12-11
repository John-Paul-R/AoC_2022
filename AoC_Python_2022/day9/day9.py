from functional import seq
import json
# print(
#     seq(all_dirs)
#         .map(lambda d: d["the_directory_size"])
#         .filter(lambda d: d > required_delete_amt)
#         .min()
# )


with open("./day9/input", "r") as f:
    lines = f.read().splitlines()

# with open("./day9/temp", "r") as f:
#     lines = f.read().splitlines()

# I ended up doing this in c#