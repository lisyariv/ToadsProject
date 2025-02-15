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
    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<TMP_Text>();
        status.text = "";
        frog = GameObject.Find("Player").GetComponent<FrogMovement>();
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
            attackTimer += Time.deltaTime;
            if (attackTimer >= 4f)
            {
                
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            status.text = "";

        }
    }

    
}
