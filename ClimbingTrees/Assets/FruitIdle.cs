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
        for (float i = 0; i < 0.3f; i += 0.03f)
        {
            transform.Translate(0, (0.3f - i), 0);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(GoDown());
    }

    private IEnumerator GoDown()
    {
        for (float i = 0; i < 0.3f; i += 0.03f)
        {
            transform.Translate(0, -(0.3f - i), 0);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(GoUp());
    }
}
