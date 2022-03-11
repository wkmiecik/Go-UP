using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = this.gameObject.GetComponent<Button>();

        button.onClick.AddListener(open);
    }

    void open()
    {
        PlayServices.instance.showLeaderboard();
    }
}
