using UnityEditor;
using UnityEngine;

namespace TopDownStealth
{
    [CustomEditor(typeof(FieldOfView))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            FieldOfView fov = (FieldOfView)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.Radius);
            Vector3 viewAngleA = fov.DirFromAngle(-fov.Angle / 2, false);
            Vector3 viewAngleB = fov.DirFromAngle(fov.Angle / 2, false);

            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.Radius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.Radius);

            Handles.color = Color.red;

            if (fov.VisibleTargets != null)
            {
                foreach (Transform visibleTarget in fov.VisibleTargets)
                {
                    Handles.DrawLine(fov.transform.position, visibleTarget.position);
                }
            }
        }
    }
}
