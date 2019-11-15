using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetalsAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float frameDelay = 0.03f;

    private Image image;
    private bool isRunning;

    public bool IsRunning
    {
        get { return isRunning; }
    }
    
    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(1, 1, 1, 0);
    }

    public void BeginPetals()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        image.color = new Color(1, 1, 1, 1);
        isRunning = true;
        foreach(Sprite sprite in sprites)
        {
            image.sprite = sprite;
            yield return new WaitForSeconds(frameDelay);
        }
        isRunning = false;
        image.color = new Color(1, 1, 1, 0);
    }
}
