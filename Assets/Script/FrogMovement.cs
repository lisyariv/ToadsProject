using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrogMovement : MonoBehaviour
{
    public Animator anim;
    public Vector3 moveDirection;
    public Vector3 jumpMovement;
    public Vector3 flyMovement;

    public float speed = 5.0f;
    public float timer;
    //public float timer1;

    public bool canJump;
    public bool canFly;
    public bool canSpeed;
    public bool onGround;
    public bool isFacingRight;
    public bool frogSwitch;
    public bool isMoving;
    public bool isFlying;

    public Rigidbody player;
    public Slider staminaBar;
    public GM gameManager;
    public TMP_Text StaminaTxt;
    public List<Sprite> frogSprites;
    public SpriteRenderer frogRenderer;
    public int animIndex;

    // Start is called before the first frame update
    void Start()
    {
        staminaBar.gameObject.SetActive(true);
        staminaBar.maxValue = 5f;
        staminaBar.value = 0.1f;
        StaminaTxt.text = "Stamina Bar";
        canJump = false;
        canFly = false;
        onGround = false;
        canSpeed = false;
        frogSwitch = false;
        isFlying = false;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Moving left to right
        if (gameManager.deadFrog == false && gameManager.isGameFinished == false) 
        {
            player.isKinematic = false;

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(x, 0, z);
            transform.Translate(moveDirection * Time.deltaTime * speed);
            
            if(x == 0 && z == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            //Animation

            anim.SetBool("isMoving", isMoving);
            anim.SetInteger("facing", animIndex);

           if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
           {
                animIndex = 0;
                if (frogSwitch == true)
                {
                    frogRenderer.sprite = frogSprites[3];
                }
                else
                {
                    frogRenderer.sprite = frogSprites[0];
                }
                
           }
          

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                animIndex = 2;
                if (frogSwitch == true)
                {
                    frogRenderer.sprite = frogSprites[5];
                }
                else
                {
                    frogRenderer.sprite = frogSprites[2];
                }

               
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
               
                animIndex = 1;
                if (frogSwitch == true)
                {
                    
                    frogRenderer.sprite = frogSprites[4];
                }
                else
                {
                    anim.Play("IdleLeft");
                    frogRenderer.sprite = frogSprites[1];
                }
                frogRenderer.flipX = false;
               
            }
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                animIndex = 1;
                if (frogSwitch == true)
                {
                    frogRenderer.sprite = frogSprites[4];
                }
                else
                {
                    frogRenderer.sprite = frogSprites[1];
                }
                frogRenderer.flipX = true;
               
            }

            //Jumping 
            if (canJump == true && Input.GetKey(KeyCode.Space))
            {
                canJump = false;
                GetComponent<Rigidbody>().AddForce(jumpMovement);
               
            }

            //Flying
            if (canFly == true && gameManager.isFlyCollected == true && Input.GetKey(KeyCode.F))
            {
                GetComponent<Rigidbody>().AddForce(flyMovement);
                staminaBar.value -= 0.01f;
                isFlying = true;
            }

            //Speeding
            if(canSpeed == true && Input.GetKey(KeyCode.E))
            {
                frogSwitch = true;
                speed = 10f;
                timer += Time.deltaTime;
                if(timer >= 2f)
                {
                    staminaBar.value -= 0.5f;
                    timer = 0;
                }
            }
            else
            {
                speed = 5f;
                frogSwitch = false;
            }

        }
        else if(onGround == false && gameManager.isCollecting == true)
        {
            player.isKinematic = true;
        }

       
        //Adding to Stamina Bar
        if(gameManager.preyCount >= 1 && gameManager.isCollected == true)
        {
            gameManager.isCollected = false;
            staminaBar.value += 1;
            Debug.Log(staminaBar.value);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bush")
        {
            gameManager.isInBush = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Bush")
        {
            gameManager.isInBush = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bush")
        {
            gameManager.isInBush = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
            canFly = true;
            onGround = true;
            gameManager.canFollowTarget = true;
        }
        if(collision.gameObject.tag == "Shelter")
        {
            gameManager.inShelter = true;
        }

        if (collision.gameObject.tag == "Predator")
        {
            canSpeed = true;
        }
    }
}
