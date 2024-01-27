using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.UI
{
    public class SceneChangerButton : UIButton
    {
        [SerializeField] private int sceneIndex;

        protected override void Awake()
        {
            base.Awake();
            Button.onClick.AddListener(ChangeScene);
        }

        private void ChangeScene()
        {
            EventManager.Instance.Broadcast(GameEvents.OnSceneChange, sceneIndex);
        }
    }
}