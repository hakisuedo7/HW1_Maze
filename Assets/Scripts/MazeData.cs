using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeData
{
    // static變數 存檔中的迷宮
    public static Dictionary<string, List<MazeBlockData>> MazeFile = new Dictionary<string, List<MazeBlockData>>();
    private static string currentMazeName = "";

    public static string MAZENAME
    {
        get { return currentMazeName; }
        set { currentMazeName = value; }
    }

    // 回傳迷宮
    public static List<MazeBlockData> GetMaze(string mazename)
    {
        return MazeFile[mazename];
    }

    // static變數 迷宮製作狀態(0: 自動生成, 1: 讀取檔案)
    private static bool load = false;

    // 檔案路徑
    public static string path = "Assets/Resources/Maze/";

    // static變數 迷宮長寬
    private static int min = 5;
    private static int max = 50;

    private static int _mazex = 10;
    private static int _mazey = 10;

    public static bool LOAD
    {
        get { return load; }
        set { load = value; }
    }
    public static int MAZE_X {
        get { return _mazex; }
        set { _mazex = Mathf.Clamp(value, min, max); }
    }
    public static int MAZE_Y {
        get { return _mazey; }
        set { _mazey = Mathf.Clamp(value, min, max); }
    }
}
