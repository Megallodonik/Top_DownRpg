using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, 0.01f);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}