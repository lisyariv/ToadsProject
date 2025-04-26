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
    public bool deadFrog;
    public bool isGameFinished;

    public float WorldTime;
    public int preyCount;

    public TMP_Text GameText;
    public TMP_Text KeysText;

    public FrogMovement frog;
   
    // Start is called before the first frame update
    void Start()
    {
        isGameFinished = false;
        isCollected = false;
        isCollecting = false;
        isInBush = false;
        deadFrog = false;
        preyCount = 0;
        isFlyCollected = false;
        isNightTime = false;
        GameText.text = "Your energy is low! Find and consume prey to boost your energy.";
        KeysText.text = "Controls: WASD or Arrow Keys to move, SPACE to jump.";

    }

    // Update is called once per frame
    void Update()
    {
        WorldTime += Time.deltaTime;
        TimeInGame();
    }

    void TimeInGame()
    {
       
        if (isFlyCollected == true && deadFrog == false)
        {
            GameText.text = "Now, you're able to fly! Use the F key repeatedly to fly in the air.";
            KeysText.text = "Controls: WASD or Arrow Keys to move, SPACE to jump, F to fly.";
        }

        if(frog.canSpeed == true)
        {
            KeysText.text = "Controls: WASD or Arrow Keys to move, SPACE to jump, F to fly, E to speed up.";
        }

        if (frog.staminaBar.value <= 0f && preyCount >= 1)
        {
            deadFrog = true;
            GameText.text = "You became the predator's next meal. Try again?";
            isGameFinished = true;
        }

        if(preyCount >= 5)
        {
            GameText.text = "Now, interact with the predators to see if you'll obtain an ability. But, be careful!";
        }

        if(frog.staminaBar.value == 1.1f)
        {
            frog.staminaBar.value -= 0.1f;
        }

        if(frog.canSpeed == true && deadFrog == false)
        {
            GameText.text = "You can speed up now by pressing E and a walking button simultaneously.";
        }

        if (WorldTime >= 100f && WorldTime <= 150f)
        {
            isNightTime = true;
            GameText.text = "It is night time. Find shelter!";

            if (inShelter == true)
            {
                GameText.text = "You found shelter and were able to complete the first day!";
                isGameFinished = true;
            }
        }

        if(WorldTime >= 150f && inShelter == false)
        {
            GameText.text = "You remained unprotected in the dark, causing predators to feast upon you. Try again?";
            isGameFinished = true;
        }
    }
}
