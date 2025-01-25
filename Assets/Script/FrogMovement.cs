using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 5.0f;
    public Vector3 jumpMovement;
    public bool canJump;
    // Start is called before the first frame update
    void Start()
    {
        canJump = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(x, 0, z);
        transform.Translate(moveDirection * Time.deltaTime * speed);
        if (canJump)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                canJump = false;
                GetComponent<Rigidbody>().AddForce(jumpMovement);
            }
        }   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }
    
}
