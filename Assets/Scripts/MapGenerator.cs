﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RollABall
{
    public class MapGenerator : MonoBehaviour
    {
        public MapSection MapeSectionPrefab;
        public PickupSpawner PickupSpawner;
        public PlayerController PlayerController;

        private List<MapSection> _mapSections = new List<MapSection>();
        private MapSection _exitSection;

        [SerializeField]
        private UIController _uIController;

        public int MapSize = 10;

        void Start()
        {
            GenerateMap();
            PlayerController.OnLevelComplete += LevelComplete;
        }

        private void LevelComplete()
        {
            //GenerateMap();
            PickupSpawner.enabled = false;
            _exitSection.OnPlayerExit += OnPlayerExit;
            _exitSection.OpenHatch();
        }
           
        private void OnPlayerExit()
        {
            _exitSection.OnPlayerExit -= OnPlayerExit;
            _exitSection = null;
            GenerateMap();
        }

        public void GenerateMap()
        {
            PickupSpawner.enabled = false;
            PickupSpawner.ClearPickups();

            PlayerController.gameObject.SetActive(false);
            _uIController.WinText = "";

            foreach (var mapSection in _mapSections.ToArray())
            {
                mapSection.gameObject.SetActive(false);
                _mapSections.Remove(mapSection);
                Destroy(mapSection.gameObject);
            }

            var halfSize = MapSize / 2;
            var mapDef = new bool[MapSize, MapSize];

            var startPosition = new Vector2Int(halfSize, halfSize);
            var iterations = 4;
            var steps = 6;

            mapDef[startPosition.x, startPosition.y] = true;

            for (var iteration = 0; iteration < iterations; iteration++)
            {
                var position = startPosition;
                for (var step = 0; step < steps; step++)
                {
                    var direction = (Directions)Random.Range(0, 4);
                    switch (direction)
                    {
                        case Directions.Up:
                            position += Vector2Int.up;
                            break;
                        case Directions.Down:
                            position += Vector2Int.down;
                            break;
                        case Directions.Left:
                            position += Vector2Int.left;
                            break;
                        case Directions.Right:
                            position += Vector2Int.right;
                            break;
                    }

                    if (position.x < 0 || position.x >= MapSize || position.y < 0 || position.y >= MapSize)
                    {
                        break;
                    }

                    mapDef[position.x, position.y] = true;
                }
            }

            for (var x = -0; x < MapSize; x++)
            {
                for (var y = -0; y < MapSize; y++)
                {
                    if (mapDef[x, y])
                    {
                        var pos = new Vector3(
                            (x - startPosition.x) * MapSize,
                            0,
                            (y - startPosition.y) * MapSize
                        );

                        var mapSection = Instantiate(MapeSectionPrefab, pos, Quaternion.identity, transform);
                        _mapSections.Add(mapSection);
                        mapSection.WestWall.SetActive(x == 0 || !mapDef[x - 1, y]);
                        mapSection.EastWall.SetActive(x == (MapSize - 1) || !mapDef[x + 1, y]);
                        mapSection.SouthWall.SetActive(y == 0 || !mapDef[x, y - 1]);
                        mapSection.NorthWall.SetActive(y == (MapSize - 1) || !mapDef[x, y + 1]);
                    }
                }
            }

            var exitLocation = Random.Range(0, _mapSections.Count() - 1);
            _exitSection = _mapSections[exitLocation];

            PlayerController.transform.position = new Vector3(0, 20f, 0);
            PlayerController.gameObject.SetActive(true);
            PickupSpawner.enabled = true;
        }

        void PlayerExit()
        {
            GenerateMap();
        }

        void Update()
        {

        }
    }
}