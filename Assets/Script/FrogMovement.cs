using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrogMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 5.0f;
    public Vector3 jumpMovement;
    public Vector3 flyMovement;
    public bool canJump;
    public bool canFly;
    public bool canSpeed;
    public Rigidbody player;
    public Slider staminaBar;
    public GM gameManager;
    public TMP_Text StaminaTxt;
    public bool onGround;

    public List<Sprite> frogSprites;
    public SpriteRenderer frogRenderer;
    public bool isFacingRight;
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
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       //Moving left to right
       if(gameManager.isCollecting == false && gameManager.deadFrog == false) 
        {
            player.isKinematic = false;

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(x, 0, z);
            transform.Translate(moveDirection * Time.deltaTime * speed);

           if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                frogRenderer.sprite = frogSprites[0];
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                frogRenderer.sprite = frogSprites[2];
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                frogRenderer.sprite = frogSprites[1];
                frogRenderer.flipX = false;
            }
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                frogRenderer.sprite = frogSprites[1];
                frogRenderer.flipX = true;
            }
            /*
                        if (x == 0 && z == 0)
                        {
                            frogRenderer.sprite = frogSprites[0];
                        }
                        else if (z > 0)
                        {
                            frogRenderer.sprite = frogSprites[2];
                        }
                        else if (x > 0)
                        {
                            frogRenderer.sprite = frogSprites[1];
                            isFacingRight = false;
                        }
                        else if (x < 0)
                        {
                            frogRenderer.sprite = frogSprites[1];
                            if(isFacingRight == false)
                            {
                                frogRenderer.flipX = true;
                            }
                            isFacingRight = true;
                        }*/
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
            }

            //Speeding
            if(canSpeed == true && Input.GetKey(KeyCode.E))
            {
                speed = 10f;
            }
            else
            {
                speed = 5f;
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

        if(other.gameObject.tag == "Predator")
        {
            canSpeed = true;
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
        }
        if(collision.gameObject.tag == "Shelter")
        {
            gameManager.inShelter = true;
        }
    }
}
