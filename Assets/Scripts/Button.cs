using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    private string Name;
    public TMPro.TextMeshProUGUI ButtonText;
    public ScrollView scrollView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName(string name)
    {
        Name = name;
        ButtonText.text = name;
    }
    public void ButtonClick()
    {
        scrollView.ButtonClicked(Name);
    }
}
