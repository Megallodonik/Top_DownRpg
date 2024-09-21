using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TrentProtectorBoss;
using static UnityEngine.GraphicsBuffer;

public class TrentProtectorBoss : Boss
{

    [SerializeField] Transform player;

    //[SerializeField] float radius = 15;
    [SerializeField] GameObject LaserCircle;
    [SerializeField] float radiusAroundPlayer = 3f;
    [SerializeField] float speedAroundPlayer = 15f;
    [SerializeField] List<GameObject> TreeList = new List<GameObject>();
    [SerializeField] List<GameObject> LaserList = new List<GameObject>();
    [SerializeField] List<GameObject> DottedLaserList = new List<GameObject>();
    [SerializeField] List<GameObject> BossHPList = new List<GameObject>();
    [SerializeField] GameObject GreenSpike;
    float positionX, positionY, angle = 0f;
    public int BossHP = 12;
    public Attacks LastAttack;
    private Rigidbody2D rb;
    private float moveSpeed = 15f;
    public int TreeCount;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChooseAttack());
        //Debug.Log("circleAttack");
        //CircleAttack();
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
        CircleAttack = 0,
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
    private void CircleAttack()
    {
        
        StartCoroutine(LaserCircleRotation());
    }

    private void AroundPlayerAttack()
    {
        StartCoroutine(AroundPlayerAttackCor()); 
    }

    private void SquareAttack()
    {
        StartCoroutine(SquareAttackCor());
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

    private IEnumerator GreenSpikeSpawn()
    {
        for (int i = 0; i < 60; i++)
        {
            var position = new Vector3(UnityEngine.Random.Range(-12f, 12f), UnityEngine.Random.Range(-7f, 7f), 0);
            Instantiate(GreenSpike, position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }


    private IEnumerator AroundPlayerAttackCor()
    {
        StartCoroutine(GreenSpikeSpawn());
        // босс вращается вокруг игрока
        for (int i = 0; i < 360; i++)
        {
            
            Transform center = player.transform;

            positionX = center.position.x + Mathf.Cos(angle) * radiusAroundPlayer;
            positionY = center.position.y + Mathf.Sin(angle) * radiusAroundPlayer;
            transform.position = new Vector2(positionX, positionY);

            angle = angle + Time.deltaTime * speedAroundPlayer;

            if (speedAroundPlayer > 0 && angle >= 360f)
            {
                angle -= 360;
            }
            if (speedAroundPlayer < 0 && angle <= 0)
            {
                angle += 360;
            }
            yield return new WaitForSeconds(0.01f);
        }
        while (transform.position != new Vector3(0,0,0))
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), step);
            yield return new WaitForSeconds(0.01f);
        }


        StartCoroutine(ChooseAttack());
    }

    private IEnumerator LaserSpawn()
    {
        for (int i = 0; i < LaserList.Count; i++)
        {
            DottedLaserList[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            LaserList[i].gameObject.SetActive(true);
            DottedLaserList[i].gameObject.SetActive(false);

            yield return new WaitForSeconds(1f);
        }
        StartCoroutine(LaserDeSpawn());
    }
    private IEnumerator LaserDeSpawn()
    {
        for (int i = 0; i < LaserList.Count; i++)
        {

            LaserList[i].gameObject.SetActive(false);


            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator LaserCircleRotation()
    {
        StartCoroutine(LaserSpawn());
        LaserCircle.SetActive(true);
        // вокруг босса вращается спрайт с лазером
        for (int i = 0; i < 1200; i++)
        {
            Debug.Log("laserCircleRot coroutine");
            LaserCircle.transform.rotation *= Quaternion.Euler(0f, 0f, 10f);
            yield return new WaitForSeconds(0.01f);
            
        }
        LaserCircle.SetActive(false);
        StartCoroutine(ChooseAttack());
    }


    private IEnumerator ChooseAttack()
    {
        int AttacksCount = Enum.GetNames(typeof(Attacks)).Length;
        int rnd = UnityEngine.Random.Range(0, AttacksCount);

        switch ((Attacks)rnd)
        {
            case Attacks.CircleAttack:
                if (LastAttack != Attacks.CircleAttack)
                {
                    Debug.Log("circleAttack");
                    CircleAttack();
                    LastAttack = Attacks.CircleAttack;
                    yield return new WaitForSeconds(3);
                    break;
                    
                }
                else
                {
                    StartCoroutine(ChooseAttack());
                    break;
                }

            case Attacks.SquareAttack:
                if (LastAttack != Attacks.SquareAttack)
                {
                    Debug.Log("SqareAttack");
                    LastAttack = Attacks.SquareAttack;
                    SquareAttack();
                    yield return new WaitForSeconds(3);
                    break;
                }
                else
                {
                    StartCoroutine(ChooseAttack());
                    break;
                }
            case Attacks.AroundPlayerAttack:
                if (LastAttack != Attacks.AroundPlayerAttack)
                {
                    Debug.Log("AroundPlayerAttack");
                    AroundPlayerAttack();
                    LastAttack = Attacks.AroundPlayerAttack;
                    yield return new WaitForSeconds(10);
                    break;
                }
                else
                {
                    StartCoroutine(ChooseAttack());
                    break;
                }

        }
        
   
    }
}
