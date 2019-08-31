using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitIdle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GoUp());   
    }

    private IEnumerator GoUp()
    {
        for (float i = 0; i < 0.1f; i += 0.01f)
        {
            transform.Translate(0, (0.1f - i), 0);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(GoDown());
    }

    private IEnumerator GoDown()
    {
        for (float i = 0; i < 0.1f; i += 0.01f)
        {
            transform.Translate(0, -(0.1f - i), 0);
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(GoUp());
    }
}
