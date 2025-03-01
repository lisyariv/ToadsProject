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
    public bool isFlyCollected;
    public bool isNightTime;
    public bool inShelter;

    public float WorldTime;
    public int preyCount;
    public TMP_Text GameText;
    public FrogMovement frog;
   
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
        if (isFlyCollected == true)
        {
            GameText.text = "Now, you're able to fly! Use the F key repeatedly to fly in the air.";
        }

        if (frog.staminaBar.value < 0.1f)
        {
            GameText.text = "You became the predator's next meal. Try again?";
            Debug.Log("ur dead.");
        }

        if (WorldTime >= 100f)
        {
            isNightTime = true;
            GameText.text = "It is night time. Find shelter!";

            if (inShelter == true)
            {
                GameText.text = "You found shelter and were able to complete the first day!";
            }
        }

        if(WorldTime >= 130 && inShelter == false)
        {
            GameText.text = "You remained unprotected in the dark, causing predators to feast upon you. Try again?";
        }
    }
}
