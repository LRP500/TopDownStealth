using System.Collections.Generic;
using Tools;
using TopDownStealth.Characters;
using UnityEngine;

namespace TopDownStealth
{
    public class PositionMarkerManager : MonoBehaviour
    {
        [SerializeField]
        private PositionMarker _markerPrefab = null;

        [SerializeField]
        private CharacterVariable _player = null;

        [SerializeField]
        private CharacterListVariable _runtimeGuards = null;

        private List<PositionMarker> _markers = null;

        private void Awake()
        {
            EventManager.Instance.Subscribe(GameplayEvent.LevelInitialized, CreateMarkers);
        }

        private void CreateMarkers(object arg = null)
        {
            _markers = new List<PositionMarker>();

            foreach (Guard guard in _runtimeGuards.Items)
            {
                PositionMarker instance = Instantiate(_markerPrefab, transform);
                instance.Initialize(_player.Value.transform, guard);
                _markers.Add(instance);
            }
        }
    }
}
