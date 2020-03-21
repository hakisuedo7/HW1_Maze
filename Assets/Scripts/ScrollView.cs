using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScrollView : MonoBehaviour
{
    public GameObject Button_Template;
    private List<string> FileNameList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        GetFileList();

        foreach (string name in FileNameList)
        {
            GameObject BtnObj = Instantiate(Button_Template) as GameObject;
            BtnObj.SetActive(true);
            Button btn = BtnObj.GetComponent<Button>();
            btn.SetName(name);
            btn.transform.SetParent(Button_Template.transform.parent);
            btn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetFileList()
    {
        FileNameList = new List<string>(MazeData.MazeFile.Keys);
    }
    public void ButtonClicked(string str)
    {
        MazeData.LOAD = true;
        SceneManager.LoadScene("MainScene");
    }
}
