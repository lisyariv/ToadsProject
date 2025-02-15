using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GM : MonoBehaviour
{
    public bool isCollected;
    public bool isCollecting;
    public bool isInBush;
    public int preyCount;
    public bool isFlyCollected;
    public bool isNightTime;
    public float WorldTime;
    public TMP_Text GameText;
    // Start is called before the first frame update
    void Start()
    {
        isCollected = false;
        isCollecting = false;
        isInBush = false;
        preyCount = 0;
        isFlyCollected = false;
        isNightTime = false;
        GameText.text = "Your energy is low! Find and consume prey to boost your energy.";
    }

    // Update is called once per frame
    void Update()
    {
        WorldTime += Time.deltaTime;
        TimeInGame();
    }

    void TimeInGame()
    {
        if(WorldTime >= 100f)
        {
            isNightTime = true;
            GameText.text = "It is night time. Find shelter!";
            Debug.Log("It is night time. Find shelter!");
        }
    }
}
