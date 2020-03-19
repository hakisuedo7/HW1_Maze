using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MazeData
{
    // static變數 迷宮長寬
    private static int min = 5;
    private static int max = 50;

    private static int _mazex = 10;
    private static int _mazey = 10;

    public static int MAZE_X {
        get { return _mazex; }
        set
        {
            _mazex = Mathf.Clamp(value, min, max);
        }
    }
    public static int MAZE_Y {
        get { return _mazey; }
        set
        {
            _mazey = Mathf.Clamp(value, min, max);
        }
    }
}
