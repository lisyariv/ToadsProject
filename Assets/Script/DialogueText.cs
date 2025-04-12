using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueText : MonoBehaviour
{
    public List<string> DialogueTxt;
    public TMP_Text Dialogue;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        Dialogue.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void nextButton()
    {
        if(index < DialogueTxt.Count)
        {
            Dialogue.text = DialogueTxt[index];
            index++;
        }
        else
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
}
