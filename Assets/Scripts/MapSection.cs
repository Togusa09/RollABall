using System;
using UnityEngine;

namespace RollABall
{
    public class MapSection : MonoBehaviour
    {
        public GameObject NorthWall;
        public GameObject SouthWall;
        public GameObject EastWall;
        public GameObject WestWall;

        [SerializeField]
        private GameObject _hatch;

        public Action OnPlayerExit;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenHatch()
        {
            _hatch.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            OnPlayerExit?.Invoke();
        }
    }
}
