using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCount : MonoBehaviour
{
    public static LoseCount instance;

    public int loseCount;
    public float timeSinceStart;
    public bool firstGame;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        firstGame = true;
        loseCount = 0;
    }

    private void Start()
    {
        // Check platform and set frame rate
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.targetFrameRate = 60;
        }
    }

    private void Update()
    {
        timeSinceStart += Time.deltaTime;
    }
}
