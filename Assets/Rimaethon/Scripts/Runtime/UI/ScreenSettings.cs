using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon.Runtime.UI
{
    public class ScreenSettings : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private Toggle fullScreenToggle;
        private int _frameRate;
        private bool _isFullScreen;
        private int _screenHeight;
        private int _screenWidth;

        private void Awake()
        {
            InitializeResolutionDropdown();
            LoadResolution();
        }


        private void OnEnable()
        {
            fullScreenToggle.onValueChanged.AddListener(FullScreenToggle);
        }

        /*public void LoadData(GameSettingsData data)
        {
            _screenHeight = data.ScreenHeight;
            _screenWidth = data.ScreenWidth;
            _isFullScreen = data.FullScreen;
            _frameRate = data.FrameRate;
            LoadResolution();
            FullScreenToggle(_isFullScreen);
        }

        public void SaveData(GameSettingsData data)
        {
            data.ScreenHeight = _screenHeight;
            data.ScreenWidth = _screenWidth;
            data.FullScreen = _isFullScreen;
            data.FrameRate = _frameRate;
        }*/


        private void InitializeResolutionDropdown()
        {
            resolutionDropdown.ClearOptions();
            var resolutions = Screen.resolutions;
            var resolutionOptions = new List<string>();

            foreach (var res in resolutions)
            {
                var option = res.width + "x" + res.height;
                if (!resolutionOptions.Contains(option)) resolutionOptions.Add(option);
            }

            resolutionDropdown.AddOptions(resolutionOptions);
        }

        public void ChangeResolution()
        {
            var selectedResolution = resolutionDropdown.options[resolutionDropdown.value].text;
            var resolution = selectedResolution.Split('x');
            _screenWidth = int.Parse(resolution[0]);
            _screenHeight = int.Parse(resolution[1]);

            Screen.SetResolution(_screenWidth, _screenHeight, _isFullScreen);
        }


        private void LoadResolution()
        {
            for (var i = 0; i < resolutionDropdown.options.Count; i++)
            {
                var option = resolutionDropdown.options[i].text;
                var resolution = option.Split('x');
                if (int.Parse(resolution[0]) == _screenWidth && int.Parse(resolution[1]) == _screenHeight)
                {
                    resolutionDropdown.value = i;
                    resolutionDropdown.RefreshShownValue();
                    ChangeResolution();
                    return;
                }
            }
        }

        private void FullScreenToggle(bool isFullScreen)
        {
            _isFullScreen = isFullScreen;
            Screen.fullScreen = _isFullScreen;
        }
    }
}