using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameControll : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenu;              // 暫停選單GameObject
    [SerializeField] private GameObject OptionMenu;             // 參數調整選單GameObject
    [SerializeField] private GameObject GameComplete;           // 通關畫面
    [SerializeField] private GameObject InputBox;               // 取名框
    [SerializeField] private GameObject TimerObj;               // 計時器
    private static bool isPause = false;                        // 是否暫停
    private static bool GameOver = false;                       // 遊戲是否結束
    public bool ISPAUSE                                         // 現在狀態是否暫停(給其他script參考)
    {
        get { return isPause; }
    }
    public bool ISGAMEOVER                                      // 現在狀態是否結束(給其他script參考)
    {
        get { return GameOver; }
    }

    const float WallScale = 3.0f;                               // 牆壁Scale Size(固定)
    Vector3 startPoint;                                         // 牆壁生成初始點
    Vector3 endPoint;                                           // 出口block中心位置
    Quaternion FrontWallQuat = Quaternion.identity;             // 牆壁正面Quaternion 
    Quaternion SideWallQuat = Quaternion.Euler(0, 90, 0);       // 牆壁側面Quaternion

    private int MazeSizeX;                                      // 迷宮大小 x
    private int MazeSizeY;                                      // 迷宮大小 y

    GameObject playerModel, wallModel;                          // GameObject 玩家 & 牆壁 模板
    GameObject player;                                          // GameObject 玩家 & 牆壁

    List<MazeBlockData> mazeBlock = new List<MazeBlockData>();  // 迷宮狀態
    List<int[]> randDir = new List<int[]>();                    // 方向亂數
    Timer timer;                                                // Timer Conponent


    enum WALL_TYPE
    {
        NO_WALL = 0,
        UP_WALL,
        RIGHT_WALL,
        UP_RIGHT_WALL
    }

    // Start is called before the first frame update
    void Start()
    {
        // 隱藏視窗 && 鼠標
        HideAllScreen();
        HideCursor();

        // 時間開始 && 初始值設定
        Time.timeScale = 1f;
        isPause = false;
        GameOver = false;

        // 設定迷宮大小
        MazeSizeX = MazeData.MAZE_X;
        MazeSizeY = MazeData.MAZE_Y;

        // 動態載入角色 && 牆壁
        playerModel = Resources.Load<GameObject>("Prefabs/Cube");
        wallModel = Resources.Load<GameObject>("Prefabs/Wall");

        // 設定起點 && 終點
        startPoint = new Vector3(-(MazeSizeX - 1) * WallScale / 2, 0, -(MazeSizeY - 1) * WallScale / 2);
        endPoint = Vector3.zero - startPoint;

        if (MazeData.LOAD)
        {
            mazeBlock = MazeData.GetMaze(MazeData.MAZENAME);
        }
        else
        {
            // 給方向值
            SetRandDir();
            // 製作迷宮
            CreateMaze();
        }

        // 畫出迷宮
        DrawMaze(mazeBlock);
        // 叫出玩家
        CreatePlayer(startPoint);

        // 計時器
        timer = TimerObj.GetComponent<Timer>();
        timer.TimeStart();
    }

    // Update is called once per frame
    void Update()
    {
        // 判斷到達終點(座標)
        if (!GameOver)
        {
            if (Mathf.Abs(player.transform.position.x - endPoint.x) <= WallScale / 2 && player.transform.position.z - endPoint.z >= WallScale)
            {
                GameOver = true;
            }
        }
        else
        {
            ShowCursor();
            if (!InputBox.activeSelf)
                GameComplete.SetActive(true);
            else
                GameComplete.SetActive(false);

            if (timer.IsStart)
            {
                TMPro.TextMeshProUGUI TMPU = GameComplete.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[1];

                TMPU.text = "通關時間: " + timer.GetTime().ToString() + "秒";
                timer.TimeStop();
            }
        }
        // 判斷是否暫停
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    void HideAllScreen()
    {
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
        GameComplete.SetActive(false);
        InputBox.SetActive(false);
    }
    void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Resume()
    {
        HideAllScreen();
        HideCursor();
        Time.timeScale = 1f;
        isPause = false;
    }
    public void Pause()
    {
        ShowCursor();
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }
    public void Restart()
    {
        Debug.Log("重新開始");
        Resume();
        Destroy(player);
        CreatePlayer(startPoint);
        GameOver = false;
        timer.TimeStop();
        timer.TimeStart();
    }
    public void showOptionMenu()
    {
        OptionMenu.SetActive(true);
    }
    public void hideOptionMenu()
    {
        OptionMenu.SetActive(false);
    }
    void CreatePlayer(Vector3 position)
    {
        player = Instantiate(playerModel, position, Quaternion.identity);
    }
    void SetRandDir()
    {
        randDir.Add(new int[] { 0, 1, 2, 3 });
        randDir.Add(new int[] { 0, 1, 3, 2 });
        randDir.Add(new int[] { 0, 2, 1, 3 });
        randDir.Add(new int[] { 0, 2, 3, 1 });
        randDir.Add(new int[] { 0, 3, 1, 2 });
        randDir.Add(new int[] { 0, 3, 2, 1 });
        randDir.Add(new int[] { 1, 0, 2, 3 });
        randDir.Add(new int[] { 1, 0, 3, 2 });
        randDir.Add(new int[] { 1, 2, 0, 3 });
        randDir.Add(new int[] { 1, 2, 3, 0 });
        randDir.Add(new int[] { 1, 3, 0, 2 });
        randDir.Add(new int[] { 1, 3, 2, 0 });
        randDir.Add(new int[] { 2, 0, 1, 3 });
        randDir.Add(new int[] { 2, 0, 3, 1 });
        randDir.Add(new int[] { 2, 1, 0, 3 });
        randDir.Add(new int[] { 2, 1, 3, 0 });
        randDir.Add(new int[] { 2, 3, 0, 1 });
        randDir.Add(new int[] { 2, 3, 1, 0 });
        randDir.Add(new int[] { 3, 0, 1, 2 });
        randDir.Add(new int[] { 3, 0, 2, 1 });
        randDir.Add(new int[] { 3, 1, 0, 2 });
        randDir.Add(new int[] { 3, 1, 2, 0 });
        randDir.Add(new int[] { 3, 2, 0, 1 });
        randDir.Add(new int[] { 3, 2, 1, 0 });
    }

    void CreateMaze()
    {
        // 設定初始
        SetInitBlockData(MazeSizeX, MazeSizeY);
        // 走訪迷宮
        TryMaze(0, 0);
    }

    void SetInitBlockData(int rows,int cols) {
        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                mazeBlock.Add(new MazeBlockData(i, j, (int)WALL_TYPE.UP_RIGHT_WALL, false));
            }
        }
        // 把出口放在右上(上方出口)
        mazeBlock[mazeBlock.Count - 1].setWallType((int)WALL_TYPE.RIGHT_WALL);
    }
    void DrawMaze(List<MazeBlockData> mazeBlocks)
    {
        foreach(MazeBlockData mazeblock in mazeBlocks)
        {
            // 上牆
            if(mazeblock.getWallType() == (int)WALL_TYPE.UP_WALL || mazeblock.getWallType() == (int)WALL_TYPE.UP_RIGHT_WALL)
            {
                Vector3 WallPos = new Vector3(mazeblock.getX() * WallScale, 0, mazeblock.getY() * WallScale + WallScale / 2);
                Instantiate(wallModel, WallPos + startPoint, FrontWallQuat);
            }
            // 右牆
            if (mazeblock.getWallType() == (int)WALL_TYPE.RIGHT_WALL || mazeblock.getWallType() == (int)WALL_TYPE.UP_RIGHT_WALL)
            {
                Vector3 WallPos = new Vector3(mazeblock.getX() * WallScale + WallScale / 2, 0, mazeblock.getY() * WallScale);
                Instantiate(wallModel, WallPos + startPoint, SideWallQuat);
            }


            // 補下牆壁
            if(mazeblock.getY() == 0)
            {
                Vector3 WallPos = new Vector3(mazeblock.getX() * WallScale, 0, mazeblock.getY() * WallScale - WallScale / 2);
                Instantiate(wallModel, WallPos + startPoint, FrontWallQuat);
            }
            // 補左牆壁
            if (mazeblock.getX() == 0)
            {
                Vector3 WallPos = new Vector3(mazeblock.getX() * WallScale - WallScale / 2, 0, mazeblock.getY() * WallScale);
                Instantiate(wallModel, WallPos + startPoint, SideWallQuat);
            }
        }
    }
    bool visited(int x,int y, int rows, int cols)
    {
        return mazeBlock[x * cols + y].isVisited();
    }
    bool visitable(int x, int y, int rows, int cols)
    {
        return x >= 0 && x < rows && y >= 0 && y < cols && !visited(x, y, rows, cols);
    }

    void TryMaze(int x, int y)
    {
        mazeBlock[x * MazeSizeY + y].setVisited();
        int[] Dir = randDir[Random.Range(0, 24)];
        for(int i = 0; i < 4; i++)
        {
            int nx = nextX(x, Dir[i]);
            int ny = nextY(y, Dir[i]);
            if (visitable(nx, ny, MazeSizeX, MazeSizeY))
            {
                BreakWall(x, y, MazeSizeX, MazeSizeY, Dir[i]);
                TryMaze(nx, ny);
            }
        }
    }
    void BreakWall(int x,int y, int rows, int cols, int dir)
    {
        switch (dir)
        {
            // 右
            case 0:
                if (mazeBlock[x * cols + y].getWallType() == (int)WALL_TYPE.UP_RIGHT_WALL)
                    mazeBlock[x * cols + y].setWallType((int)WALL_TYPE.UP_WALL);
                else if (mazeBlock[x * cols + y].getWallType() == (int)WALL_TYPE.RIGHT_WALL)
                    mazeBlock[x * cols + y].setWallType((int)WALL_TYPE.NO_WALL);
                break;
            // 上
            case 1:
                if (mazeBlock[x * cols + y].getWallType() == (int)WALL_TYPE.UP_RIGHT_WALL)
                    mazeBlock[x * cols + y].setWallType((int)WALL_TYPE.RIGHT_WALL);
                else if (mazeBlock[x * cols + y].getWallType() == (int)WALL_TYPE.UP_WALL)
                    mazeBlock[x * cols + y].setWallType((int)WALL_TYPE.NO_WALL);
                break;
            // 左
            case 2:
                if (mazeBlock[(x - 1) * cols + y].getWallType() == (int)WALL_TYPE.UP_RIGHT_WALL)
                    mazeBlock[(x - 1) * cols + y].setWallType((int)WALL_TYPE.UP_WALL);
                else if (mazeBlock[(x - 1) * cols + y].getWallType() == (int)WALL_TYPE.RIGHT_WALL)
                    mazeBlock[(x - 1) * cols + y].setWallType((int)WALL_TYPE.NO_WALL);
                break;
            // 下
            case 3:
                if (mazeBlock[x * cols + y - 1].getWallType() == (int)WALL_TYPE.UP_RIGHT_WALL)
                    mazeBlock[x * cols + y - 1].setWallType((int)WALL_TYPE.RIGHT_WALL);
                else if (mazeBlock[x * cols + y - 1].getWallType() == (int)WALL_TYPE.UP_WALL)
                    mazeBlock[x * cols + y - 1].setWallType((int)WALL_TYPE.NO_WALL);
                break;
            default:
                break;
        }
    }

    int nextX(int x, int dir)
    {
        int[] move = new int[] { 1, 0, -1, 0 };
        return x + move[dir];
    }
    int nextY(int y, int dir)
    {
        int[] move = new int[] { 0, 1, 0, -1 };
        return y + move[dir];
    }

    public void ShowInputBox()
    {
        InputBox.SetActive(true); 
    }
    public void HideInputBox()
    {
        InputBox.SetActive(false);
    }
    public void saveMazeData(string filename)
    {
        HideInputBox();
        string data = filename + "\n";

        // 紀錄長寬
        data += MazeData.MAZE_X + " " + MazeData.MAZE_Y + "\n";

        // 紀錄牆壁狀態
        foreach(MazeBlockData block in mazeBlock)
        {
            data += block.getX() + " " + block.getY() + " " + block.getWallType() + "\n";
        }

        File.WriteAllText(MazeData.path + filename + ".dat", data);
    }
}
