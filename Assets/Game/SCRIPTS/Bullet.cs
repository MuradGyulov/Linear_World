using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [Space(16)]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private BoxCollider2D bc;
    [SerializeField] private ParticleSystem ps;

    private bool bulletCollision = false;

    private void OnEnable()
    {
        Destroy(gameObject, bulletLifeTime);
    }

    private void FixedUpdate()
    {
        if (!bulletCollision)
        {
            rb.velocity = transform.right * bulletSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bulletCollision = true;
        ps.Play();
        bulletSpeed = 0;
        sr.enabled = false;
        bc.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject, bulletLifeTime);
    }
}
