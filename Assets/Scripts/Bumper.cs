using UnityEngine;

namespace RollABall
{
    public class Bumper : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnTriggerEnter(Collider collider)
        {
            Debug.Log("Bumper activated");

            var animator = GetComponentInChildren<Animator>();
            animator.Play("Bumper");
        }
    }
}
