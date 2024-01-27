using Rimaethon.Runtime.UI;
using UnityEditor;

namespace Rimaethon.Scripts.UI
{
    public class QuitGameButton : UIButton
    {
        protected override void Awake()
        {
            base.Awake();

            Button.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}