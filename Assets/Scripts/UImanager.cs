using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
using UnityStandardAssets.ImageEffects;
using Unity.Burst;

[BurstCompile]
public class UImanager : MonoBehaviour
{
    public static UImanager instance;

    [SerializeField] private GameObject startPanel, endPanel, pointsPanel, gpsPanel, main, leaderB, restartB;
    [SerializeField] private float fadeSpeed, blurOutSpeed;

    public Material blurMat;

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
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        startPanel = GameObject.FindGameObjectWithTag("StartPanel");
        endPanel = GameObject.FindGameObjectWithTag("EndPanel");
        pointsPanel = GameObject.FindGameObjectWithTag("PointsPanel");

        main = GameObject.FindGameObjectWithTag("Main");
        leaderB = GameObject.FindGameObjectWithTag("LeaderB");
        restartB = GameObject.FindGameObjectWithTag("RestartB");
    }


    IEnumerator FadeGroup(GameObject obj, bool IN, float delay = 0)
    {
        float transition = fadeSpeed * Time.deltaTime;

        if (IN)
        {
            obj.GetComponent<CanvasGroup>().alpha = 0f;
        } else
        {
            obj.GetComponent<CanvasGroup>().alpha = 1f;
        }

        if (delay != 0)
        {
            yield return new WaitForSeconds(delay);
        }

        if (IN)
        {
            for (float ft = 0f; ft <= 1.5f; ft += transition)
            {
                transition = fadeSpeed * Time.deltaTime;
                obj.GetComponent<CanvasGroup>().alpha = ft;
                yield return new WaitForSeconds(0);
            }
        } else
        {
            for (float ft = 1f; ft >= -0.5f; ft -= transition)
            {
                transition = fadeSpeed * Time.deltaTime;
                obj.GetComponent<CanvasGroup>().alpha = ft;
                yield return new WaitForSeconds(0);
            }
        }

        if (!IN)
        {
            obj.GetComponent<Canvas>().enabled = false;
        }
        yield break;
    }

    IEnumerator BlurStartOut()
    {
        StopCoroutine(BlurStartIn());
        float transition = blurOutSpeed * Time.deltaTime;

        for (float ft = 1f; ft >= 0f; ft -= transition)
        {
            transition = blurOutSpeed * Time.deltaTime;
            Camera.main.GetComponent<Blur>().blurSpread = ft;
            yield return new WaitForSeconds(0);
        }

        Camera.main.GetComponent<Blur>().blurSpread = 0f;
        yield return new WaitForSeconds(0);
        Camera.main.GetComponent<Blur>().iterations = 2;
        yield return new WaitForSeconds(0);
        Camera.main.GetComponent<Blur>().iterations = 1;
        yield return new WaitForSeconds(0);
        Camera.main.GetComponent<Blur>().enabled = false;
        yield break;
    }
    IEnumerator BlurStartIn()
    {
        StopCoroutine(BlurStartOut());

        Blur blur = Camera.main.GetComponent<Blur>();

        blur.enabled = true;
        yield return new WaitForSeconds(0);
        blur.iterations = 1;
        yield return new WaitForSeconds(0);
        blur.iterations = 2;
        yield return new WaitForSeconds(0);

        float transition = blurOutSpeed * Time.deltaTime;

        for (float ft = 0f; ft <= 1f; ft += transition)
        {
            transition = blurOutSpeed * Time.deltaTime;
            blur.blurSpread = ft;
            yield return new WaitForSeconds(0);
        }

        blur.blurSpread = 1f;
        yield break;
    }



    public void OpenEndPanel()
    {
        endPanel.GetComponent<Canvas>().enabled = true;

        StartCoroutine(FadeGroup(main, true, 0f));
        StartCoroutine(FadeGroup(leaderB, true, 0.15f));
        StartCoroutine(FadeGroup(restartB, true, 0.3f));
    }
    public void CloseEndPanel()
    {
        StartCoroutine(FadeGroup(endPanel, false));
    }


    public void OpenStartPanel()
    {
        startPanel.GetComponent<Canvas>().enabled = true;
        StartCoroutine(FadeGroup(startPanel, true));
    }
    public void CloseStartPanel()
    {
        StartCoroutine(FadeGroup(startPanel, false));
    }


    public void OpenPointsPanel()
    {
        pointsPanel.GetComponent<Canvas>().enabled = true;
        StartCoroutine(FadeGroup(pointsPanel, true));
    }
    public void ClosePointsPanel()
    {
        StartCoroutine(FadeGroup(pointsPanel, false));
    }


    public void CloseGPSpanel(bool success)
    {
        StartCoroutine(FadeGroup(gpsPanel, false));
        StartCoroutine(BlurStartOut());
    }
    public void BlurIn()
    {
        StartCoroutine(BlurStartIn());
    }
    public void BlurOut()
    {
        StartCoroutine(BlurStartOut());
    }


    public void createPopUp(string text)
    {

    }


    public void restart()
    {
        StopAllCoroutines();
    }
}
