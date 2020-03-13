using UnityEngine;
using System.Collections;

public class ForwardEvent : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            string eventName = "TriggerEntered_" + this.name;
            EventManager.TriggerEvent(eventName, other.name);
        }
    }

}
