using Rimaethon.Runtime.UI;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Prefabs.UI
{
    public class PageChangerButton : UIButton
    {
        [SerializeField] private int pageIndex;

        protected override void Awake()
        {
            base.Awake();
            Button.onClick.AddListener(ChangePage);
        }

        private void ChangePage()
        {
            EventManager.Instance.Broadcast(GameEvents.OnPageChange, pageIndex);
        }
    }
}