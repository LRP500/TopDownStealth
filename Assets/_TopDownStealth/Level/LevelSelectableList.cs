using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace TopDownStealth
{
    [CreateAssetMenu(menuName = "Top Down Stealth/Level Selectable List")]
    public class LevelSelectableList : ScriptableObject
    {
        [SerializeField]
        [AssetList(Path = "_TopDownStealth/Content/Levels/")]
        private List<Level> _items = null;
        public List<Level> Items => _items;

        public Level Random()
        {
            return _items[UnityEngine.Random.Range(0, _items.Count)];
        }
    }
}