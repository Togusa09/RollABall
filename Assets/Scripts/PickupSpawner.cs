using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    private PickupSpawnArea[] _pickupSpawnAreas;

    public float PickupSpawnInterval;
    public Pickup PickupPrefab;

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
            var spawnArea = _pickupSpawnAreas[0]; // Only have one at the moment
            var maxBound = spawnArea.transform.TransformPoint(new Vector3(0.5f, 0.5f, 0.5f));
            var minBound = spawnArea.transform.TransformPoint(new Vector3(-0.5f, -0.5f, -0.5f));

            var xSpawnPosition = Random.Range(minBound.x, maxBound.x);
            var ySpawnPosition = 0.5f; // Shift half unit up as pickup origin is in center
            var zSpawnPosition = Random.Range(minBound.z, maxBound.z);

            var newPickup = Instantiate(PickupPrefab, new Vector3(xSpawnPosition, ySpawnPosition, zSpawnPosition), Quaternion.identity, transform);

            yield return new WaitForSeconds(PickupSpawnInterval);
        }
    }
}
