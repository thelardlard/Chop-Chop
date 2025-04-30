using UnityEngine;

public class Tree : MonoBehaviour
{
    //public int _maxHits = 5; //How many chops to cut down tree
    //private int _currentHits = 0; //How many chops has the tree recieved
    
    public GameObject stumpPrefab;
    public GameObject cutTreePrefab;    
    
    
    /*public void ChopTree()
    {
        _currentHits++; //Increase chops recieved by 1

        THINGS TO DO
         * Change the way this works to some sort of rhythym game
         * Add visual effects and sound effects
         

        if (_currentHits >= _maxHits)
        {
            FallTreelo); //Cut down tree if current hits equals max hits
        }
    }
*/

    public void FallTree(int logsToSpawn)
    {
        UIManager.Instance.ClearInteraction(); //Hide the interaction UI
        //Vector3 spawnPosition = transform.position + new Vector3 (0,2,0); //Ensure the log doesn't spawn below ground
        //Instantiate(_logPrefab, spawnPosition , Quaternion.identity); //Spawn a log
        //Destroy(gameObject); //Destroy the tree

        // 1. Spawn stump at current position
        Instantiate(stumpPrefab, transform.position, Quaternion.identity);

        // 2. Spawn cut tree top
        GameObject top = Instantiate(cutTreePrefab, transform.position, Quaternion.identity);
        Rigidbody rb = top.GetComponent<Rigidbody>();
        // Ensure zero initial velocity
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Pass log count to the CutTreeTop script
        CutTreeTop cutTreeTop = top.GetComponent<CutTreeTop>();
        if (cutTreeTop != null)
        {
            cutTreeTop.SetLogCount(logsToSpawn);
        }

        // Optional: Add a very slight angular velocity to determine fall direction
        rb.angularVelocity = new Vector3(0.1f, 0, 0.1f); // Very small values
        //rb.AddForce(transform.forward * 2f + Vector3.up, ForceMode.Impulse);

        // 3. Destroy original tree
        Destroy(gameObject);
        
        /*THINGS TO DO
         * Change the destroyed tree to a stump and a cut tree prefab and allow the tree to fall. Upon collision with ground, explode into 3 log prefabs?
         * Add visual effects and sound effects
         */

    }
}
