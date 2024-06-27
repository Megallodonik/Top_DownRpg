using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    Vector2 mousePos;
    private Camera cam;
    [SerializeField] GameObject Cursor;
    [SerializeField] GameObject AxeSprite;
    [SerializeField] GameObject PlayerObj;
    Player player;
    public GameObject TreeObjHere;
    bool isTree = false;
    // Start is called before the first frame update
    void Start()
    {
        player = PlayerObj.GetComponent<Player>();
        cam = Camera.main;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.E) && isTree)
        {
            player.OnTreeCut();
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Cursor.transform.position = mousePos;
        if (isTree )
        {
            AxeSprite.SetActive(true);
        }
        else
        {
            AxeSprite.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trunk"))
        {

            TreeObjHere = other.gameObject.transform.parent.gameObject;
            isTree = true;
            Debug.Log(isTree);
        }
        else
        {
            

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Trunk"))
        {
            TreeObjHere = null;
            isTree = false;
            Debug.Log(isTree);
        }
    }
}
