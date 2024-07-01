using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject MouseObj;
    Mouse mouseScr;

    Vector2 mousePos;
    private Camera cam;
    bool TreeNear = false;
    GameObject TreeNearObj = null;
    
    void Start()
    {
        mouseScr = MouseObj.GetComponent<Mouse>();
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
            if (TreeNearObj == mouseScr.TreeObjHere)
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
            Debug.Log("tree");
            TreeNear = true;
            TreeNearObj = other.gameObject;
        }


    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(TreeNear && other.gameObject.CompareTag("Tree")) 
        {
 
                Debug.Log("no tree");
                TreeNear = false;
                TreeNearObj = null;
           
        }
    }

}
