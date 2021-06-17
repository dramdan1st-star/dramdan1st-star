using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Meta.Tools
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _startPos;

        private string _itemName;
        private Action<string, Transform> _onFinishDrag;

        public void Init(string itemName, Action<string, Transform> onFinishDrag)
        {
            _itemName       = itemName;
            _onFinishDrag   = onFinishDrag;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startPos = transform.position;
        }

        public void OnDrag(PointerEventData data)
        {
            transform.Translate(data.delta);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _onFinishDrag?.Invoke(_itemName, this.transform);
        }

        public void ResetPos()
        {
            transform.position = _startPos;
        }
    }
}