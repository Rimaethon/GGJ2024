using Rimaethon.Runtime.UI;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;

namespace Rimaethon.Prefabs.UI
{
    public class BackButton : UIButton
    {
        protected override void Awake()
        {
            base.Awake();

            Button.onClick.AddListener(OnBackButtonPress);
        }

        private void OnBackButtonPress()
        {
            EventManager.Instance.Broadcast(GameEvents.OnUIBack);
        }
    }
}