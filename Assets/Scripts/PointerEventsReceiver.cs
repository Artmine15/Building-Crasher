using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Artmine15.HappyBirthday.v3.Gisha
{
    public class PointerEventsReceiver : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public event Action OnPointerUpEvent;
        public event Action OnPointerDownEvent;

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke();
        }
    }
}

