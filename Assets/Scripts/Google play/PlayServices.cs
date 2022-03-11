using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayServices : MonoBehaviour
{
    public static PlayServices instance;

    public TextMeshProUGUI debugText;

    public GameObject gpsPanel;

    public bool debug;

    public bool finishedAuth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            gpsPanel.SetActive(true);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(this.gameObject);
        finishedAuth = false;
    }


    private void Start()
    {
        GooglePlayGames.BasicApi.
        PlayGamesClientConfiguration.Builder config = new PlayGamesClientConfiguration.Builder();
        PlayGamesPlatform.InitializeInstance(config.Build());
        //PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success, string err) =>
        {
            UImanager.instance.CloseGPSpanel(success);

            if (success)
            {
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.TOP);
                if (debug) debugText.text = "login success";
                finishedAuth = true;
            }
            else
            {
                if (debug) debugText.text = "login failed: " + err;
                finishedAuth = true;
            }
        });
    }

    public void showLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI_YerluMPEAIQAQ");
        if (debug) debugText.text = "opening leaderboard";
    }

    public void postToLeaderboard(int newScore)
    {
        Social.Active.ReportScore(newScore, "CgkI_YerluMPEAIQAQ", (bool success) =>
        {
            if (success)
            {
                if (debug) debugText.text = "post success";
            }
            else
            {
                if (debug) debugText.text = "post failed";
            }
        });
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (debug) debugText = GameObject.FindGameObjectWithTag("DebugText").GetComponent<TextMeshProUGUI>();
    }
}
