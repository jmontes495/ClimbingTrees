using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawningManager : MonoBehaviour {

    public static TreeSpawningManager Instance
    {
        get { return instance; }
        set { }
    }
    
    private static TreeSpawningManager instance;

    [SerializeField]
    private float[] treeHeights;

    [SerializeField]
    private float playerSpawningHeight;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(this);
    }

    public float AdjustHeight(float height)
    {
        if (height >= 0 && height <= (treeHeights[1] + treeHeights[0]) / 2)
            return treeHeights[0];
        for (int i = 1; i < treeHeights.Length - 1; i++)
        {
            if (height >= (treeHeights[i] + treeHeights[i - 1]) / 2 && height <= (treeHeights[i + 1] + treeHeights[i]) / 2)
                return treeHeights[i];
        }

        return treeHeights[treeHeights.Length - 1];
    }

    public float GetPlayerHeight()
    {
        return playerSpawningHeight;
    }
}
