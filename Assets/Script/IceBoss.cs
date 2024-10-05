using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;
using static IceBoss;
using static UnityEngine.GraphicsBuffer;

public class IceBoss : Boss
{

    

    //[SerializeField] float radius = 15;
    [SerializeField] float radiusAroundPlayer = 3f;
    [SerializeField] float speedAroundPlayer = 15f;
    [SerializeField] List<GameObject> TreeList = new List<GameObject>();
    [SerializeField] List<GameObject> BossHPList = new List<GameObject>();
    [SerializeField] List<GameObject> IceSpikesList = new List<GameObject>();
    [SerializeField] List<GameObject> DottedLaserList = new List<GameObject>();
    [SerializeField] List<Vector3> DashPoints = new List<Vector3>();
    [SerializeField] GameObject Player;
    [SerializeField] GameObject GreenSpike;
    [SerializeField] GameObject BossHeart;
    LineRenderer lineRenderer;

    float positionX, positionY, angle = 0f;
    public int BossHP = 12;
    private Attacks LastAttack;
    private Rigidbody2D rb;
    private float moveSpeed = 150f;
    public int TreeCount;
    bool rotation = true;
    bool followPlayer = false;
    private bool greenSpikeSpawn = false;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(ChooseAttack());
        //Debug.Log("dashAttack");
        //Invoke("DashAttack", 2f);
        //Invoke("IceSpikesAttack", 2f);
        StartCoroutine(spawnHearts());
        Invoke("ChoosingAttack", 5f);
    }
    private void ChoosingAttack()
    {
        StartCoroutine(ChooseAttack());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotation)
        {
            transform.rotation *= Quaternion.Euler(0f, 0f, 10f);
        }
        
    }

    private void OnEnable()
    {
        TreeCutter.OnTreeCut += TreeCutter_OnTreeCut;


    }

    private void OnDisable()
    {

        TreeCutter.OnTreeCut -= TreeCutter_OnTreeCut;

    }
    private void BossHPChange(int HpChange)
    {
        BossHP += HpChange;


        BossHPList[BossHP].gameObject.SetActive(false);

        if (BossHP <= 0)
        {
            StopAllCoroutines();
        }
    }
    private void TreeCutter_OnTreeCut()
    {

        BossHPChange(-1);

        TreeCount++;
        if (TreeCount >= 4)
        {
            StartCoroutine(ChooseAttack());

        }


    }

    public enum Attacks
    {
        DashAttack = 0,
        AroundPlayerAttack = 1
    }
    //private void CircleAttack()
    //{
    //    GameObject TreeObj = Instantiate(Tree);
    //    float x = UnityEngine.Random.Range(-radius, radius);
    //    float y = UnityEngine.Random.Range(-radius, radius);
    //    Vector2 pos = transform.position;
    //    TreeObj.transform.position = pos + new Vector2(x, y);
    //}
    private void DashAttack()
    {
        StartCoroutine(DashAttackCor_1());
        
    }

    private void IceSpikesAttack()
    {

        StartCoroutine(IceSpikesAttackCor());
    }

 
    private IEnumerator spawnHearts()
    {
        var position = new Vector3(UnityEngine.Random.Range(-12f, 12f), UnityEngine.Random.Range(-7f, 7f), 0);

        Instantiate(BossHeart, position, Quaternion.identity);

        yield return new WaitForSeconds(5f);
        StartCoroutine(spawnHearts());
    }

    private IEnumerator DashAttackCor_1()
    {
        Debug.Log("DashAttackCor");

        for (int i = 0; i < 15; i++)
        {
            int rnd = UnityEngine.Random.Range(0, DashPoints.Count);
            Vector3[] positions = { transform.position, DashPoints[rnd] };
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(positions);
            yield return new WaitForSeconds(0.5f);
            lineRenderer.positionCount = 0;
            while (transform.position != DashPoints[rnd])
            {
                
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, DashPoints[rnd], step);
                
                yield return new WaitForSeconds(0.01f);
            }
            
            Vector3 pos = Player.transform.position;
            Vector3[] positions_2 = { transform.position, pos };
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(positions_2);
            yield return new WaitForSeconds(0.1f);
            lineRenderer.positionCount = 0;
            while (transform.position != pos)
            {

                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, pos, step);

                yield return new WaitForSeconds(0.01f);
            }


        }
        while (transform.position != new Vector3(0, 0, 0))
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), step);
            yield return new WaitForSeconds(0.01f);
        }
        
        StartCoroutine(ChooseAttack());
    }

    //private IEnumerator SquareAttackCor()
    //{
    //    Debug.Log("SquareAttackCor");

    //    TreeCount = 0;
    //    for (int i = 0; i < TreeList.Count; i++)
    //    {
    //        TreeList[i].gameObject.SetActive(true);
    //        yield return new WaitForSeconds(1f);
    //    }


    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HitPlayer(-1);
        }
    }

    private IEnumerator IceSpikesAttackCor()
    {
        Debug.Log("IceSpikeAttackCor");

        for (int i = 0; i < IceSpikesList.Count; i++)
        {
            DottedLaserList[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            DottedLaserList[i].gameObject.SetActive(false);
            IceSpikesList[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);



        }

        while (transform.position != new Vector3(0, 0, 0))
        {
            float speed = 25f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), step);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(ChooseAttack());
    }






    private IEnumerator ChooseAttack()
    {
        StopCoroutine(DashAttackCor_1());
        StopCoroutine(IceSpikesAttackCor());
        Debug.Log("Chooseattackcor");
        yield return new WaitForSeconds(2f);
        int AttacksCount = Enum.GetNames(typeof(Attacks)).Length;
        int rnd = UnityEngine.Random.Range(0, AttacksCount);

        switch ((Attacks)rnd)
        {
            case Attacks.DashAttack:
                if (LastAttack != Attacks.DashAttack)
                {
                    Debug.Log("DashAttack");
                    LastAttack = Attacks.DashAttack;
                    DashAttack();

                    yield return null;
                    break;

                }
                else
                {
                    StartCoroutine(ChooseAttack());
                    yield return null;
                    break;
                }
            case Attacks.AroundPlayerAttack:
                if (LastAttack != Attacks.AroundPlayerAttack)
                {
                    Debug.Log("AroundAttack");
                    LastAttack = Attacks.AroundPlayerAttack;
                    IceSpikesAttack();
                    yield return null;
                    break;
                }
                else
                {
                    StartCoroutine(ChooseAttack());
                    yield return null;
                    break;
                }

        }


    }
}
