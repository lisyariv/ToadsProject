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
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameManager.isInBush == false )
        {
            status.text = "You have been Spotted";
            StartCoroutine(attack);

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            status.text = "";

        }
    }

    public IEnumerator attacked()
    {
        attack = attacked();
        attackTimer += Time.deltaTime;
        while (hp >= 0)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer == 4f)
            {
                frog.staminaBar.value -= 1;
            }
           
        }
        yield return frog.staminaBar.value;
    }
}
