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
    }
}
