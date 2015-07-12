using UnityEngine;
using System.Collections;

public class PlayerCollisionDetect : MonoBehaviour
{
    public bool topField;
    public bool sideField;

    public bool disableLeft;
    public bool disableRight;

    // If there is collision
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            // Send Message(s)
            if (sideField) SendMessageUpwards("HitSide");
            if (topField) SendMessageUpwards("HitTop");
        }
    }

    // If collision stays
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            // Send Message(s)
            if (disableLeft) SendMessageUpwards("DisableLeft");
            if (disableRight) SendMessageUpwards("DisableRight");
        }
    }
}
