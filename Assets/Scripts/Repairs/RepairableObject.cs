
using UnityEngine;
using System.Collections;

public class RepairableObject : MonoBehaviour, IInteractable
{
    public RepairableData data;
    private bool isRepaired = false;
    private PlayerInventory _playerInventory;
    private RepairTooltipUI _tooltipUI;

    public void Initialize(PlayerInventory inventory, RepairTooltipUI tooltipUI)
    {
        _playerInventory = inventory;
        _tooltipUI = tooltipUI;
    }

    public void Interact()
    {
        if (isRepaired || _playerInventory == null) return;

        if (_playerInventory.HasEnough(data))
        {            
            _playerInventory.SpendResources(data);                     
            _tooltipUI.Hide(); // Hide the tooltip immediately
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
