using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Burst;

[BurstCompile]
public class Player : MonoBehaviour
{
    [SerializeField]
    GameManager manager; 

    Rigidbody2D rb;

    [SerializeField]
    Vector2 jumpForceLeft, jumpForceRight;

    private Animator anim;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if(SplashScreen.isFinished && PlayServices.instance.finishedAuth)
        {
            if (manager.playing)
            {
                // Stop idle animation
                anim.SetBool("IdleLoop", false);

                // Touch inputs
                if (Input.touchCount > 0)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            if (touch.position.x > Screen.width / 2)
                            {
                                JumpRight();
                            }
                            if (touch.position.x < Screen.width / 2)
                            {
                                JumpLeft();
                            }
                        }
                    }
                }

                // Keyboard inputs
                if (Input.GetKeyDown(KeyCode.A))
                {
                    JumpLeft();
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    JumpRight();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    manager.ScorePoint(); manager.ScorePoint(); manager.ScorePoint(); manager.ScorePoint(); manager.ScorePoint(); manager.ScorePoint(); manager.ScorePoint(); manager.ScorePoint(); manager.ScorePoint(); manager.ScorePoint();
                    }

            } else
            {
                // Touch inputs
                if (Input.touchCount > 0)
                {
                    foreach (Touch touch in Input.touches)
                    {
                        if (touch.phase == TouchPhase.Began)
                        {
                            if (touch.position.x > Screen.width / 2)
                            {
                                manager.StartPlaying();
                                JumpRight();
                            }
                            if (touch.position.x < Screen.width / 2)
                            {
                                manager.StartPlaying();
                                JumpLeft();
                            } 
                        }
                    }
                }

                // Keyboard inputs
                if (Input.GetKeyDown(KeyCode.A))
                {
                    manager.StartPlaying(); JumpLeft();
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    manager.StartPlaying(); JumpRight();
                }
            }
        }
    }

    void JumpLeft()
    {
        rb.AddForce(jumpForceLeft);
        anim.SetTrigger("FlyL");
    }
    void JumpRight()
    {
        rb.AddForce(jumpForceRight);
        anim.SetTrigger("FlyR");
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        manager.loseFlag = true;
        Destroy(gameObject);
    }
}
