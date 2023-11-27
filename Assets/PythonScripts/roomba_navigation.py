import random

def calculate_new_direction():
    # ランダムな新しい角度を生成
    angle = random.uniform(-180, 180)
    return angle

if __name__ == "__main__":
    new_direction = calculate_new_direction()
    print(new_direction)
