using UnityEngine;

public class Tree : MonoBehaviour
{
    //public int _maxHits = 5; //How many chops to cut down tree
    //private int _currentHits = 0; //How many chops has the tree recieved
    
    public GameObject stumpPrefab;
    public GameObject cutTreePrefab;

    public ParticleSystem smokeRing;
    public ParticleSystem smokeLength;

    public void PlaySmokeRing(float scale)
    {
        if (smokeRing != null)
        {
            smokeRing.transform.localScale = Vector3.one * scale;
            smokeRing.Play();
        }
    }

    public void PlaySmokeLength()
    {
        if (smokeLength != null)
        {
            smokeLength.transform.parent = null; // Detach from tree
            smokeLength.Play();
        }
    }
    

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
        
        

    }
}
