using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Player characteristics")]
    [SerializeField] [Range(0f, 10f)] float Walking_Speed;
    [SerializeField] [Range(5f, 50f)] float Jump_Force;
    [SerializeField] [Range(10, 300)] int Number_Of_Bullets;
    [SerializeField] [Range(0f, 2f)] float Fire_Rate;
    [SerializeField] LayerMask What_is_Ground;

    private enum State { playerIsLife, playerIsDead, playerIsWin } State state = State.playerIsLife;

    private float horizontalInput;
    private float groundChekerRadius = 0.1f;
    private float nextShoot;
    private bool playerGrounded = false;
    private bool facingRight = false;
    private bool gameIsPaused = false;
    private float louncherRotationAngle;

    [SerializeField] private Transform Ground_Cheker;
    [SerializeField] private Transform Bullet_Louncher;
    [SerializeField] private GameObject Bullet;

    [SerializeField] private AudioClip Jump_Sound;
    [SerializeField] private AudioClip Shoot_Sound;
    [SerializeField] private AudioClip Dead_Sound;
    [SerializeField] private AudioClip Win_Sound;

    private SpriteRenderer SP;
    private Rigidbody2D RB;
    private AudioSource AU;
    private Animator AN;

    [HideInInspector] public static UnityEvent<int> BulletCounter = new UnityEvent<int>();
    [HideInInspector] public static UnityEvent PlayerIsDead = new UnityEvent();

    private void Start()
    {
        BulletCounter.Invoke(Number_Of_Bullets);
        Game_Canvas.PauseGame.AddListener(GameIsPaused);
        Game_Canvas.PlayGame.AddListener(PlayGame);

        SP = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        AU = GetComponent<AudioSource>();
        AN = GetComponent<Animator>();

        AU.volume = PlayerPrefs.GetFloat("My Sounds Volume");
    }

    private void Update()
    {
        PlayerGrounded();
        PlayerFlip();
        PlayerJump();

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    private void FixedUpdate()
    {
        PlayerWalk();
        PlayerShoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AN.SetBool("Player Jump", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Spike":
            PlayerDead();
                break;
            case "Gost":
                PlayerDead();
                break;
            case "Finish":
                PlayerWin();
                break;
            case "Chicken":
                PlayerDead();
                break;
            case "Snag":
                PlayerDead();
                break;
            case "Fly":
                PlayerDead();
                break;
            case "Snake":
                PlayerDead();
                break;
        }
    }

    private void GameIsPaused()
    {
        gameIsPaused = true;
    }
    private void PlayGame()
    {
        gameIsPaused = false;
    }

    private void PlayerGrounded()
    {
        playerGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Ground_Cheker.position, groundChekerRadius, What_is_Ground);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                playerGrounded = true;
            }
        }
    }

    private void PlayerFlip()
    {
        if(state == State.playerIsLife)
        {
            if (horizontalInput < 0 && !facingRight)
            {
                louncherRotationAngle = 180f;
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            else if(horizontalInput > 0 && facingRight)
            {
                louncherRotationAngle = 0f;
                facingRight = !facingRight;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
        }
    }

    private void PlayerWalk()
    {
        if(state == State.playerIsLife)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal") * Walking_Speed;
            RB.velocity = new Vector2(horizontalInput, RB.velocity.y);
            AN.SetFloat("Player Walk", Math.Abs(horizontalInput));
        }
    }

    private void PlayerJump()
    {
        if(Input.GetKeyDown(KeyCode.W) && state == State.playerIsLife && playerGrounded && !gameIsPaused)
        {
            RB.AddForce(transform.up * Jump_Force, ForceMode2D.Impulse);
            playerGrounded = false;
            AU.PlayOneShot(Jump_Sound);
            AN.SetBool("Player Jump", true);
        }
    }

    private void PlayerShoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (state == State.playerIsLife && Time.time > nextShoot && Number_Of_Bullets > 0)
            {
                nextShoot = Time.time + Fire_Rate;
                AU.PlayOneShot(Shoot_Sound);
                Instantiate(Bullet, Bullet_Louncher.transform.position, Quaternion.Euler(new Vector3(0, 0, louncherRotationAngle)));
                Number_Of_Bullets--;
                BulletCounter.Invoke(Number_Of_Bullets);
            }
        }
    }

    private void PlayerDead()
    {
        if(state == State.playerIsLife)
        {
            RB.constraints = RigidbodyConstraints2D.FreezePositionX;
            state = State.playerIsDead;
            Walking_Speed = 0;
            RB.AddForce(transform.up * 10, ForceMode2D.Impulse);
            PlayerIsDead.Invoke();
            AU.PlayOneShot(Dead_Sound);
            AN.SetBool("Player Dead", true);
        }
    }

    private void PlayerWin()
    {
        state = State.playerIsWin;
        Walking_Speed = 0;
        AN.SetFloat("Player Walk", 0);
        AU.PlayOneShot(Win_Sound);
        RB.constraints = RigidbodyConstraints2D.FreezePositionX;
    }
}
