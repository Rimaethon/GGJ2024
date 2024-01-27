using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.UI
{
    public class UIManager : MonoBehaviour
    {
        private void Start()
        {
            EventManager.Instance.Broadcast(GameEvents.OnGameStart);
        }
    }
}