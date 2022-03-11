using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Burst;

[ExecuteInEditMode]
[BurstCompile]
public class ColorManager : MonoBehaviour
{
    GameManager manager;

    Camera cam;

    public GameObject platform;

    public Color platformColor, backgroundColor;
    public Color[] colorList;

    Renderer[] renderers;


    int level;
    int oldLevel;

    private float transition = 0;
    public float transitionSpeed;

    void Start()
    {
        // Get objects
        renderers = platform.GetComponentsInChildren<Renderer>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        cam = FindObjectOfType<Camera>();

        level = 0;
        oldLevel = 0;
    }

    void Update()
    {
        if (manager.points < 100)
        {
            // Check level (every 10 points)
            level = Mathf.FloorToInt(manager.points / 10);
            if (oldLevel != level) transition = 0; oldLevel = level;
            // If no more color set to last
            if (level * 2 < colorList.Length) ChangeColors();
        } else
        {
            // Check level (every 50 points)
            level = 8 + Mathf.FloorToInt(manager.points / 50);
            if (oldLevel != level) transition = 0; oldLevel = level;
            // If no more color set to last
            if (level * 2 < colorList.Length) ChangeColors();
        }
    }

    void ChangeColors()
    {
        Color newPlat = colorList[level * 2];
        Color newBack = colorList[(level * 2) + 1];

        Color oldPlat = level == 0 ? colorList[level * 2] : colorList[(level * 2) - 2];
        Color oldBack = level == 0 ? colorList[(level * 2) + 1] : colorList[(level * 2) - 1];

        Color nowPlat = Color.Lerp(oldPlat, newPlat, transition);
        Color nowBack = Color.Lerp(oldBack, newBack, transition);

        // Set color of platform and background
        if (Application.isPlaying)
        {
            foreach (Renderer rend in renderers)
            {
                if (rend.tag == "Platform")
                    rend.sharedMaterial.SetColor("_Color", nowPlat);
            }
            cam.backgroundColor = nowBack;
        }
        else
        {
            foreach (Renderer rend in renderers)
            {
                if (rend.tag == "Platform")
                    rend.sharedMaterial.SetColor("_Color", platformColor);
            }
            cam.backgroundColor = backgroundColor;
        }

        transition += transitionSpeed * Time.deltaTime;
    }
}
