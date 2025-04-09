using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public PlayerInventory playerInventory;
    [SerializeField] private RepairTooltipUI tooltipUI;    
    public Transform _cameraTransform;
    public Transform _playerTransform;
    private Tree _targetTree;
    private IInteractable _currentInteractable;
    public bool HasTargetTree() => _targetTree != null;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && _currentInteractable != null)
        {
            _currentInteractable.Interact();
            ClearTargets();
        }
    }

    private void OnTriggerStay(Collider other)
    {        
        if (other.CompareTag("Tree"))
        {
            //Debug.Log("Tree Trigger entered");
            _targetTree = other.GetComponent<Tree>(); // Get the Tree component of the object that entered                     
            UIManager.Instance.ShowInteraction("Left click to Chop"); //Show interaction UI
        }
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            _currentInteractable = interactable;

            if (interactable is Log)
            {
                UIManager.Instance.ShowInteraction("Press E to pick up");
            }            
        }

    }

    private void OnTriggerEnter(Collider other)
    {        
        if (other.TryGetComponent<IInteractable>(out var interactable))
        {
            _currentInteractable = interactable;                       
            if (interactable is RepairableObject repairable)
            {
                repairable.Initialize(playerInventory, tooltipUI); // Give it access to inventory & tooltip UI
                tooltipUI.Show(repairable);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        ClearTargets(); 
        var repairable = other.GetComponent<RepairableObject>();
        if (repairable != null)
        {
            tooltipUI.Hide(); //Turns repair UI prompt off
        }

    }

    void PickUp(Log _targetLog)
{
    playerInventory.AddResource(ResourceType.Wood, 1); //Adds 1 wood to player inventory
    Destroy(_targetLog.gameObject); //Destroys Log
        ClearTargets(); 
}
public void ClearTargets() //Ensures all targets are nulled when leaving a trigger zone
    {
        UIManager.Instance.ClearInteraction(); 
        _targetTree = null;
        _currentInteractable = null;
    }
public void ChopTree() //Runs the chop tree method contained in the target tree
    {
        _targetTree.ChopTree();
    }
}


