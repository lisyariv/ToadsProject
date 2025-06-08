using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDetectionScript : MonoBehaviour
{
    public Transform target;
    public NavMesh NM;
    public TMP_Text status;
    public float attackTimer;
    public IEnumerator attack;
    public GM gameManager;
    public FrogMovement frog;
    public float hp;
    public EnemyMovement EM;
   
    public bool TargetSeen;
    // Start is called before the first frame update
    void Start()
    {
        frog = GameObject.Find("Player").GetComponent<FrogMovement>();
        status = GameObject.Find("Status").GetComponent<TMP_Text>();
        status.text = "";
        hp = frog.staminaBar.value;
        gameManager = GameObject.Find("GameManager").GetComponent<GM>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        TargetSeen = false;
        gameManager.canFollowTarget = false;
        EM.leftPatrolX = transform.position.x - 10;
        EM.rightPatrolX = transform.position.x + 10;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (TargetSeen && !gameManager.isInBush)
        {
            NM.Track();
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (gameManager.isInBush == true)
            {
                EM.leftPatrolX = transform.position.x - 10;
                EM.rightPatrolX = transform.position.x + 10;
                status.text = "";
                attackTimer = 0;
                TargetSeen = false;
                gameManager.canFollowTarget = false;
            }
            if (gameManager.isInBush == false)
            {
                TargetSeen = true;
                status.text = "You were detected by a predator! Run to a bush to lose their focus on you";

                attackTimer += Time.deltaTime;
                if (attackTimer >= 2f)
                {
                    frog.staminaBar.value -= 0.5f;
                    Debug.Log(frog.staminaBar.value);
                    attackTimer = 0;
                }
            }
            if(frog.staminaBar.value <= 0f)
            {
                gameManager.diedFromPred = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            status.text = "";
            attackTimer = 0;

            if(gameManager.isInBush == true)
            {
                TargetSeen = false;
                NM.Track();
            }
            EM.leftPatrolX = transform.position.x - 10;
            EM.rightPatrolX = transform.position.x + 10;
        }
    }
}
