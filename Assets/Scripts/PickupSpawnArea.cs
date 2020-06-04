using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawnArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
