using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RollABall
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float growthRate;
         private Rigidbody rigidBody;
        
        private int _maxScore = 1;
        private int _scoreIncrement = 1;
        
        public Action OnLevelChange;
        public Action OnLevelComplete;

        [SerializeField]
        private PickupSpawner _pickupSpawner;
        [SerializeField]
        private UIController _uIController;

        private int _level = 1;
        private int Level
        {
            get { return _level; }
            set
            {
                if (_level == value) return;

                _level = value;

                _uIController.LevelText = $"Level {_level}";
                _uIController.RemainingCountText = $"Next Level In: {_maxScore - _count}";
            }
        }

        private int _count;
        private int Count 
        { 
            get 
            { 
                return _count; 
            }
            set
            {
                _count = value;

                _uIController.CountText = $"Count: {_count}";
                _uIController.RemainingCountText = $"Next Level In: {_maxScore - _count}";
                if (_count >= _maxScore)
                {
                    _maxScore += _scoreIncrement;
                    _scoreIncrement += _scoreIncrement;
                    Level++;
                    _uIController.WinText = "Level complete - Move to exit";
                    OnLevelComplete?.Invoke();
                    _pickupSpawner.ClearPickups();
                }
            }
        }

        void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            Count = 0;
            _uIController.WinText = "";
            _uIController.LevelText = $"Level {_level}";
        }

        void FixedUpdate()
        {
            rigidBody.AddForce(new Vector3(_movementForce.x, 0, _movementForce.y) * speed);
        }

        private Vector2 _movementForce;

        void OnMove(InputValue value)
        {
            _movementForce = value.Get<Vector2>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Pick Up"))
            {
                other.gameObject.SetActive(false);
                var pickup = other.gameObject.GetComponent<Pickup>();

                switch (pickup.PickupType)
                {
                    case PickupType.Point:
                        {
                            Count = Count + 1;
                            var audioSource = GetComponent<AudioSource>();
                            audioSource.PlayOneShot(audioSource.clip, 0.5f);
                            break;
                        }

                    case PickupType.Grow:
                        {
                            transform.localScale += (Vector3.one * 0.1f);
                            break;
                        }
                }
            }
        }
    }
}