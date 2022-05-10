using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] [Range(0f, 200f)] float Movementf_Speed;
    [SerializeField] [Range(1, 10)] int Player_Health;

    [SerializeField] Animator animatior;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] BoxCollider2D boxCollider;

    private bool fachingRight;
    


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                animatior.SetBool("Chicken Take Damage", true);
                Invoke("ChickenTakeDamage", 0.04f);
                break;

            case "Gost":
                PlayerFlip();
                break;
            case "Chicken":
                PlayerFlip();
                break;
            case "Finish":
                PlayerFlip();
                break;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            PlayerFlip();
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Box"))
        {
            PlayerFlip();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Finish"))
        {
            PlayerFlip();
        }
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector2(Movementf_Speed * Time.fixedDeltaTime, rigidBody.velocity.y);
    }

    private void PlayerFlip()
    {
        fachingRight = !fachingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        Movementf_Speed *= -1;
        transform.localScale = theScale;
    }

    private void ChickenTakeDamage()
    {
        Player_Health--;
        animatior.SetBool("Chicken Take Damage", false);
        if (Player_Health <= 0)
        {
            Destroy(this.gameObject, 0.04f);
        }
    }
}
