using Extensions;
using UnityEngine;

namespace TopDownStealth.Characters.Behaviours
{
    [CreateAssetMenu(menuName = "Top Down Stealth/Characters/Behaviours/Follow Path")]
    public class FollowPathBehaviour : CharacterBehaviour
    {
        [SerializeField]
        private float _reachDistanceTolerance = 0.05f;

        public override void Initialize(Character character)
        {
            Vector3[] waypoints = character.Path?.Waypoints;
            int initialWaypoint = GetClosestWaypoint(character.transform.position, waypoints);
            character.Brain.Remember("path_waypoints", waypoints);
            character.Brain.Remember("current_waypoint_index", initialWaypoint);
        }

        public override void Run(Character character)
        {
            Vector3[] waypoints = character.Brain.Remember<Vector3[]>("path_waypoints");
            int current = character.Brain.Remember<int>("current_waypoint_index");

            if (waypoints != null && waypoints.Length > 2)
            {
                if (HasReachedWaypoint(character, waypoints[current]))
                {
                    GetNextWaypoint(ref current, waypoints);
                    character.Brain.Remember("current_waypoint_index", current);
                }

                Vector3 direction = waypoints[current] - character.transform.position;
                direction.SetY(0);
                character.Move(direction.normalized);
            }
        }

        /// <summary>
        /// Returns true if character has reached waypoint, returns false otherwise.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="waypoint"></param>
        /// <returns></returns>
        private bool HasReachedWaypoint(Character character, Vector3 waypoint)
        {
            return character.transform.position.AlmostEqualXZ(waypoint, _reachDistanceTolerance);
        }

        /// <summary>
        /// Returns the index of the next waypoint in path.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="waypoints"></param>
        private void GetNextWaypoint(ref int current, Vector3[] waypoints)
        {
            current = (current < waypoints.Length - 1) ? current + 1 : 0;
        }

        /// <summary>
        /// Returns the index of the closest waypoint.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="waypoints"></param>
        /// <returns></returns>
        private int GetClosestWaypoint(Vector3 position, Vector3[] waypoints)
        {
            int closestWaypointIndex = 0;
            float closestWaypointDistance = float.PositiveInfinity;

            for (int i = 0, length = waypoints.Length; i < length; i++)
            {
                float distanceToWaypoint = Vector3.Distance(position, waypoints[i]);
                if (distanceToWaypoint < closestWaypointDistance)
                {
                    closestWaypointIndex = i;
                    closestWaypointDistance = distanceToWaypoint;
                }
            }

            return closestWaypointIndex;
        }
    }
}
