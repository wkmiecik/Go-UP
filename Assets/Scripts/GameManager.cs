using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms;
using Unity.Burst;

[BurstCompile]
public class GameManager : MonoBehaviour
{
    [SerializeField] float gapSize;

    [SerializeField] GameObject blockPrefab, pointLinePrefab;

    [SerializeField] TextMeshProUGUI pointsText, scoreText, bestText, debugText;

    float blockSize = 10;

    [HideInInspector] public int points = 0;
    int bestScore;

    int blocksSpawned = 0;

    public bool loseFlag = false;

    public bool playing = false;

    public GameObject player;

    void Start()
    {
        // Load best score
        bestScore = PlayerPrefs.GetInt("Best", 0);

        // Show best score on start
        pointsText.text = "Best\n" + bestScore.ToString();

        // Fade start screen in
        UImanager.instance.OpenStartPanel();
        UImanager.instance.OpenPointsPanel();

        if (!LoseCount.instance.firstGame)
        {
            UImanager.instance.BlurOut();
        }
    }

   public void StartPlaying()
    {
        if (!playing)
        {
            playing = true;
            player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

            UImanager.instance.CloseStartPanel();

            pointsText.text = "0";

            // Start spawning
            InvokeRepeating("SpawnBlocks", 0f, 1.3f);
        }
    }

    private void Update()
    {
        if (loseFlag) Lose();

        // Screenshots
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    ScreenshotHandler.TakeScreenshot_Static(1024, 500);
        //}
    }

    void SpawnBlocks()
    {
        float offset = 0;

        if (blocksSpawned < 5)
            offset = (5 - blocksSpawned) / 3;

        float leftPos = Random.Range(-9.5f, -0.5f - (gapSize+offset));

        Instantiate(blockPrefab, new Vector2(leftPos, 11), new Quaternion());
        Instantiate(pointLinePrefab, new Vector2(0, 11), new Quaternion());
        Instantiate(blockPrefab, new Vector2(leftPos+gapSize + offset + 10f, 11), new Quaternion());

        blocksSpawned++;
    }

    public void Lose()
    {
        UImanager.instance.BlurIn();
        LoseCount.instance.firstGame = false;

        loseFlag = false;

        PlayerPrefs.SetInt("Best", bestScore);

        LoseCount.instance.loseCount++;

        //pointsText.text = "";
        UImanager.instance.ClosePointsPanel();

        scoreText.text = points.ToString();
        bestText.text = points > bestScore ? points.ToString() : bestScore.ToString();

        PlayServices.instance.postToLeaderboard(points);

        if (LoseCount.instance.loseCount > 10 && LoseCount.instance.timeSinceStart > 80)
        {
            LoseCount.instance.loseCount = 0;
            LoseCount.instance.timeSinceStart = 0;
            PlayAd();
        } else
        {
            UImanager.instance.OpenEndPanel();
        }
    }

    public void Restart()
    {
        UImanager.instance.restart();
        SceneManager.LoadScene("Game");
    }

    public void ScorePoint()
    {
        points += 1;
        if (points > 99999) points = 99999;

        pointsText.text = points.ToString();

        if (points > bestScore) bestScore = points;
    }


    // Show FPS
    //void OnGUI()
    //{
    //    int w = Screen.width, h = Screen.height;

    //    GUIStyle style = new GUIStyle();

    //    Rect rect = new Rect(0, 0, w, h * 2 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h * 2 / 100;
    //    style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
    //    float msec = Time.deltaTime * 1000.0f;
    //    float fps = 1.0f / Time.deltaTime;
    //    string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
    //    GUI.Label(rect, text, style);
    //}

    public void PlayAd()
    {
        AdsManager.instance.ShowRegularAd(OnAdClosed);
    }

    private void OnAdClosed(ShowResult result)
    {
        //endCanvas.GetComponent<Canvas>().enabled = true;
        UImanager.instance.OpenEndPanel();
    }
}
