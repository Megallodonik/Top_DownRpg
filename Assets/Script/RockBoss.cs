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
    [SerializeField] List<GameObject> RockTurretList = new List<GameObject>();
    [SerializeField] GameObject Player;
    [SerializeField] GameObject BossHeart;
    [SerializeField] GameObject DottedCircle;
    [SerializeField] GameObject FireHole;
    [SerializeField] GameObject BlackCube;
    [SerializeField] GameObject RockBullet;
    [SerializeField] GameObject RockTurret;

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
        for (int i = 0; i < DottedCircleList.Count; i++)
        {
            

            
            GameObject temp = Instantiate(FireHole, DottedCircleList[i].transform.position, Quaternion.identity);
            temp.SetActive(false);
            FireHoleList.Add(temp);
        }
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Invoke("ChoosingAttack", 5f);
        //StartCoroutine(SpawnRocksAttackCor());
        //StartCoroutine(BulletHellAttackCor());
        //StartCoroutine(TurretAttackCor());

        StartCoroutine(spawnHearts());
        
    }
    public void ChoosingAttack()
    {
        StopCoroutine(BulletHellAttackCor());
        StopCoroutine(TurretAttackCor());
        StopCoroutine(SpawnRocksAttackCor());
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
        BulletHellAttack = 1,
        RockTurretAttack = 2
    }
    

    private IEnumerator BulletHellAttackCor()
    {
        
        BlackCube.transform.position = Player.transform.position;
        BlackCube.SetActive(true);
        for (int i = 0; i < 40; i++)
        {
            Instantiate(RockBullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            if (i == 39)
            {
                StartCoroutine(ChooseAttack());
            }
        }
        BlackCube.SetActive(false);

        
    }
    private IEnumerator TurretAttackCor()
    {
        
        for (int i = 0; i < RockTurretList.Count; i++)
        {
            RockTurretList[i].SetActive(true);


        }

        yield return new WaitForSeconds(23f);

        
    }
    private IEnumerator SpawnRocksAttackCor()
    {
        bool passed = false;

            for (int b = 0; b < DottedCircleList.Count; b++)
            {
                DottedCircleList[b].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                DottedCircleList[b].gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(2f);

            for (int b = 0; b < DottedCircleList.Count; b++)
            {
                DottedCircleList[b].gameObject.SetActive(true);

            }
            yield return new WaitForSeconds(0.5f);

            for (int b = 0; b < DottedCircleList.Count; b++)
            {

                DottedCircleList[b].gameObject.SetActive(false);

            }
            yield return new WaitForSeconds(0.5f);
            for (int b = 0; b < FireHoleList.Count; b++)
            {
                FireHoleList[b].gameObject.SetActive(true);

            }
            yield return new WaitForSeconds(2f);
            for (int b = 0; b < FireHoleList.Count; b++)
            {
                FireHoleList[b].gameObject.SetActive(false);
                passed = true;
            }
        if (passed)
        {
            StartCoroutine(ChooseAttack());
        }


        }

    private IEnumerator spawnHearts()
    {

        var position = new Vector3(UnityEngine.Random.Range(-12f, 12f), UnityEngine.Random.Range(-7f, 7f), 0);

        Instantiate(BossHeart, position, Quaternion.identity);

        yield return new WaitForSeconds(10f);
        StartCoroutine(spawnHearts());
    }






    private IEnumerator ChooseAttack()
    {

        for (int i = 0; i < RockTurretList.Count; i++)
        {
            RockTurretList[i].SetActive(false);


        }
        StopCoroutine(BulletHellAttackCor());
        StopCoroutine(TurretAttackCor());
        StopCoroutine(SpawnRocksAttackCor());
        Debug.Log("Chooseattackcor");
        yield return new WaitForSeconds(2f);
        int AttacksCount = Enum.GetNames(typeof(Attacks)).Length;
        int rnd = UnityEngine.Random.Range(0, AttacksCount);

        switch ((Attacks)rnd)
        {
            case Attacks.SpawnRocksAttack:
                if (LastAttack != Attacks.SpawnRocksAttack)
                {

                    for (int i = 0; i < RockTurretList.Count; i++)
                    {
                        RockTurretList[i].SetActive(false);


                    }
                    Debug.Log("DashAttack");
                    LastAttack = Attacks.SpawnRocksAttack;
                    StopCoroutine(BulletHellAttackCor());
                    StopCoroutine(TurretAttackCor());
                    StartCoroutine(SpawnRocksAttackCor());
                    


                    yield return null;
                    break;

                }
                else
                {

                    for (int i = 0; i < RockTurretList.Count; i++)
                    {
                        RockTurretList[i].SetActive(false);


                    }
                    StopCoroutine(BulletHellAttackCor());
                    StopCoroutine(TurretAttackCor());
                    StopCoroutine(SpawnRocksAttackCor());
                    ChoosingAttack();


                    yield return null;
                    break;
                }
            case Attacks.BulletHellAttack:
                if (LastAttack != Attacks.BulletHellAttack)
                {

                    for (int i = 0; i < RockTurretList.Count; i++)
                    {
                        RockTurretList[i].SetActive(false);


                    }
                    StartCoroutine(BulletHellAttackCor());
                    StopCoroutine(TurretAttackCor());
                    StopCoroutine(SpawnRocksAttackCor());
                    Debug.Log("AroundAttack");
                    LastAttack = Attacks.BulletHellAttack;
                    
                    yield return null;
                    break;
                }
                else
                {

                    for (int i = 0; i < RockTurretList.Count; i++)
                    {
                        RockTurretList[i].SetActive(false);


                    }
                    StopCoroutine(BulletHellAttackCor());
                    StopCoroutine(TurretAttackCor());
                    StopCoroutine(SpawnRocksAttackCor());
                    ChoosingAttack();
                    yield return null;
                    break;
                }
            case Attacks.RockTurretAttack:
                if (LastAttack != Attacks.RockTurretAttack)
                {
                    StopCoroutine(BulletHellAttackCor());
                    StartCoroutine(TurretAttackCor());
                    StopCoroutine(SpawnRocksAttackCor());
                    Debug.Log("RockTurret");
                    LastAttack = Attacks.RockTurretAttack;
                    
                    yield return null;
                    break;
                }
                else
                {

                    for (int i = 0; i < RockTurretList.Count; i++)
                    {
                        RockTurretList[i].SetActive(false);


                    }
                    StopCoroutine(BulletHellAttackCor());
                    StopCoroutine(TurretAttackCor());
                    StopCoroutine(SpawnRocksAttackCor());
                    ChoosingAttack();
                    yield return null;
                    break;
                }

        }


    }
}
