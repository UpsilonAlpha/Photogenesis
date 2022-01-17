using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockCheck : MonoBehaviour
{
    public Sprite unknown;
    public string index;
    public Sprite lightImage;

    public void MenuOpen()
    {
        foreach (LockCheck p in GetComponentsInChildren<LockCheck>())
        {
            p.Check();
        }
    }

    public void Check()
    {
        
        if(unknown != null)
        {
            string truth = PlayerPrefs.GetString(index, "");
            if (truth != "true")
            {
                GetComponent<Image>().sprite = unknown;
            }
            else
            {
                GetComponent<Image>().sprite = lightImage;
                LeanTween.scale(this.gameObject, new Vector3(17f,17f,1), 1f).setEaseInOutBack().setDelay(1f);
                LeanTween.scale(this.gameObject, new Vector3(15f,15f,1), 1f).setEaseInOutBack().setDelay(2f);
            }
        }
    }
}
