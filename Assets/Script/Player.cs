using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    Vector2 mousePos;
    private Camera cam;
    bool TreeNear = false;
    GameObject TreeNearObj = null;
    void Start()
    {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("кнопка");
            agent.SetDestination(mousePos);
            
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && TreeNear)
        {
            Debug.Log("Tree deleted");
            Destroy(TreeNearObj);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.CompareTag("Tree"))
        {
            Debug.Log("tree");
            TreeNear = true;
            TreeNearObj = other.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (TreeNear)
        {
            TreeNear = false;
            TreeNearObj = null;
        }
    }
}
