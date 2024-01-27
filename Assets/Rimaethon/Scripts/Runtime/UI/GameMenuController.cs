using System.Collections.Generic;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using UnityEngine;

namespace Rimaethon.Runtime.UI
{
    [RequireComponent(typeof(Canvas))]
    public class GameMenuController : MonoBehaviour
    {
        [SerializeField] private List<UIPage> pages;
        [SerializeField] private bool isMainMenu;

        private readonly Stack<UIPage> _openPageStack = new();
        private UIPage _currentPage;
        private Canvas _menuCanvas;


        private void Awake()
        {
            _menuCanvas = GetComponent<Canvas>();
        }

        private void OnEnable()
        {
            EventManager.Instance.AddHandler(GameEvents.OnUIBack, HandleUIBack);
            EventManager.Instance.AddHandler<int>(GameEvents.OnPageChange, PushPageToStack);
        }

        private void OnDisable()
        {
            if (EventManager.Instance == null) return;
            EventManager.Instance.RemoveHandler(GameEvents.OnUIBack, HandleUIBack);
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnPageChange, PushPageToStack);
        }


        private void HandleUIBack()
        {
            if (isMainMenu || _openPageStack.Count > 1)
                PopPageFromStack();
            else
                TogglePause();
        }

        private void PopPageFromStack()
        {
            if (IsStackEmpty()) return;
            _openPageStack.Peek().gameObject.SetActive(false);
            _openPageStack.Pop();
            if (IsStackEmpty())
            {
                _currentPage = null;
                return;
            }

            _currentPage = _openPageStack.Peek();
            _openPageStack.Peek().gameObject.SetActive(true);
        }

        private void PushPageToStack(int pageIndex)
        {
            var newPage = pages[pageIndex];
            if (_currentPage != null && isMainMenu)
            {
                if (_currentPage == newPage)
                {
                    PopPageFromStack();
                    return;
                }

                PopPageFromStack();
            }

            _currentPage = newPage;
            _openPageStack.Push(newPage);
            _openPageStack.Peek().gameObject.SetActive(true);
        }

        private bool IsStackEmpty()
        {
            return _openPageStack.Count == 0;
        }

        private void TogglePause()
        {
            if (_menuCanvas.enabled)
            {
                PopPageFromStack();
                Time.timeScale = 1;
                EventManager.Instance.Broadcast(GameEvents.OnResume);
                _menuCanvas.enabled = false;
            }
            else
            {
                PushPageToStack(0);
                EventManager.Instance.Broadcast(GameEvents.OnPause);
                _menuCanvas.enabled = true;
                Time.timeScale = 0;
            }
        }
    }
}