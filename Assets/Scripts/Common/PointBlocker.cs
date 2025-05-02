/// Owner: Dongjin Kuk
/// Block touch events when the user touches UI.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace IMP.Common
{
    public class PointBlocker
    {
        public static bool IsOverUI(Vector2 pos)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = new Vector2(pos.x, pos.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            return results.Count > 0;
        }
    }
}

