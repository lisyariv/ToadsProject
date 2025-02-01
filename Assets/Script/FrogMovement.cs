using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 5.0f;
    public Vector3 jumpMovement;
    public Vector3 flyMovement;
    public bool canJump;
    public bool canFly;
    public GameObject player;
    public Slider staminaBar;

    // Start is called before the first frame update
    void Start()
    {
        staminaBar.gameObject.SetActive(false);
        staminaBar.maxValue = 5f;
        canJump = false;
        canFly = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      /* if (player.transform.position.y > 10)
        {
            canFly = false;
        }
        else
        {
            canFly = true;
        }*/
       //Moving left to right
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

        if (canFly == true && Input.GetKey(KeyCode.F))
        {
            GetComponent<Rigidbody>().AddForce(flyMovement);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
            canFly = true;
        }
    }
    
}
