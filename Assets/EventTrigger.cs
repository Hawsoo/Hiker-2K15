using UnityEngine;
using System.Collections;

public class EventTrigger : MonoBehaviour
{
    public static bool allowLoggingOfEventTriggers = true;

    public MonoBehaviour triggerScript;
    public string targetTag;

    // When trigger is activated
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == targetTag)
        {
            if (allowLoggingOfEventTriggers)
            {
                Debug.Log("Trigger fired on gameobject \"" + gameObject.name + "\"\n" +
                    "\t::Fired function \"OnEventTrigger\"::");
            }

            // Send trigger message
            triggerScript.SendMessage("OnEventTrigger");
        }
    }
}
