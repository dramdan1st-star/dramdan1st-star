// Created by Tomy
// Consist of the item data
// Create any variable with this class, then you can drag the scriptable object as refference
// Better to be updated to addressable

using UnityEngine;
using Meta.Tools;

namespace MiniGame.PuzzleGeometri
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemData", order = 1)]
    public class GameData : ScriptableObject
    {
        [SerializeField] private string[] _items;

        public GameObject SpawnObject(string name)
        {
            var obj = Resources.Load(name);
            if (obj != null)
            {
                GameObject itemObj = Instantiate(obj) as GameObject;

                return itemObj;
            }

            return null;
        }

        public void DestroyGameObject(GameObject obj)
        {
            Destroy(obj);
        }

        public Item SpawnItem(string type)
        {
            var obj = Resources.Load(type);
            if (obj != null)
            {
                GameObject itemObj = Instantiate(obj) as GameObject;
                Item item = itemObj.GetComponent<Item>();

                return item;
            }

            return null;
        }

        public string[] GetRandomUniqueItems(int number)
        {
            string[] selectItems = new string[number];

            for (int i = 0; i < selectItems.Length; i++)
            {
                string randomItem       = "";
                bool randomItemIsUnique = false;

                while(!randomItemIsUnique)
                {
                    randomItem          = _items[Random.Range(0, _items.Length)];
                    bool isExist        = false;

                    for (int j = 0; j < selectItems.Length; j++)
                    {
                        if (randomItem == selectItems[j])
                        {
                            isExist = true;
                        }
                    }

                    randomItemIsUnique = !isExist;
                }

                selectItems[i]      = randomItem;
            }

            return selectItems;
        }
    }
}