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
        private int openPageCount => _openPageStack.Count;
        private CursorLockMode lockMode;

        private void Awake()
        {
            _menuCanvas = GetComponent<Canvas>();
            lockMode = CursorLockMode.Locked;
            Cursor.visible = false;
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleUIBack();
            }
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
            if (openPageCount ==1)
            {
                PopPageFromStack();
                Time.timeScale = 1;
                EventManager.Instance.Broadcast(GameEvents.OnResume);
                Cursor.visible = false;
                _menuCanvas.enabled = false;
                lockMode = CursorLockMode.Locked;
            }
            else if(openPageCount==0)
            {
                lockMode = CursorLockMode.None;
                PushPageToStack(0);
                EventManager.Instance.Broadcast(GameEvents.OnPause);
                _menuCanvas.enabled = true;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
            else
            {
                PopPageFromStack();
            }
        }
    }
}