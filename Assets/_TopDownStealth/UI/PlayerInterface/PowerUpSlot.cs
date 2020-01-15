﻿using TMPro;
using UnityEngine;

namespace TopDownStealth
{
    public class PowerUpSlot : MonoBehaviour
    {
        [SerializeField]
        private KeyCode _displayedkey = default;

        [SerializeField]
        private TextMeshProUGUI _keyText = null;

        private void Awake()
        {
            _keyText.text = _displayedkey.ToString();
        }
    }
}
