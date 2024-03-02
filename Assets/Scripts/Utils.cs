using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace Row.Utils
{

    public static class Utils
    {
        public static Vector3 GetWorldPosAtY()
        {
            return GetWorldPosAtY(0,Camera.main);
        }
        public static Vector3 GetWorldPosAtY(float y)
        {
            return GetWorldPosAtY(y,Camera.main);
        }
        public static Vector3 GetWorldPosAtY(float y, Camera cam)
        {
            Plane plane = new Plane(Vector3.up,y);

            float distance;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if(plane.Raycast(ray,out distance))
            {
                return ray.GetPoint(distance);
            }
            return cam.gameObject.transform.position;
        } 

        public static void SetVisible(bool visible, GameObject obj)
        {
            obj.SetActive(visible);
        }

        public static Vector3 ProjectDirectionOnPlane(Vector3 direction, Vector3 normal)
        {
            return (direction - normal * Vector3.Dot(direction, normal)).normalized;
        }

        //Returns 'true' if we touched or hovering on Unity UI element.
        public static bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
    
    
        //Returns 'true' if we touched or hovering on Unity UI element.
        private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    return true;
            }
            return false;
        }
    
    
        //Gets all event system raycast results of current mouse or touch position.
        static List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }

        // Remap a value from one range to another
        static float RemapToDifferentRange(float from, float fromMin, float fromMax, float toMin, float toMax)
        {
            var fromAbs = from - fromMin;
            var fromMaxAbs = fromMax - fromMin;

            var normal = fromAbs / fromMaxAbs;

            var toMaxAbs = toMax - toMin;
            var toAbs = toMaxAbs * normal;

            var to = toAbs + toMin;

            return to;
        }

        public static Vector3Int OffsetToCube(Vector2Int offset)
        {
            var q = offset.x - (offset.y + (offset.y % 2)) / 2;
            var r = offset.y;
            return new Vector3Int(q, r, -q - r);
        }

        public static Vector3 GetPositionForHexFromCoordinate(int column, int row, float radius = 1f, bool isFlatTopped = false)
        {
            float width, height, xPosition, yPosition, horizontalDistance, verticalDistance, offset;
            bool shouldOffset;
            float size = radius;

            if(!isFlatTopped)
            {
                shouldOffset = (row % 2) == 0;
                width = Mathf.Sqrt(3) * size;
                height = 2f * size;

                horizontalDistance = width;
                verticalDistance = height * (3f / 4f);

                offset = (shouldOffset) ? width / 2 : 0;

                xPosition = (column * (horizontalDistance)) + offset;
                yPosition = (row * verticalDistance);
            }
            else
            {
                shouldOffset = (column % 2) == 0;
                width = 2f * size;
                height = Mathf.Sqrt(3f) * size;

                horizontalDistance = width * (3f / 4f);
                verticalDistance = height;

                offset = (shouldOffset) ? height / 2 : 0;
                xPosition = (column * (horizontalDistance));
                yPosition = (row * verticalDistance) - offset;
            }

            return new Vector3(xPosition, 0, -yPosition);
        }

        public static async void ActionWithDelay(int delay, System.Action action)
        {
            await System.Threading.Tasks.Task.Delay(delay);
            action?.Invoke();
        }
    }
}