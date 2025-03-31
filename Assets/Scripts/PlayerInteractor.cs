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

    void Update()
    {
        //DetectInteractable();

        if (Input.GetKeyDown(KeyCode.Mouse0) && _targetTree != null)
        {
            _targetTree.ChopTree();
        }

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





    /*void DetectInteractable()
    {
        // Get the forward direction of the camera but flatten it (ignore Y component)
        Vector3 flatDirection = _cameraTransform.forward;
        flatDirection.y = 0; // Remove vertical tilt
        flatDirection.Normalize(); // Keep direction consistent

        Ray ray = new Ray(_playerTransform.position, flatDirection);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * _interactionRange, Color.red); // Draw the ray in Scene View

        if (Physics.Raycast(ray, out hit, _interactionRange, _interactableLayer))
        {
            Debug.Log("Hit: " + hit.collider.name); // Check what object is being hit
                                                    // Get the interactable component (ANY type)
            Component interactable = hit.collider.GetComponent<Tree>();

            switch (interactable)
            {
                case Tree tree:
                    _targetTree = tree;
                    _targetLog = null;
                    UIManager.Instance.ShowInteraction("Left click to Chop");
                    break;
                                    
                default:
                    ClearTargets();
                    break;
            }
        }
        else
        {
            ClearTargets();
        }
    }
    */
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

}
