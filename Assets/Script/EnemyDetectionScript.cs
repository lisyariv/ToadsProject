using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyDetectionScript : MonoBehaviour
{
    public Transform target;
    public TMP_Text status;
    public float attackTimer;
    public IEnumerator attack;
    public GM gameManager;
    public FrogMovement frog;
    public float hp;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float leftPatrolX, rightPatrolX;
    [SerializeField] private float minPauseTime, maxPauseTime;
    [SerializeField] private float minWalkTime, maxWalkTime;
    [SerializeField] private int facingDirection = -1;

    private float randomTime, timer;
    private bool isWalking = true;
    private bool isFlipping;
    public bool TargetSeen;
    // Start is called before the first frame update
    void Start()
    {
        frog = GameObject.Find("Player").GetComponent<FrogMovement>();
        status = GameObject.Find("Status").GetComponent<TMP_Text>();
        status.text = "";
        hp = frog.staminaBar.value;
        gameManager = GameObject.Find("GameManager").GetComponent<GM>();
        randomTime = Random.Range(minWalkTime, maxWalkTime);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        leftPatrolX = transform.position.x - 10;
        rightPatrolX = transform.position.x + 10;
        TargetSeen = false;
        gameManager.canFollowTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= randomTime)
        {
            StateChange();
        }
        if(!isFlipping && (transform.position.x > rightPatrolX || transform.position.x < leftPatrolX))
        {
            StartCoroutine(Flip());
        }
        if (isWalking)
        {
            rb.velocity = Vector2.right * facingDirection * speed;
        }

        /*if (TargetSeen && !gameManager.isInBush)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }*/
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            TargetSeen = true;
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (gameManager.isInBush == true)
            {
                leftPatrolX = transform.position.x - 10;
                rightPatrolX = transform.position.x + 10;
                status.text = "";
                attackTimer = 0;
                TargetSeen = false;
            }
            if (gameManager.isInBush == false)
            {
                status.text = "You were detected by a predator! Run to a bush to lose their focus on you";

                attackTimer += Time.deltaTime;
                if (attackTimer >= 2f)
                {
                    frog.staminaBar.value -= 1;
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
        }
    }
   
    IEnumerator Flip()
    {
        isFlipping = true;
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;
    }

    void StateChange()
    {
        isWalking = !isWalking;
        randomTime = isWalking ? Random.Range(minWalkTime, maxWalkTime) : Random.Range(minPauseTime, maxPauseTime);
        timer = 0;
    }
}
