using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TrentProtectorBoss;

public class TrentProtectorBoss : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] GameObject Tree;
    [SerializeField] float radius = 15;
    [SerializeField] GameObject LaserCircle;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChooseAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum Attacks
    {
        CircleAttack = 0,
        SquareAttack = 1
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
        LaserCircle.SetActive(true);
        StartCoroutine(LaserCircleRotation());
    }
    IEnumerator LaserCircleRotation()
    {
        Debug.Log("laserCircleRot coroutine");
        LaserCircle.transform.rotation *= Quaternion.Euler(0f, 0f, 10f);
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(LaserCircleRotation());
    }
    IEnumerator ChooseAttack()
    {
        int AttacksCount = Enum.GetNames(typeof(Attacks)).Length;
        int rnd = UnityEngine.Random.Range(0, AttacksCount);
        switch ((Attacks)rnd)
        {
            case Attacks.CircleAttack:
                Debug.Log("circleAttack");
                CircleAttack();
                yield return new WaitForSeconds(3); //ожидаем 5 секунд(время можете установить своё
                StartCoroutine(ChooseAttack());
                break;
            case Attacks.SquareAttack:
                Debug.Log("SqareAttack");
                yield return new WaitForSeconds(3); //ожидаем 5 секунд(время можете установить своё
                StartCoroutine(ChooseAttack());
                break;

        }
        
   
    }
}
