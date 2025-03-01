using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDetectionScript : MonoBehaviour
{
    public TMP_Text status;
    public float attackTimer;
    public IEnumerator attack;
    public GM gameManager;
    public FrogMovement frog;
    public float hp;
    
    // Start is called before the first frame update
    void Start()
    {
        frog = GameObject.Find("Player").GetComponent<FrogMovement>();
        status = GameObject.Find("Status").GetComponent<TMP_Text>();
        status.text = "";
        hp = frog.staminaBar.value;
        gameManager = GameObject.Find("GameManager").GetComponent<GM>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameManager.isInBush == true)
            {
                status.text = "";
                attackTimer = 0;
            }
            if (gameManager.isInBush == false)
            {
                status.text = "You were detected by a predator! Run to a hiding place to lose their focus on you";

                attackTimer += Time.deltaTime;
                if (attackTimer >= 2f)
                {
                    frog.staminaBar.value -= 1;
                    Debug.Log(frog.staminaBar.value);
                    attackTimer = 0;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            status.text = "";
            attackTimer = 0;
        }
    }

    
}
