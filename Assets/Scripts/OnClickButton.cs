using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickButton : MonoBehaviour
{
    MenuControll menuControll;
    // Start is called before the first frame update
    void Start()
    {
        menuControll = GameObject.FindGameObjectWithTag("GameController").GetComponent<MenuControll>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        menuControll.SetMenuActive("SelectDifficultyMenu");
    }

    public void Load()
    {
        // 讀取選單 再看看要不要做
        menuControll.SetMenuActive("LoadMenu");
    }

    public void Option()
    {
        // 選項選單 再看看要不要做
        menuControll.SetMenuActive("OptionMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        menuControll.SetMenuActive("MainMenu");
    }

    public void BackToSelectDifficultyMenu()
    {
        menuControll.SetMenuActive("SelectDifficultyMenu");
    }

    public void Easy()
    {
        MazeData.MAZE_X = 10;
        MazeData.MAZE_Y = 10;
        SceneManager.LoadScene("MainScene");
    }

    public void Normal()
    {
        MazeData.MAZE_X = 12;
        MazeData.MAZE_Y = 12;
        SceneManager.LoadScene("MainScene");

    }

    public void Hard()
    {
        MazeData.MAZE_X = 30;
        MazeData.MAZE_Y = 30;
        SceneManager.LoadScene("MainScene");
    }

    public void Custom()
    {
        // 自訂迷宮選單
        menuControll.SetMenuActive("CustomMenu");
    }
}
