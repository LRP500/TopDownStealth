using UnityEngine;

namespace TopDownStealth.Characters
{
    public class Path : MonoBehaviour
    {
        private Vector3[] _waypoints = null;
        public Vector3[] Waypoints
        {
            get
            {
                _waypoints = _waypoints ?? GetWaypoints();
                return _waypoints;
            }
        }

        /// <summary>
        /// Returns number of waypoints in path.
        /// </summary>
        /// <returns></returns>
        public int GetWaypointCount()
        {
            return transform.childCount;
        }

        /// <summary>
        /// Returns waypoint at index position.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector3 GetWaypointAt(int index)
        {
            return transform.GetChild(index).transform.position;
        }

        /// <summary>
        /// Returns all waypoints in path.
        /// </summary>
        /// <returns></returns>
        private Vector3[] GetWaypoints()
        {
            Vector3[] waypoints = new Vector3[transform.childCount];

            for (int i = 0, length = waypoints.Length; i < length; i++)
            {
                waypoints[i] = transform.GetChild(i).transform.position;
            }

            return waypoints;
        }

        #region Editor

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Vector3[] waypoints = GetWaypoints();
            Vector3 previousPosition = waypoints[0];

            foreach (Vector3 waypoint in waypoints)
            {
                Gizmos.DrawWireCube(waypoint, Vector3.one / 2);
                Gizmos.DrawLine(previousPosition, waypoint);
                previousPosition = waypoint;
            }

            Gizmos.DrawLine(previousPosition, waypoints[0]);
        }

        #endregion Editor
    }
}
