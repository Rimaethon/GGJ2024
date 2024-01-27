using System.Collections;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Rimaethon.Runtime.UI
{
    //This class is a singleton because of the chance of duplication. 
    public class LoadingHandler : PersistentSingleton<LoadingHandler>
    {
        private const float TargetPivotX = 1.25f;
        private const float TargetPivotY = 0.5f;
        private const float ScreenMoveDuration = 0.4f;
        [SerializeField] private Image loadingScreenImage;
        [SerializeField] private GameObject loadingIcon;
        private readonly WaitForSeconds _waitForHalfSecond = new(0.5f);
        private bool _isRespawnScreen;
        private bool _isScreenMoving;
        private bool _isScreenShouldContinue;
        private Canvas _loadingScreenCanvas;
        private AsyncOperation _sceneUnload;

        protected override void Awake()
        {
            base.Awake();
            _loadingScreenCanvas = GetComponent<Canvas>();
            _loadingScreenCanvas.enabled = false;
        }

        private void OnEnable()
        {
            EventManager.Instance.AddHandler(GameEvents.OnPlayerRespawn, HandleScreen);
            EventManager.Instance.AddHandler<int>(GameEvents.OnSceneChange, HandleLoading);
        }

        private void OnDisable()
        {
            if (EventManager.Instance == null) return;
            EventManager.Instance.RemoveHandler(GameEvents.OnPlayerRespawn, HandleScreen);
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnSceneChange, HandleLoading);
        }

        private void HandleLoading(int sceneIndex)
        {
            if (_isScreenMoving) return;
            StartCoroutine(LoadScene(sceneIndex));
        }

        private void HandleScreen()
        {
            if (_isScreenMoving) return;
            _isScreenShouldContinue = true;
            _isRespawnScreen = true;
            StartCoroutine(MoveLoadingScreen(new Vector2(TargetPivotX, TargetPivotY), ScreenMoveDuration));
        }

        private IEnumerator LoadScene(int sceneIndex)
        {
            _isScreenShouldContinue = false;
            _isRespawnScreen = false;
            StartCoroutine(MoveLoadingScreen(new Vector2(TargetPivotX, TargetPivotY), ScreenMoveDuration));
            loadingIcon.SetActive(true);
            _sceneUnload = SceneManager.LoadSceneAsync(sceneIndex);
            _sceneUnload.allowSceneActivation = false;

            while (_sceneUnload.progress < 0.9f) yield return null;
            yield return _waitForHalfSecond;
            loadingIcon.SetActive(false);
            _isScreenShouldContinue = true;
            _sceneUnload.allowSceneActivation = true;
        }

        private IEnumerator MoveLoadingScreen(Vector2 targetPivot, float duration)
        {
            _loadingScreenCanvas.enabled = true;
            _isScreenMoving = true;
            var startPivot = loadingScreenImage.rectTransform.pivot;
            var halfWayPivot = (targetPivot + startPivot) / 2;
            var elapsedTime = 0f;
            var startTime = Time.time;

            while (elapsedTime < duration)
            {
                elapsedTime = Time.time - startTime;
                var t = Mathf.Clamp01(elapsedTime / duration);
                loadingScreenImage.rectTransform.pivot = Vector2.Lerp(startPivot, halfWayPivot, t);
                yield return null;
            }

            if (_isRespawnScreen) EventManager.Instance.Broadcast(GameEvents.OnPlayerRespawned);

            while (!_isScreenShouldContinue) yield return null;

            elapsedTime = 0f;
            startTime = Time.time;
            while (elapsedTime < duration)
            {
                elapsedTime = Time.time - startTime;
                var t = Mathf.Clamp01(elapsedTime / duration);
                loadingScreenImage.rectTransform.pivot = Vector2.Lerp(halfWayPivot, targetPivot, t);
                yield return null;
            }

            _loadingScreenCanvas.enabled = false;
            _isScreenMoving = false;
            loadingScreenImage.rectTransform.pivot = startPivot;
        }
    }
}