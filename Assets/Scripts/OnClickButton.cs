using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        // 未製作
        menuControll.SetMenuActive("LoadMenu");
    }

    public void Option()
    {
        // 未製作
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
        menuControll.SetMenuActive("CustomMenu");
    }

    public void CustomPlay()
    {
        GameObject[] sliders = GameObject.FindGameObjectsWithTag("slider");
        MazeData.MAZE_X = (int)sliders[0].GetComponent<Slider>().value;
        MazeData.MAZE_Y = (int)sliders[1].GetComponent<Slider>().value;
        SceneManager.LoadScene("MainScene");
    }

    public void ClickReset()
    {
        OptionData.VOLUME = 1f;
        OptionData.M_SensitivityX = 2f;
        OptionData.M_SensitivityY = 2f;
    }
}
