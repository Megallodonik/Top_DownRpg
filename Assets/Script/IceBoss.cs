using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static IceBoss;
using static UnityEngine.GraphicsBuffer;

public class IceBoss : Boss
{

    [SerializeField] Transform player;

    //[SerializeField] float radius = 15;
    [SerializeField] float radiusAroundPlayer = 3f;
    [SerializeField] float speedAroundPlayer = 15f;
    [SerializeField] List<GameObject> TreeList = new List<GameObject>();
    [SerializeField] List<GameObject> BossHPList = new List<GameObject>();
    [SerializeField] List<GameObject> IceSpikesList = new List<GameObject>();
    [SerializeField] List<Vector3> DashPoints = new List<Vector3>();
    float positionX, positionY, angle = 0f;
    public int BossHP = 12;
    private Attacks LastAttack;
    private Rigidbody2D rb;
    private float moveSpeed = 150f;
    public int TreeCount;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(ChooseAttack());
        //Debug.Log("dashAttack");
        //Invoke("DashAttack", 5f);
        Invoke("IceSpikesAttack", 2f);
        
        //Invoke("ChoosingAttack", 5f);
    }
    private void ChoosingAttack()
    {
        StartCoroutine(ChooseAttack());
    }
    // Update is called once per frame
    void Update()
    {

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
        SquareAttack = 1,
        AroundPlayerAttack = 2
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

    private void SquareAttack()
    {
        StartCoroutine(SquareAttackCor());
    }


    private IEnumerator DashAttackCor_1()
    {
        for (int i = 0; i < 15; i++)
        {
            int rnd = UnityEngine.Random.Range(0, DashPoints.Count);
            while (transform.position != DashPoints[rnd])
            {
                transform.rotation *= Quaternion.Euler(0f, 0f, 10f);
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, DashPoints[rnd], step);
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

    private IEnumerator SquareAttackCor()
    {

        TreeCount = 0;
        for (int i = 0; i < TreeList.Count; i++)
        {
            TreeList[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HitPlayer(-1);
        }
    }

    private IEnumerator IceSpikesAttackCor()
    {
        for (int i = 0; i < IceSpikesList.Count; i++)
        {
            IceSpikesList[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(0.5f);



        }
        StartCoroutine(ChooseAttack());
    }






    private IEnumerator ChooseAttack()
    {
        int AttacksCount = Enum.GetNames(typeof(Attacks)).Length;
        int rnd = UnityEngine.Random.Range(0, AttacksCount);

        switch ((Attacks)rnd)
        {
            case Attacks.DashAttack:
                if (LastAttack != Attacks.DashAttack)
                {

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

            case Attacks.SquareAttack:
                if (LastAttack != Attacks.SquareAttack)
                {

                    LastAttack = Attacks.SquareAttack;
                    SquareAttack();
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
