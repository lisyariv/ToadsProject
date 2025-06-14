using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    public static int sensitivity;
    public Slider sensBar;
    public TMP_Text sensText;
    // Start is called before the first frame update
    void Start()
    {
        sensBar.maxValue = 40;
        sensBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        sensitivity = (int)sensBar.value;
        sensText.text = "" + sensitivity * 10;
    }
}
