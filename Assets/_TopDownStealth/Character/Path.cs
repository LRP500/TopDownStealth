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

        private Vector3[] GetWaypoints()
        {
            Vector3[] waypoints = new Vector3[transform.childCount];

            for (int i = 0, length = waypoints.Length; i < length; i++)
            {
                waypoints[i] = transform.GetChild(i).transform.position;
            }

            return waypoints;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach (Vector3 waypoint in Waypoints)
            {
                Gizmos.DrawWireSphere(waypoint, 0.5f);

            }
        }
    }
}
