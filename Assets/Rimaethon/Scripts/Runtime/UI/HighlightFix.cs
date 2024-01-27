using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rimaethon.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class HighlightFix : MonoBehaviour, IPointerExitHandler
    {
        private static HighlightFix lastHovered;
        private Button _button;
        private SpriteState _defaultSpriteState;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _defaultSpriteState = _button.spriteState;
        }


        public void OnPointerExit(PointerEventData eventData)
        {
        }

        private void ResetButtonState()
        {
            if (_button != null) _button.spriteState = _defaultSpriteState;
        }
    }
}