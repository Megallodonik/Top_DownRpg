using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBoss : Boss
{
    //[SerializeField] float radius = 15;
    [SerializeField] float radiusAroundPlayer = 3f;
    [SerializeField] float speedAroundPlayer = 15f;
    [SerializeField] List<GameObject> BossHPList = new List<GameObject>();
    [SerializeField] List<GameObject> DottedCircleList = new List<GameObject>();
    [SerializeField] List<GameObject> FireHoleList = new List<GameObject>();
    [SerializeField] GameObject Player;
    [SerializeField] GameObject BossHeart;
    [SerializeField] GameObject DottedCircle;
    [SerializeField] GameObject FireHole;
    LineRenderer lineRenderer;

    float positionX, positionY, angle = 0f;
    public int BossHP = 12;
    private Attacks LastAttack;
    private Rigidbody2D rb;
    private float moveSpeed = 150f;
    public int TreeCount;
    bool rotation = true;


    void Start()
    {
        for (int i = 0; i < 60; i++)
        {
            var position = new Vector3(UnityEngine.Random.Range(-12f, 12f), UnityEngine.Random.Range(-7f, 7f), 0);

            GameObject temp = Instantiate(DottedCircle, position, Quaternion.identity);
            GameObject temp_1 = Instantiate(FireHole, position, Quaternion.identity);
            temp.SetActive(false);
            temp_1.SetActive(false);
            DottedCircleList.Add(temp);
            FireHoleList.Add(temp_1);
        }
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //StartCoroutine(ChooseAttack());
        StartCoroutine(SpawnRocksAttackCor());
        StartCoroutine(spawnHearts());
        
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
            transform.rotation *= Quaternion.Euler(0f, 0f, 2f);
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
        SpawnRocksAttack = 0,
        BulletHellAttack = 1
    }

    private IEnumerator SpawnRocksAttackCor()
    {
        for (int i = 0; i < DottedCircleList.Count; i++)
        {
            DottedCircleList[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < DottedCircleList.Count; i++)
        {
            DottedCircleList[i].gameObject.SetActive(false);
            FireHoleList[i].gameObject.SetActive(true);

        }
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator spawnHearts()
    {
        var position = new Vector3(UnityEngine.Random.Range(-12f, 12f), UnityEngine.Random.Range(-7f, 7f), 0);

        Instantiate(BossHeart, position, Quaternion.identity);

        yield return new WaitForSeconds(5f);
        StartCoroutine(spawnHearts());
    }






    private IEnumerator ChooseAttack()
    {

        Debug.Log("Chooseattackcor");
        yield return new WaitForSeconds(2f);
        int AttacksCount = Enum.GetNames(typeof(Attacks)).Length;
        int rnd = UnityEngine.Random.Range(0, AttacksCount);

        switch ((Attacks)rnd)
        {
            case Attacks.SpawnRocksAttack:
                if (LastAttack != Attacks.SpawnRocksAttack)
                {
                    Debug.Log("DashAttack");
                    LastAttack = Attacks.SpawnRocksAttack;
                    

                    yield return null;
                    break;

                }
                else
                {
                    StartCoroutine(ChooseAttack());
                    yield return null;
                    break;
                }
            case Attacks.BulletHellAttack:
                if (LastAttack != Attacks.BulletHellAttack)
                {
                    Debug.Log("AroundAttack");
                    LastAttack = Attacks.BulletHellAttack;
                    
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
