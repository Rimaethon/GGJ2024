using UnityEngine;

namespace Rimaethon.Scripts.UI
{
    public class CursorChanger : MonoBehaviour
    {
        [SerializeField] private Texture2D newCursorSprite;


        public void Start()
        {
            ChangeCursor();
        }


        private void ChangeCursor()
        {
            Cursor.lockState = CursorLockMode.Confined;

            var hotSpot = new Vector2();
            if (newCursorSprite == null) return;

            hotSpot.x = newCursorSprite.width / 2;
            hotSpot.y = newCursorSprite.height / 2;

            Cursor.SetCursor(newCursorSprite, hotSpot, CursorMode.Auto);
        }
    }
}