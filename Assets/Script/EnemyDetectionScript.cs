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
    // Start is called before the first frame update
    void Start()
    {
        status = GameObject.Find("Status").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            status.text = "You have been Spotted";
            //attack

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
