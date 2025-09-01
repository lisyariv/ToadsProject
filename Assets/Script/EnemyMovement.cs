using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float speed;
    [SerializeField] public float leftPatrolX, rightPatrolX;
    [SerializeField] private float minPauseTime, maxPauseTime;
    [SerializeField] private float minWalkTime, maxWalkTime;
    [SerializeField] private int facingDirection = -1;

    private float randomTime, timer;
    private bool isWalking;
    private bool isFlipping;
    // Start is called before the first frame update
    private void Start()
    {
        randomTime = Random.Range(minWalkTime, maxWalkTime);
        anim.SetInteger("facingDirection", facingDirection);
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
        //anim.SetBool("isWalking", isWalking ? true : false);
        randomTime = isWalking ? Random.Range(minWalkTime, maxWalkTime) : Random.Range(minPauseTime, maxPauseTime);
        timer = 0;
    }
}
