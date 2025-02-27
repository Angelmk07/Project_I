using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField] private float force;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<InputListner>().enabled = false;
            collision.GetComponent<Rigidbody2D>().gravityScale = 0;
            collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(Input.GetAxis("Horizontal"), 0) * force, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<InputListner>().enabled = true;
            collision.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}