using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        //rb.MovePosition(new Vector2(-5, -5));
        rb.velocity = new Vector2(0,-5);
    }
}
