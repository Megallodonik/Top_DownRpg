using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{




    Vector2 mousePos;
    private Camera cam;
    bool TreeNear = false;
    GameObject TreeNearObj = null;
    [SerializeField] GameObject AxeImg;
    void Start()
    {

        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //    Debug.Log("кнопка");
        //    agent.SetDestination(mousePos);
            
        //}

    }
    public void OnTreeCut()
    {
        if (TreeNear)
        {
            if (TreeNearObj != null)
            {
                Debug.Log("Tree deleted");
                Destroy(TreeNearObj);
            }

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Tree"))
        {
            AxeImg.SetActive(true);
            Debug.Log("tree");
            TreeNear = true;
            TreeNearObj = other.gameObject;
        }


    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(TreeNear && other.gameObject.CompareTag("Tree")) 
        {
                AxeImg.SetActive(false);
                Debug.Log("no tree");
                TreeNear = false;
                TreeNearObj = null;
           
        }
    }

}
