using System.Threading.Tasks;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Managers;
using TMPro;
using UnityEngine;

namespace Rimaethon.Runtime.UI.UIElements
{
    public class HUDDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI blueGemCountText;
        [SerializeField] private TextMeshProUGUI redGemCountText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private RectTransform RedGemHUD;
        private int _scoreCount;
        [Tooltip("1.5 is default value")]
        [SerializeField] private float lerpSpeed=1.5f;

        private void Awake()
        {
            RedGemHUD.pivot = new Vector2(1, 1);
        }

        private void OnEnable()
        {
            EventManager.Instance.AddHandler<int>(GameEvents.OnGemCollected, UpdateScoreDisplay);
            EventManager.Instance.AddHandler(GameEvents.OnRedGemCollected, HandleRedGemCollected);
            EventManager.Instance.AddHandler<int>(GameEvents.OnHealthChange, UpdateHealthDisplay);
        }

        private void OnDisable()
        {
            if (EventManager.Instance == null) return;
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnGemCollected, UpdateScoreDisplay);
            EventManager.Instance.RemoveHandler(GameEvents.OnRedGemCollected, HandleRedGemCollected);
            EventManager.Instance.RemoveHandler<int>(GameEvents.OnHealthChange, UpdateHealthDisplay);
        }

        private async Task Test1()
        {
            Debug.Log(Time.deltaTime);
            await Task.Yield();
        }
        private void HandleRedGemCollected()
        {
           // AnimateRedGemHUD().Forget();
            Debug.Log("Red Gem Collected");
        }
        /*private async UniTask AnimateRedGemHUD()
        {
            float time = 0;

            await AnimatePivot(new Vector2(1, 1), new Vector2(0, 1), time);

            await UniTask.Delay(1000);

            await AnimatePivot(new Vector2(0, 1), new Vector2(1, 1), time);
        }

        private async UniTask AnimatePivot(Vector2 startPivot, Vector2 endPivot, float time)
        {
            while (RedGemHUD.pivot != endPivot)
            {
                RedGemHUD.pivot = Vector2.Lerp(startPivot, endPivot, time);
                await UniTask.Yield();
                time += Time.deltaTime * lerpSpeed;
            }
        }
        */

      
        private void UpdateGemDisplay(int gemAmount)
        {
            redGemCountText.text = $"X {gemAmount}";
        }
     
        private void UpdateScoreDisplay(int scoreAmount)
        {
            _scoreCount += scoreAmount;
            blueGemCountText.text = $"X {_scoreCount}";
        }

        private void UpdateHealthDisplay(int healthValue)
        {
            healthText.text = healthValue.ToString();
        }
        
    }
}