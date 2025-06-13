using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour
{
    public static int Sensitivity;
    public Slider sensBar;
    // Start is called before the first frame update
    void Start()
    {
        sensBar.maxValue = 300;
        sensBar.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Sensitivity = (int)sensBar.value;
        Debug.Log(Sensitivity);
    }
}
