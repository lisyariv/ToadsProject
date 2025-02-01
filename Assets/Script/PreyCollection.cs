using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PreyCollection : MonoBehaviour
{
    public GM GM;
    public bool isCollected;
    public TMP_Text infoTxt;
    public Slider bar;
    public IEnumerator collect;
    public float timer;
    

    // Start is called before the first frame update
    void Start()
    {
        bar.gameObject.SetActive(false);
        bar.maxValue = 2f;
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
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            timer += Time.deltaTime;
            
            bar.value = (timer / 2);
        }
    }
    void OnTriggerExit(Collider other)
    {
        StopCoroutine(collect);
        infoTxt.text = "";
        timer = 0;
        bar.gameObject.SetActive(false);
        bar.value = 0;


    }

    public IEnumerator Collecting()
    {
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);

        GM.isCollected = true;
        GM.preyCount += 1;
        
        //gameObject.isCollected = true;
    }

}
