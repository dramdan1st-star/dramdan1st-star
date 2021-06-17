using UnityEngine;
using MiniGame.PuzzleGeometri;
using System.Collections.Generic;
using System;

namespace Meta.Tools
{
    public class ItemsSelect
    {
        private RectTransform _area;
        private GameData _gameData;
        private List<GameObject> _objs = new List<GameObject>();
        private Action<string, Transform> _onDropSelectedItem;

        private string[] _currentItems;
        public string[] currentItems
        {
            get { return _currentItems; }
        }

        public ItemsSelect(GameData gameData, RectTransform area, Action<string, Transform> onDropSelectedItem)
        {
            _area               = area;
            _gameData           = gameData;
            _onDropSelectedItem = onDropSelectedItem;
        }

        public void SpawnRandomItems(int num)
        {
            _currentItems = _gameData.GetRandomUniqueItems(num);
            for (int i = 0; i < _currentItems.Length; i++)
            {
                GameObject parent   = _gameData.SpawnObject("Parent");
                parent.name         = "parent " + _currentItems[i];
                parent.transform.SetParent(_area);
                parent.gameObject.transform.localScale = Vector3.one;

                Item item = _gameData.SpawnItem(_currentItems[i]);
                item.gameObject.transform.SetParent(parent.transform);
                item.gameObject.transform.localScale    = Vector3.one;
                item.gameObject.transform.localPosition = Vector3.zero;
                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(UnityEngine.Random.Range(-100f, 100f),0);

                Draggable drag = item.gameObject.AddComponent<Draggable>();
                drag.Init(_currentItems[i], OnFinishDrag);

                _objs.Add(parent);
            }
        }

        void OnFinishDrag(string itemName, Transform objectTransform)
        {
            _onDropSelectedItem?.Invoke(itemName, objectTransform);
        }

        public void Reset()
        {
            for (int i = 0; i < _objs.Count; i++)
            {
                _gameData.DestroyGameObject(_objs[i]);
            }

            _objs.Clear();
        }
    }
}