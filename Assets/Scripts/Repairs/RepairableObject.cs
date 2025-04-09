
using UnityEngine;
using System.Collections;

public class RepairableObject : MonoBehaviour
{
    public RepairableData data;
    private bool isRepaired = false;

    public void TryRepair(PlayerInventory inventory)
    {
        if (isRepaired) return;

        if (inventory.HasEnough(data))
        {
            inventory.SpendResources(data);
            StartCoroutine(DoRepair());
        }
    }

    private IEnumerator DoRepair()
    {
        yield return new WaitForSeconds(data.repairTime);
        isRepaired = true;

        if (data.replacementPrefab != null)
        {
            Instantiate(data.replacementPrefab, transform.position, transform.rotation);
        }

        if (data.repairSound != null)
        {
            AudioSource.PlayClipAtPoint(data.repairSound, transform.position);
        }

        Destroy(gameObject);
    }
}
