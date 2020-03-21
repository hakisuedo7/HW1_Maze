using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuControll : MonoBehaviour
{
    [SerializeField] private GameObject[] AllMenu;

    private void Awake()
    {
        SetMenuActive("MainMenu");
        LoadAllMaze();                  // 讀取資料夾下所有檔案
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void SetAllMenuInActive()
    {
        foreach (GameObject menu in AllMenu)
            menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMenuActive(string name)
    {
        int index = 0;
        foreach (GameObject menu in AllMenu)
        {
            if (menu.name == name)
            {
                break;
            }
            index++;
        }

        if(index >= AllMenu.Length)
        {
            Debug.LogError("menu" + name + "沒有找到");
            SetMenuActive("MainMenu");
        }
        else
        {
            SetAllMenuInActive();
            AllMenu[index].SetActive(true);
        }
    }
    void LoadAllMaze()
    {
        Debug.Log("load all data");
        if (Directory.Exists(MazeData.path))
        {
            foreach (string path in Directory.GetFiles(MazeData.path))
            {
                string ext = Path.GetExtension(path);
                if (ext == ".dat")
                {
                    CheckAndSaveMaze(path);         // 副檔名.dat再檢查迷宮是否合法
                }
            }
        }
        else
        {
            Directory.CreateDirectory(MazeData.path);
        }
        
    }
    void CheckAndSaveMaze(string path)
    {
        List<MazeBlockData> tempMaze = new List<MazeBlockData>();
        string mazeName;
        
        string AllData = File.ReadAllText(path);
        string[] LineData = AllData.Split(System.Environment.NewLine.ToCharArray());

        // 第一行 迷宮名字
        if (!string.IsNullOrEmpty(LineData[0]))
        {
            mazeName = LineData[0];
        }
        else
        {
            return;
        }

        // 第二行 迷宮長寬
        if (!string.IsNullOrEmpty(LineData[1]))
        {
            string[] tempStr = LineData[1].Split(' ');
            int maze_x = int.Parse(tempStr[0]);
            int maze_y = int.Parse(tempStr[1]);
            if (maze_x < 5 || maze_x > 50 || maze_y < 5 || maze_y > 50)
                return;

            MazeData.MAZE_X = maze_x;
            MazeData.MAZE_Y = maze_y;
        }
        else
        {
            return;
        }
            
        // 第三行之後
        for(int i = 0; i < MazeData.MAZE_X * MazeData.MAZE_Y; i++)
        {
            if (!string.IsNullOrEmpty(LineData[i + 2]))
            {
                string[] tempStr = LineData[i + 2].Split(' ');
                int x = int.Parse(tempStr[0]);
                int y = int.Parse(tempStr[1]);
                int z = int.Parse(tempStr[2]);

                if (x < 0 || x >= MazeData.MAZE_X || y < 0 || y >= MazeData.MAZE_Y || z < 0 || z > 3)
                    return;

                tempMaze.Add(new MazeBlockData(x, y, z));
            }
            else
            {
                return;
            }
        }

        // 存進MazeData
        MazeData.MazeFile.Add(mazeName, tempMaze);
        Debug.Log(mazeName + "成功讀取");
    }
}
