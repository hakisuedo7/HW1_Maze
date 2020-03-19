using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControll : MonoBehaviour
{
    [SerializeField] private GameObject[] AllMenu;

    // Start is called before the first frame update
    void Start()
    {
        SetMenuActive("MainMenu");
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
}
