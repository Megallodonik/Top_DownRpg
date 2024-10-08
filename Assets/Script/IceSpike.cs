using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : Obstacle
{
    float moveSpeed = 70f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(fly());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HitPlayer(-1);
        }
    }

    private IEnumerator fly()
    {
        if (transform.position.x > 0)
        {
            float startPos = transform.position.x;
            while (transform.position.x != startPos - 30)
            {

                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(startPos - 30, transform.position.y), step);
                yield return new WaitForFixedUpdate();
            }
            transform.position = new Vector3(startPos, transform.position.y);
            this.gameObject.SetActive(false);
        }
        else
        {
            float startPos = transform.position.x;
            while (transform.position.x != startPos + 30)
            {

                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(startPos + 30, transform.position.y), step);
                yield return new WaitForFixedUpdate();
            }
            transform.position = new Vector3(startPos, transform.position.y);
            this.gameObject.SetActive(false);
        }

    }
}
