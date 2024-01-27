using Rimaethon.Scripts.Utility;
using UnityEngine;


namespace Rimaethon.Runtime.Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        private int count1;
        private int count2;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = -1;
        }
     
    }
}