using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreyCollection : MonoBehaviour
{
    public GM GM;
    public TMP_Text infoTxt;
    public Slider bar;
    public IEnumerator collect;
    public float timer;
    


    // Start is called before the first frame update
    void Start()
    {
        bar.gameObject.SetActive(false);
        bar.maxValue = 1f;
        infoTxt.text = "";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            
            infoTxt.text = "Collecting";
            bar.gameObject.SetActive(true);
            collect = Collecting();
            StartCoroutine(collect);
            GM.isCollecting = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timer += Time.deltaTime;
            
            bar.value = (timer / 2);
        }
        GM.isCollecting = true;
        other.gameObject.transform.position = transform.position;
    }
    void OnTriggerExit(Collider other)
    {
        StopCoroutine(collect);
        infoTxt.text = "";
        timer = 0;
        bar.gameObject.SetActive(false);
        bar.value = 0;
        GM.isCollecting = false;
    }

    public IEnumerator Collecting()
    {
        yield return new WaitForSeconds(2f);
       

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
