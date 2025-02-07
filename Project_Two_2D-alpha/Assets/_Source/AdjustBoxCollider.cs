
using UnityEngine;
using UnityEngine.VFX;

public class AdjustBoxCollider : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Vector3 size = boxCollider.size;
        size += Vector3.forward;
        GetComponent<VisualEffect>().SetVector3("Size", size); 
        GetComponent<VisualEffect>().SetVector3("Center", boxCollider.offset); 
    }
}