using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public bool diedFromPred;
    public bool canFollowTarget;
    public bool foundShelter;

    public float WorldTime;
    public int preyCount;
    public int matCount;

    public TMP_Text GameText;
    public TMP_Text KeysText;
    public TMP_Text TaskText;

    public FrogMovement frog;

    public List<GameObject> shelters;

    // Start is called before the first frame update
    void Start()
    {

        GameObject[] objectsShelters = GameObject.FindGameObjectsWithTag("Shelter");
        shelters = new List<GameObject>(objectsShelters);
        diedFromPred = false;
        isGameFinished = false;
        foundShelter = false;
        isCollected = false;
        isCollecting = false;
        isInBush = false;
        deadFrog = false;
        preyCount = 0;
        isFlyCollected = false;
        isNightTime = false;
        GameText.text = "Your energy is low! Find and consume prey to replenish your energy. Collect at least 5.";
        KeysText.text = "Controls: WASD or Arrow Keys to move, SPACE to jump.";
        TaskText.text = "Prey Collected: " + preyCount;

        for (int i = 0; i < shelters.Count; i++)
        {
            shelters[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        WorldTime += Time.deltaTime;
        TimeInGame();
    }

    void TimeInGame()
    {
       
        if (!foundShelter && isFlyCollected == true && deadFrog == false && isGameFinished == false)
        {
            TaskText.text = "Prey Collected: " + preyCount;
            GameText.text = "Now, you're able to fly! Use the F key repeatedly to fly in the air, but not too much as it will decrease your stamina.";
            KeysText.text = "Controls: WASD or Arrow Keys to move, SPACE to jump, F to fly.";

        }

        if(frog.canSpeed == true)
        {
            KeysText.text = "Controls: WASD or Arrow Keys to move, SPACE to jump, F to fly, E to speed up.";
        }

        if (frog.staminaBar.value <= 0f && preyCount >= 1 && isGameFinished == false && diedFromPred == true)
        {
            deadFrog = true;
            GameText.text = "You became the predator's next meal. Try again?";
            isGameFinished = true;
            SceneManager.LoadScene("LoseScene");

        }
        if (diedFromPred == false && isGameFinished == false && preyCount >= 1 && frog.staminaBar.value <= 0f)
        {
            GameText.text = "You passed out from a lack of stamina. Try again?";
            isGameFinished = true;
            SceneManager.LoadScene("LoseScene");
        }

        if (!isNightTime && !foundShelter && !frog.shelterCreated && preyCount >= 5 && deadFrog == false && isGameFinished == false)
        {
            GameText.text = "Now, collect items to build a shelter. Also, interact with the predators, but be careful!";
            TaskText.text = "Material Collected: " + matCount + "/8";

            if (matCount >= 8)
            {
                for (int i = 0; i < shelters.Count; i++)
                {
                    shelters[i].SetActive(true);
                }
            }
        }

        if(frog.staminaBar.value == 1.1f)
        {
            frog.staminaBar.value -= 0.1f;
        }

        if(!isNightTime && !foundShelter && frog.canSpeed == true && deadFrog == false && isGameFinished == false)
        {
            GameText.text = "You can speed up now by pressing E and a walking button simultaneously. This will also drain your stamina.";
        }

        if (WorldTime >= 130f && WorldTime <= 180f && isGameFinished == false)
        {
            isNightTime = true;

            if(foundShelter == false && frog.shelterCreated == false)
            {
                GameText.text = "It is night time. Create your shelter in a safe area!";
            }

            if(foundShelter && frog.shelterCreated)
            {
                GameText.text = "It is night time. Go to your shelter!";
            }
            
            if(matCount == 8)
            {
                TaskText.text = "Find a safe area and construct your shelter.";
            }

            if (inShelter == true)
            {
                GameText.text = "You found shelter and were able to complete the first day!";
                isGameFinished = true;
                SceneManager.LoadScene("WinScreen");
            }

        }

        if (WorldTime >= 180f && inShelter == false && isGameFinished == false)
        {
            GameText.text = "You remained unprotected in the dark, causing predators to feast upon you. Try again?";
            isGameFinished = true;

        }
    }
}
