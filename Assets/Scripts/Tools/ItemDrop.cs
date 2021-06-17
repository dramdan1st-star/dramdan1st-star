using UnityEngine;
using MiniGame.PuzzleGeometri;
using System.Collections.Generic;

namespace Meta.Tools
{
    public class ItemDrop
    {
        private RectTransform _area;
        private GameData _gameData;
        private List<GameObject> _objs = new List<GameObject>();
        
        public ItemDrop(GameData gameData, RectTransform area)
        {
            _area       = area;
            _gameData   = gameData;
        }

        public void Reset()
        {
            for (int i = 0; i < _objs.Count; i++)
            {
                _gameData.DestroyGameObject(_objs[i].gameObject);
            }

            _objs.Clear();
        }

        public GameObject GetNearestObject(Transform other)
        {
            float nearestDistance = 10000f;
            int nearest = 0;
            for (int i = 0; i < _objs.Count; i++)
            {
                float distance = Vector3.Distance(other.position, _objs[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearest = i;
                }
            }

            return _objs[nearest];
        }

        public void SpawnItems(string[] items)
        {
            int[] randomOrder = RandomOrder(items.Length);

            for (int i = 0; i < randomOrder.Length; i++)
            {
                string itemName = items[randomOrder[i]];

                GameObject parent   = _gameData.SpawnObject("Parent");
                parent.name         = itemName;
                parent.transform.SetParent(_area);
                parent.gameObject.transform.localScale = Vector3.one;

                Item item = _gameData.SpawnItem(itemName);
                item.gameObject.transform.SetParent(parent.transform);
                item.gameObject.transform.localScale    = Vector3.one;
                item.gameObject.transform.localPosition = Vector3.zero;

                UnityEngine.UI.Image image = item.GetComponent<UnityEngine.UI.Image>();
                if (image != null) image.color = Color.black;

                float xPos = Random.Range(-_area.rect.width / 2 + 100f, _area.rect.width / 2 - 100f);

                item.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos, 0);

                _objs.Add(parent);
            }
        }

        int[] RandomOrder(int num)
        {
            int[] randomOrder = new int[num];

            for (int i = 0; i < randomOrder.Length; i++)
            {
                randomOrder[i] = -1;
            }

            for (int i = 0; i < randomOrder.Length; i++)
            {
                int random      = -1;
                bool isUnique   = false;

                while (!isUnique)
                {
                    random = Random.Range(0, num);
                    bool isExist = false;

                    for (int j = 0; j < randomOrder.Length; j++)
                    {
                        if (random == randomOrder[j])
                        {
                            isExist = true;
                        }
                    }

                    isUnique = !isExist;
                }

                randomOrder[i] = random;
            }

            return randomOrder;
        }
    }
}