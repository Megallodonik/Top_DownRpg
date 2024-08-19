using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TrentProtectorBoss;

public class TrentProtectorBoss : MonoBehaviour
{
    [SerializeField] Transform Player;
    [SerializeField] GameObject Tree;
    //[SerializeField] float radius = 15;
    [SerializeField] GameObject LaserCircle;
    [SerializeField] float radiusAroundPlayer = 3f;
    [SerializeField] float speedAroundPlayer = 15f;
    float positionX, positionY, angle = 0f;
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
        LaserCircle.SetActive(true);
        StartCoroutine(LaserCircleRotation());
    }

    private void AroundPlayerAttack()
    {
        StartCoroutine(AroundPlayerAttackCor()); 
    }

    IEnumerator AroundPlayerAttackCor()
    {
        // босс вращается вокруг игрока
        for (int i = 0; i < 360; i++)
        {
            Transform center = Player.transform;

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

        StartCoroutine(ChooseAttack());
    }


    IEnumerator LaserCircleRotation()
    {
        // вокруг босса вращается спрайт с лазером
        for (int i = 0; i < 360; i++)
        {
            Debug.Log("laserCircleRot coroutine");
            LaserCircle.transform.rotation *= Quaternion.Euler(0f, 0f, 10f);
            yield return new WaitForSeconds(0.1f);
        }
        LaserCircle.SetActive(false);
        StartCoroutine(ChooseAttack());
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
                yield return new WaitForSeconds(3); 
                break;
            case Attacks.SquareAttack:
                Debug.Log("SqareAttack");
                yield return new WaitForSeconds(3);
                StartCoroutine(ChooseAttack());
                break;
            case Attacks.AroundPlayerAttack:
                Debug.Log("AroundPlayerAttack");
                AroundPlayerAttack();
                yield return new WaitForSeconds(10); 
                break;
        }
        
   
    }
}
