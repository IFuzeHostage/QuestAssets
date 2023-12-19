using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class UI_InfoPanel : MonoBehaviour
    {
        private const float HEIGHT_OFFSET = 2f;

        [SerializeField]
        private TextMeshProUGUI _text;

        private Transform _followed;

        public void SetFollowed(Transform toFollow)
        {
            _followed = toFollow;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        private void Update()
        {
            if (_followed)
                transform.position = Camera.main.WorldToScreenPoint(_followed.position + Vector3.up * HEIGHT_OFFSET);
        }
    }
}