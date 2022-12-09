using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine;
using YG;

public class Player : PlayerInput
{
    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private float playerJumpForce;
    [SerializeField] private float playerFireRate;
    [SerializeField] private float groundCheckRadius;
    [Space(10)]
    [SerializeField] private LayerMask whatIsGround;
    [Space(10)]
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform bulletLouncher;
    [Space(20)]
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip winSound;
    [Space(20)]
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Animator AN;
    [SerializeField] private CapsuleCollider2D CC;
    [SerializeField] private AudioSource AU;

    private float nextShoot;

    private bool isDeadOrWin = false;
    private bool isGrounded = false;
    private bool facingRight = true;

    public static UnityEvent PlayerIsDead = new UnityEvent();
    public static UnityEvent PlayerIsWin = new UnityEvent();

    private void Start()
    {
        AU.volume = YandexGame.savesData.soundsVolume;
        PlayerInput.JumpEvent.AddListener(PlayerJump);
    }

    private void Update()
    {
        if (!isDeadOrWin && fire)
        {
            PlayerShoot();
        }
    }

    private void FixedUpdate()
    {
        if (!isDeadOrWin)
        {
            GroundCheck();
            PlayerRun();
        }
    }

    private void GroundCheck()
    {
        isGrounded = false;
        AN.SetBool("Player Jump", true);
        Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);

        for (int i = 0; i < groundColliders.Length; i++)
        {
            if (groundColliders[i].gameObject.layer == 3)
            {
                isGrounded = true;
                AN.SetBool("Player Jump", false);
            }
        }
    }

    private void PlayerRun()
    {
        RB.velocity = new Vector3(move * playerMovementSpeed, RB.velocity.y);
        AN.SetFloat("Player Walk", Mathf.Abs(move));

        if (facingRight && move < 0)
        {
            PlayerFlip();
            BulletLouncherFlip(180);
        }
        else if (!facingRight && move > 0)
        {
            PlayerFlip();
            BulletLouncherFlip(0);
        }
    }

    private void PlayerJump()
    {
        if (isGrounded && Time.timeScale == 1 && !isDeadOrWin)
        {
            RB.AddForce(transform.up * playerJumpForce, ForceMode2D.Impulse);
            AN.SetBool("Player Jump", true);
            AU.PlayOneShot(jumpSound);
        }
    }

    private void PlayerFlip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void BulletLouncherFlip(float angleOfRotation)
    {
        Quaternion rotate = bulletLouncher.transform.rotation;
        rotate.y = angleOfRotation;
        bulletLouncher.transform.rotation = rotate;
    }

    private void PlayerShoot() 
    {
        if (Time.time > nextShoot)
        {
            nextShoot = Time.time + playerFireRate;
            AU.PlayOneShot(shootSound);
            GameObject bullet = Instantiate(playerBullet, bulletLouncher.position, bulletLouncher.transform.rotation);
        }
    }

    private void PlayerDead()
    {
        if (!isDeadOrWin)
        {
            isDeadOrWin = true;
            RB.constraints = RigidbodyConstraints2D.FreezePositionX;
            RB.AddForce(transform.up * playerJumpForce, ForceMode2D.Impulse);
            AN.SetBool("Player Jump", false);
            AN.SetBool("Player Dead", true);
            AU.PlayOneShot(deadSound);
            PlayerIsDead.Invoke();
        }
    }

    private void PlayerWin()
    {
        if (!isDeadOrWin)
        {
            isDeadOrWin = true;
            AU.PlayOneShot(winSound);
            AN.SetBool("Player Jump", false);
            AN.SetFloat("Player Walk", 0);
            PlayerIsWin.Invoke();

            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            if(sceneIndex >= YandexGame.savesData.completedLevels)
            {
                YandexGame.savesData.completedLevels = sceneIndex + 1;
                YandexGame.SaveProgress();
            }
            else if(sceneIndex == 20)
            {
                YandexGame.savesData.completedLevels = 20;
                YandexGame.SaveProgress();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDeadOrWin)
        {         
            switch (collision.gameObject.tag)
            {
                case "Dangerous":
                    PlayerDead();
                    break;
                case "Finish":
                    PlayerWin();
                    playerMovementSpeed = 0;
                    RB.bodyType = RigidbodyType2D.Static;
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
