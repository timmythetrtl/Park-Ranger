using UnityEngine;

public class ActivateObjectOnTrigger : MonoBehaviour
{
    public GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the object to activate is not null
            if (objectToActivate != null)
            {
                // Activate the specified object
                objectToActivate.SetActive(true);
            }
        }
    }
}
