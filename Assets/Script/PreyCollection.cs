using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreyCollection : MonoBehaviour
{
    public GM GM;
    public IEnumerator collect;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            collect = Collecting();
            StartCoroutine(collect);
            GM.isCollecting = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        GM.isCollecting = true;
    }
    void OnTriggerExit(Collider other)
    {
        StopCoroutine(collect);
        GM.isCollecting = false;
    }

    public IEnumerator Collecting()
    {
        yield return new WaitForSeconds(0f);

        Destroy(gameObject);
        GM.isCollecting = false;
        GM.isCollected = true;

        if(gameObject.tag == "fly")
        {
            GM.isFlyCollected = true;
            Debug.Log("The Fly has been collected.");
        }

        GM.preyCount += 1;
        
    }

}
