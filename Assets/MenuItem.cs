using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MenuItem : MonoBehaviour
{
    
    [SerializeField] MeshRenderer _Text1;
    [SerializeField] string  functionName;
    [SerializeField] TextMeshPro text;
    [SerializeField] Sprite icon1;
    [SerializeField] Sprite icon2;
    [SerializeField] SpriteRenderer icon;

    private bool Soundon=true;

    public void Sound()
    {
        if (Soundon == true)
        {
            AudioManager.instance.pause();
            icon.sprite = icon2;
            text.SetText("Sound On");

        }
        else
        {
            AudioManager.instance.resume();
            icon.sprite = icon1;
            text.SetText("Sound Off");
        }
        Soundon = !Soundon;
        Debug.Log("Sound");
    }
    
    public void Resume()
    {
        transform.parent.gameObject.SetActive(false);
        FindObjectOfType<Timer>().StartTimer();
    }
    

    public void OnMouseDown() 
    {
        Invoke(functionName,0.1f);
        
    }

    public void Help() 
    {
        Debug.Log("Help");
    }

    public void Hints() 
    {
        Debug.Log("Hints");
    }

     public void MainMenu() 
    {
        FindObjectOfType<Navigate>().BackHome();
    }

    
    

    // Start is called before the first frame update
    void Start()
    {
        _Text1.sortingOrder = 13;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
