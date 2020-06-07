using System.Collections;
using System.Linq;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    private PickupSpawnArea[] _pickupSpawnAreas;

    public float PickupSpawnInterval;
    public Pickup PickupPrefab;
    public int MaxPickups = 20;


    void Awake()
    {
        _pickupSpawnAreas = GetComponentsInChildren<PickupSpawnArea>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPickup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator SpawnPickup()
    {
        while (enabled)
        {
            if (GetComponentsInChildren<Pickup>().Length < MaxPickups)
            {
                var spawnAreaIndex = Random.Range(0, _pickupSpawnAreas.Length);

                var spawnArea = _pickupSpawnAreas[spawnAreaIndex];
                var maxBound = spawnArea.transform.TransformPoint(new Vector3(0.5f, 0.5f, 0.5f));
                var minBound = spawnArea.transform.TransformPoint(new Vector3(-0.5f, -0.5f, -0.5f));

                var xSpawnPosition = Random.Range(minBound.x, maxBound.x);
                var ySpawnPosition = 0.5f; // Shift half unit up as pickup origin is in center
                var zSpawnPosition = Random.Range(minBound.z, maxBound.z);

                GetOrCreatePickup(new Vector3(xSpawnPosition, ySpawnPosition, zSpawnPosition));
            }

            yield return new WaitForSeconds(PickupSpawnInterval);
        }
    }

    private Pickup GetOrCreatePickup(Vector3 position)
    {
        var existing = GetComponentsInChildren<Pickup>(true).FirstOrDefault(x => !x.isActiveAndEnabled);
        if (existing)
        {
            existing.transform.position = position;
            existing.transform.rotation = Quaternion.identity;
            existing.gameObject.SetActive(true);

            return existing;
        }

        return Instantiate(PickupPrefab, position, Quaternion.identity, transform);
    }
}
