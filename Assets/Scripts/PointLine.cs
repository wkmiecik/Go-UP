using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLine : MonoBehaviour
{
    GameManager manager;

    private void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.ScorePoint();
        Destroy(gameObject);
    }
}
