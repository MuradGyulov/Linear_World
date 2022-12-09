using UnityEngine;

public class Chicken : MonoBehaviour
{
    [SerializeField] private float chickenMovementSpeed;
    [SerializeField] private int chickenHealthNumber;
    [Space(20)]
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Animator AN;


    private bool facingLeft = true;

    private void FixedUpdate()
    {
        ChickenMove();
    }

    private void ChickenMove()
    {
        RB.velocity = transform.right * chickenMovementSpeed;

        if (facingLeft && chickenMovementSpeed < 0)
        {
            ChickenFlip();
        }
        else if (!facingLeft && chickenMovementSpeed > 0)
        {
            ChickenFlip();
        }
    }

    private void ChickenFlip()
    {
        facingLeft = !facingLeft;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        chickenMovementSpeed *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3) { ChickenFlip();}

        switch (collision.gameObject.tag)
        {
            case "Dangerous":
                ChickenFlip();
                break;
            case "Bullet":
                chickenHealthNumber--;
                AN.SetBool("Take Damage", true);
                Invoke("DefaultAnimation", 0.1f);
                if(chickenHealthNumber <= 0) { Destroy(gameObject, 0.1f); }
                break;
        }
    }

    private void DefaultAnimation()
    {
        AN.SetBool("Take Damage", false);
    }
}
