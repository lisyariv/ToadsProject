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
    public Rigidbody player;
    public Slider staminaBar;
    public GM gameManager;
    public TMP_Text StaminaTxt;
    public bool onGround;
    
   
    
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
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       //Moving left to right
       if(gameManager.isCollecting == false && staminaBar.value > 0f)
        {
            player.isKinematic = false;

            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(x, 0, z);
            transform.Translate(moveDirection * Time.deltaTime * speed);

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
        }
       else if(onGround == false && gameManager.isCollecting == true)
        {
            player.isKinematic = true;
        }
       
       
        //Adding to Stamina Bar
        if(gameManager.preyCount == 1)
        {
            staminaBar.value += 1;
            gameManager.preyCount = 0;
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
        }
        if(collision.gameObject.tag == "Shelter")
        {
            gameManager.inShelter = true;
        }
    }

    
}
