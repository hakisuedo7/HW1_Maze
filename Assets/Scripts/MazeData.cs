using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeData
{
    // static變數 迷宮製作狀態(0: 自動生成, 1: 讀取檔案)
    private static bool load = false;
    // 檔案路徑 && 名稱
    public static string path = "Assets/Resources/Maze.dat/";
    private static string mazeName = "";

    // static變數 迷宮長寬
    private static int min = 5;
    private static int max = 50;

    private static int _mazex = 5;
    private static int _mazey = 5;

    public static bool LOAD
    {
        get { return load; }
        set { load = value; }
    }
    public static string MAZENAME
    {
        get { return mazeName; }
        set { mazeName = value; }
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
