using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueText : MonoBehaviour
{
    public List<string> DialogueTxt;
    public List<Sprite> sceneImages;
    public Image currentImage;
    public TMP_Text Dialogue;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        Dialogue.text = DialogueTxt[0];
        index = 1;
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
            currentImage.sprite = sceneImages[index];
            index++;
        }
        else
        {
            SceneManager.LoadScene("GameplayScene");
        }
    }
}
