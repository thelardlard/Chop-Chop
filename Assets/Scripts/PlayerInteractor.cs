using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public float _interactionRange = 30f;
    public LayerMask _interactableLayer;
    public Transform _cameraTransform;
    public Transform _playerTransform;
    [SerializeField]
    private Tree _targetTree;
    [SerializeField]
    private Log _targetLog;
    public bool HasTargetTree() => _targetTree != null;

    void Update()
    {
             
        if (Input.GetKeyDown(KeyCode.E) && _targetLog != null)
        {
            PickUp(_targetLog);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name); // Debug to see which object entered the trigger

        if (other.CompareTag("Tree"))
        {
            Debug.Log("Tree Trigger entered");
            _targetTree = other.GetComponent<Tree>(); // Get the Tree component of the object that entered
            _targetLog = null;            
            UIManager.Instance.ShowInteraction("Left click to Chop");
        }
        if (other.CompareTag("Log"))
        {
            UIManager.Instance.ShowInteraction("Press E to pickup");
            _targetTree = null; // Get the Tree component of the object that entered
            _targetLog = other.GetComponent<Log>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        ClearTargets();
    }

    void PickUp(Log _targetLog)
{
    InventoryManager.Instance.AddLog(1);
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


