using UnityEngine;

public class Steam : MonoBehaviour
{
    [SerializeField] private float force;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>() != null)
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.GetComponent<Rigidbody2D>().velocity.x, 0);
            collision.GetComponent<Rigidbody2D>().AddForce(collision.transform.up * force, ForceMode2D.Impulse);
        }
    }
}