using System;
using UnityEngine;
using UnityEngine.UI;

namespace RollABall
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public float speed;
        public float growthRate;
        public Text countText;
        public Text remainingCountText;
        public Text winText;
        public Text levelText;

        private Rigidbody rigidBody;
        
        private int _maxScore = 1;
        private int _scoreIncrement = 1;
        
        public Action OnLevelChange;

        private int _level = 1;
        private int Level
        {
            get { return _level; }
            set
            {
                if (_level == value) return;

                _level = value;

                levelText.text = $"Level {_level}";
                remainingCountText.text = $"Next Level In: {_maxScore - _count}";

                OnLevelChange?.Invoke();
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

                countText.text = $"Count: {_count}";
                remainingCountText.text = $"Next Level In: {_maxScore - _count}";
                if (_count >= _maxScore)
                {
                    //winText.text = "You Win";
                    _maxScore += _scoreIncrement;
                    _scoreIncrement += _scoreIncrement;
                    Level++;
                }
            }
        }

        void Start()
        {
            if (countText == null) throw new ArgumentNullException(nameof(countText));
            if (winText == null) throw new ArgumentNullException(nameof(winText));

            rigidBody = GetComponent<Rigidbody>();
            Count = 0;
            winText.text = "";
            levelText.text = $"Level {_level}";
        }

        void FixedUpdate()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            rigidBody.AddForce(new Vector3(moveHorizontal, 0, moveVertical) * speed);
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