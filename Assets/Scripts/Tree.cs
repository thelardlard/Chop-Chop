using UnityEngine;

public class Tree : MonoBehaviour
{
    public int _maxHits = 5; //How many chops to cut down tree
    private int _currentHits = 0; //How many chops has the tree recieved
    public GameObject _logPrefab; 

    public void ChopTree()
    {
        _currentHits++; //Increase chops recieved by 1

        /*THINGS TO DO
         * Change the way this works to some sort of rhythym game
         * Add visual effects and sound effects
         */

        if (_currentHits >= _maxHits)
        {
            FallTree(); //Cut down tree if current hits equals max hits
        }
    }

    void FallTree()
    {
        UIManager.Instance.ClearInteraction(); //Hide the interaction UI
        Vector3 spawnPosition = transform.position + new Vector3 (0,2,0); //Ensure the log doesn't spawn below ground
        Instantiate(_logPrefab, spawnPosition , Quaternion.identity); //Spawn a log
        Destroy(gameObject); //Destroy the tree

        /*THINGS TO DO
         * Change the destroyed tree to a stump and a cut tree prefab and allow the tree to fall. Upon collision with ground, explode into 3 log prefabs?
         * Add visual effects and sound effects
         */

    }
}
