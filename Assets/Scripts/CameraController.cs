using System;
using UnityEngine;

namespace RollABall
{
    public class CameraController : MonoBehaviour
    {
        public GameObject player;
        private Vector3 offset;

        // Start is called before the first frame update
        void Start()
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            offset = transform.position - player.transform.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = player.transform.position + offset;
        }
    }
}