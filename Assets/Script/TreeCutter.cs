using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCutter : MonoBehaviour
{
    bool TreeNear = false;
    GameObject TreeNearObj = null;
    [SerializeField] GameObject AxeImg;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static event Action OnTreeCut;


    public void TreeCutting()
    {
        if (TreeNear)
        {
            if (TreeNearObj != null)
            {
                Debug.Log("Tree deleted");
                TreeNearObj.SetActive(false);
                
                OnTreeCut?.Invoke();
                
            }

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("TreeBoss"))
        {
            AxeImg.SetActive(true);
            Debug.Log("tree");
            TreeNear = true;
            TreeNearObj = other.gameObject;
        }


    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (TreeNear && other.gameObject.CompareTag("TreeBoss"))
        {
            AxeImg.SetActive(false);
            Debug.Log("no tree");
            TreeNear = false;
            TreeNearObj = null;

        }
    }


}
