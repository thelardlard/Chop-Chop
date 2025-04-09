using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public PlayerInventory playerInventory;
    [SerializeField] private RepairTooltipUI tooltipUI;
    public LayerMask _interactableLayer; //Is this doing anything?
    public Transform _cameraTransform;
    public Transform _playerTransform;
    private Tree _targetTree;
    private Log _targetLog;
    public bool HasTargetTree() => _targetTree != null;

    void Update()
    {
             
        if (Input.GetKeyDown(KeyCode.E) && _targetLog != null)
        {
            PickUp(_targetLog); //Pick up a log if one is in range and player presses E
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTriggerEnter called with: " + other.gameObject.name); // Debug to see which object entered the trigger

        if (other.CompareTag("Tree"))
        {
            //Debug.Log("Tree Trigger entered");
            _targetTree = other.GetComponent<Tree>(); // Get the Tree component of the object that entered
            _targetLog = null; //Ensure no target log is set            
            UIManager.Instance.ShowInteraction("Left click to Chop"); //Show interaction UI
        }
        if (other.CompareTag("Log"))
        {
            UIManager.Instance.ShowInteraction("Press E to pickup");
            _targetTree = null; // Get the Tree component of the object that entered
            _targetLog = other.GetComponent<Log>();
        }
        if (other.CompareTag("Repairable"))
        {
            Debug.Log("Repairable Trigger Enterer");
            var repairable = other.GetComponent<RepairableObject>();
            if (repairable != null)
            {
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
            tooltipUI.Hide();
        }

    }

    void PickUp(Log _targetLog)
{
    playerInventory.AddResource(ResourceType.Wood, 1);
    Destroy(_targetLog.gameObject);
        ClearTargets();
}
public void ClearTargets()
    {
        UIManager.Instance.ClearInteraction();
        _targetTree = null;
        _targetLog = null;

    }
public void ChopTree()
    {
        _targetTree.ChopTree();
    }
}


