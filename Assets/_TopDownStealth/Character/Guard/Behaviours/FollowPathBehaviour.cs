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
            character.Brain.Remember("path_waypoints", waypoints);
            character.Brain.Remember("current_waypoint_index", 0);
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
                character.Move(Vector3.Normalize(direction));
            }
        }

        private bool HasReachedWaypoint(Character character, Vector3 waypoint)
        {
            return character.transform.position.AlmostEqualXZ(waypoint, _reachDistanceTolerance);
        }

        private void GetNextWaypoint(ref int current, Vector3[] waypoints)
        {
            current = (current < waypoints.Length - 1) ? current + 1 : 0;
        }
    }
}
