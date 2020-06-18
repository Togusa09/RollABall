using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PickupWeights 
{
    public int Weight;
    public Pickup Prefab;
}

public class PickupSpawner : MonoBehaviour
{
    private PickupSpawnArea[] _pickupSpawnAreas;

    public float PickupSpawnInterval;
    public Pickup PickupPrefab;
    public int MaxPickups = 20;

    public List<PickupWeights> Pickups = new List<PickupWeights>();


    void Awake()
    {
        RescanSpawnAreas();
    }

    private void OnEnable()
    {
        RescanSpawnAreas();
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

                var type = GetRandomPickupType();

                GetOrCreatePickup(type, new Vector3(xSpawnPosition, ySpawnPosition, zSpawnPosition));
            }

            yield return new WaitForSeconds(PickupSpawnInterval);
        }
    }

    private PickupWeights GetRandomPickupType()
    {
        var totalWeights = Pickups.Sum(x => x.Weight);
        var weightedRandom = Random.Range(0, totalWeights);

        int currentSum = 0;
        foreach(var pickup in Pickups)
        {
            currentSum += pickup.Weight;

            if (currentSum > weightedRandom)
            {
                return pickup;
            }
        }

        return Pickups[0];
    }

    private Pickup GetOrCreatePickup(PickupWeights pickup, Vector3 position)
    {
        var existing = GetComponentsInChildren<Pickup>(true).FirstOrDefault(x => !x.isActiveAndEnabled && x.PickupType == pickup.Prefab.PickupType);
        if (existing)
        {
            existing.transform.position = position;
            existing.transform.rotation = Quaternion.identity;
            existing.gameObject.SetActive(true);

            return existing;
        }

        return Instantiate(pickup.Prefab, position, Quaternion.identity, transform);
    }

    public void RescanSpawnAreas()
    {
        _pickupSpawnAreas = FindObjectsOfType<PickupSpawnArea>();
    }
}
