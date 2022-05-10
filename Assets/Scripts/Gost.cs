using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gost : MonoBehaviour
{
    [SerializeField] [Range(0f, 50f)] float Movementf_Speed;
    [SerializeField] [Range(1, 10)] int Player_Health;

    [SerializeField] LayerMask GroundLayer;

    [SerializeField] Animator animatior;
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] Transform GroundChek;

    private bool fachingRight;
    private bool playerGrounded;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                animatior.SetBool("Gost Take Damage", true);
                Invoke("GostTakeDamage", 0.04f);
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
        playerGrounded = Physics2D.OverlapCircle(GroundChek.transform.position, 0.1f, GroundLayer);
        if (!playerGrounded) { PlayerFlip(); }

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

    private void GostTakeDamage()
    {
        animatior.SetBool("Gost Take Damage", false);
        Player_Health--;
        if (Player_Health <= 0)
        {
            Destroy(this.gameObject, 0.04f);
        }
    }
}
