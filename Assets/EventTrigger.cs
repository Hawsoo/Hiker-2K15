using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour
{
    public bool allowLogging = true;
    //public bool sendUpwards;
    public bool onEnterOnly;
    public bool destroyWhenActivated;

    public string targetTag;

    public MonoBehaviour triggerScript;
    public string triggerMessage = "OnEventTrigger";

    // When trigger is activated (for onEnterOnly == true)
    void OnTriggerEnter(Collider other)
    {
        // Check out
        if (!onEnterOnly) return;

        if (other.gameObject.tag == targetTag)
        {
            if (allowLogging)
            {
                Debug.Log("Trigger fired on gameobject \"" + gameObject.name + "\"\n" +
                    "\t::Fired function \"" + triggerMessage + "\"::");
            }

            // Send trigger message
            /*if (sendUpwards)
            {
                triggerScript.SendMessageUpwards(triggerMessage, other);
            }
            else*/
            {
                triggerScript.SendMessage(triggerMessage, other);
            }

            // Destroy if wanted
            if (destroyWhenActivated) GameObject.Destroy(gameObject);
        }
    }

    // When trigger is activated (for onEnterOnly == false)
    void OnTriggerStay(Collider other)
    {
        // Check out
        if (onEnterOnly) return;

        if (other.gameObject.tag == targetTag)
        {
            if (allowLogging)
            {
                Debug.Log("Trigger fired on gameobject \"" + gameObject.name + "\"\n" +
                    "\t::Fired function \"" + triggerMessage + "\"::");
            }

            // Send trigger message
            /*if (sendUpwards)
            {
                triggerScript.SendMessageUpwards(triggerMessage, other);
            }
            else*/
            {
                triggerScript.SendMessage(triggerMessage, other);
            }

            // Destroy if wanted
            if (destroyWhenActivated) GameObject.Destroy(gameObject);
        }
    }
}
