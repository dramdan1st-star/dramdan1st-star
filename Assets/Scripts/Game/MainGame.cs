// Created by Tomy
// Controller the game flow
// Drag this to the scene, add the refference, then play the game

using UnityEngine;
using Meta.Tools;

namespace MiniGame.PuzzleGeometri
{
    public class MainGame : MonoBehaviour
    {
        [Header("Tools")]
        [SerializeField] private int _numQuestion;
        [SerializeField] private int _numSpawnItems = 3;
        [SerializeField] private float _tresholdDrop;
        [SerializeField] private GameData _gameData;

        [Header("HUD")]
        [SerializeField] private UnityEngine.UI.Text _uiTextTimer;
        [SerializeField] private UnityEngine.UI.Text _uiTextQuestionNumber;

        [Header("Object Refference")]
        [SerializeField] private RectTransform _itemSelectArea;
        [SerializeField] private RectTransform _itemDropArea;

        [SerializeField] private UnityEngine.Events.UnityEvent _onFinishGame;

        private ItemsSelect _itemsSelect;
        private ItemDrop _itemDrop;

        private int _currentQuestion;
        private int _numAnswered;

        void Start()
        {
            _currentQuestion = -1;
            _itemsSelect    = new ItemsSelect(_gameData, _itemSelectArea, OnCheckItemDrop);
            _itemDrop       = new ItemDrop(_gameData, _itemDropArea);

            _uiTextQuestionNumber.text = (_currentQuestion + 1) + "/" + _numQuestion;

            StartCoroutine(StopWatch.RunIncrement(_uiTextTimer, null));
            StartNewPuzzle();
        }

        void StartNewPuzzle()
        {
            _currentQuestion++;
            _uiTextQuestionNumber.text = (_currentQuestion + 1) + "/" + _numQuestion;

            if (_currentQuestion >= _numQuestion - 1)
            {
                GameEnd();
                return;
            }

            _numAnswered = 0;
            _itemDrop.Reset();
            _itemsSelect.Reset();

            _itemsSelect.SpawnRandomItems(_numSpawnItems);
            _itemDrop.SpawnItems(_itemsSelect.currentItems);
        }

        void OnCheckItemDrop(string name, Transform dropItem)
        {
            GameObject nearestObj = _itemDrop.GetNearestObject(dropItem);

            float distance = Vector3.Distance(dropItem.position, nearestObj.transform.position);

            if (nearestObj.name == name && distance < _tresholdDrop)
            {
                dropItem.SetParent(nearestObj.transform);
                dropItem.localScale     = Vector3.one;
                dropItem.localPosition  = nearestObj.transform.GetChild(0).localPosition;

                Draggable draggable     = dropItem.GetComponent<Draggable>();
                draggable.enabled       = false;

                _numAnswered++;
                if (_numAnswered >= _numSpawnItems)
                {
                    StartNewPuzzle();
                }
            }
            else
            {
                Draggable draggable = dropItem.GetComponent<Draggable>();
                draggable.ResetPos();
            }
        }

        void GameEnd()
        {
            SaveLoadData.Save(StopWatch.runTime);
            StopWatch.Stop();

            _onFinishGame?.Invoke();
        }
    }
}