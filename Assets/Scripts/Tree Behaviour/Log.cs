using UnityEngine;

public class Log : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        var inventory = FindFirstObjectByType<PlayerInventory>();
        if (inventory == null) Debug.Log("No inventory found");
        if (inventory != null)
        {
            inventory.AddResource(ResourceType.Wood, 1);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        if (transform.position.y < -5f) Destroy(gameObject); // safety net
    }
}
