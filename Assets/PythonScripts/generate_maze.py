import random
import json
import os

def dig(x, y, maze, width, height):
    dir = [(1, 0), (-1, 0), (0, 1), (0, -1)]
    random.shuffle(dir)
    for (dx, dy) in dir:
        new_x, new_y = x + dx * 2, y + dy * 2
        if (0 < new_x < width-1) and (0 < new_y < height-1) and (maze[new_y][new_x] == 1):
            maze[new_y][new_x] = 0
            maze[new_y - dy][new_x - dx] = 0
            dig(new_x, new_y, maze, width, height)

def generate_maze(width, height):
    maze = [[1 for _ in range(width)] for _ in range(height)]
    start_x, start_y = (1, 1)
    maze[start_y][start_x] = 0
    dig(start_x, start_y, maze, width, height)
    return maze

def save_maze_to_json(maze, filename):
    flattened_maze = [cell for row in maze for cell in row]
    maze_data = {"maze": flattened_maze, "width": len(maze[0]), "height": len(maze)}
    with open(filename, 'w') as f:
        json.dump(maze_data, f)


width, height = 15, 15
maze = generate_maze(width, height)

# 2次元リストを出力
for row in maze:
    print(' '.join(map(str, row)))
    
# 現在のスクリプトのディレクトリを取得
current_dir = os.path.dirname(os.path.realpath(__file__))
# Unityプロジェクトのルートディレクトリへの相対パスを構築
unity_project_path = os.path.join(current_dir, '../Assets/Resources')

# 迷路データを保存するファイルパス
maze_file_path = os.path.join(unity_project_path, 'maze.json')

# 迷路データの生成と保存（generate_maze関数とmaze変数は先のコードを参照）
maze = generate_maze(width, height)
save_maze_to_json(maze, maze_file_path)