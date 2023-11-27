// MazeGenerator.cs
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab; // 壁のプレハブ

    public void GenerateMaze(string mazeData)
    {
        string[] rows = mazeData.Trim().Split('\n');
        for (int y = 0; y < rows.Length; y++)
        {
            string[] cells = rows[y].Trim().Split(' ');
            for (int x = 0; x < cells.Length; x++)
            {
                if (cells[x] == "1")
                {
                    // 壁のオブジェクトを生成
                    Instantiate(wallPrefab, new Vector3(x, 0.5f, y), Quaternion.identity);
                }
            }
        }
    }
}
