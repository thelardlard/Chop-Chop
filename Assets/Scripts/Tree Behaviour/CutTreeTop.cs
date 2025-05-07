using UnityEngine;
public class CutTreeTop : MonoBehaviour
{
    public GameObject logPrefab;
    public GameObject particlePrefab;
    public Collider topCollider; // Reference to the specific top collider
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float explosionDelay = 0.3f;
    [SerializeField] private float minImpactVelocity = 2f;
    private bool _hasExploded = false;
    private float _timeSinceSpawn = 0f;
    private int _logsToSpawn = 3;

    public void SetLogCount(int count)
    {
        _logsToSpawn = Mathf.Max(1, count); // Ensure at least 1 log
    }

    private void Update()
    {
        _timeSinceSpawn += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasExploded || _timeSinceSpawn < explosionDelay)
            return;

        // Check if the collision involves our top collider
        ContactPoint[] contacts = collision.contacts;
        bool topColliderInvolved = false;

        foreach (ContactPoint contact in contacts)
        {
            if (contact.thisCollider == topCollider)
            {
                topColliderInvolved = true;
                break;
            }
        }

        if (!topColliderInvolved)
            return;

        // Only explode if we hit the correct layer
        if (((1 << collision.gameObject.layer) & groundLayer) == 0)
            return;

        // Optional: only explode on "meaningful" impact
        if (collision.relativeVelocity.magnitude < minImpactVelocity)
            return;

        Explode();
    }

    private void Explode()
    {
        _hasExploded = true;
        Vector3 origin = transform.position;

        for (int i = 0; i < _logsToSpawn; i++)
        {
            Vector3 offset = transform.forward * i * 2f;
            Vector3 spawnPosition = origin + offset;

            // Raycast downwards to ensure the log spawns above the ground
            RaycastHit hit;
            if (Physics.Raycast(spawnPosition + Vector3.up * 5f, Vector3.down, out hit, 10f))
            {
                spawnPosition.y = hit.point.y + 0.5f; // 0.5f offset to make sure it spawns just above the ground
            }
            Instantiate(logPrefab, spawnPosition, Quaternion.identity);
        }
        var particleSystem = GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.transform.parent = null; // Detach from tree
            particleSystem.Play();
            Destroy(particleSystem.gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
        }

        Destroy(gameObject); // Immediately destroy the tree        
    }
}