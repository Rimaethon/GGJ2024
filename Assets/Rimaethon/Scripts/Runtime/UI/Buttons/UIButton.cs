using Rimaethon.Scripts.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rimaethon.Runtime.UI
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        protected Button Button;

        protected virtual void Awake()
        {
            Button = GetComponent<Button>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
           // AudioManager.Instance.PlaySFX(SFXClips.UIForward);
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            //AudioManager.Instance.PlaySFX(SFXClips.UISwitch);
        }
    }
}