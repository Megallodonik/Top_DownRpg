using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BlackCube : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = new Vector3(2, 2, 2);
        StartCoroutine(Scaling());
    }
    private IEnumerator Scaling()
    {
        while (transform.localScale.x >= 1f)
        {
            transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
            yield return new WaitForSeconds(0.01f);
        }

        
    }
}
