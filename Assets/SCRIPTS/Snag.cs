using UnityEngine;

public class Snag : MonoBehaviour
{
    [SerializeField] private int snagHealthNumber;

    [SerializeField] private Animator _animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            _animator.SetBool("Snag Take Damage", true);
            Invoke("EndAnimation", 0.1f);
            snagHealthNumber--;

            if(snagHealthNumber <= 0)
            {
                Destroy(gameObject, 0.1f);
            }
        }
    }

    private void EndAnimation()
    {
        _animator.SetBool("Snag Take Damage", false);
    }
}
