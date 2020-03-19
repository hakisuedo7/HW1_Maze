using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MenuControll : MonoBehaviour
{
    [SerializeField] private GameObject[] AllMenu;
    List<string> Files = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        SetMenuActive("MainMenu");
        CheckMaze();
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
    void CheckMaze()
    {
        foreach(string path in Directory.GetFiles(MazeData.path))
        {
            string ext = Path.GetExtension(path);
            if(ext == ".dat")
            {
                string filename = path.Substring(MazeData.path.Length);
                Files.Add(filename);
            }
        }
    }
    void LoadMaze(string filename)
    {
        string path = MazeData.path + filename;
        string mazedata = File.ReadAllText(path);

        Debug.Log(mazedata);
    }
}
