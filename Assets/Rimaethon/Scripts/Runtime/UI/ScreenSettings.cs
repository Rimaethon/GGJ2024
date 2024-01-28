using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rimaethon.Runtime.UI
{
    public class ScreenSettings : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown fullScreenDropdown;
        private int _frameRate;
        private bool _isFullScreen;
        private int _screenHeight;
        private int _screenWidth;

        private void Awake()
        {
            InitializeResolutionDropdown();
            InitializeFullScreenDropdown();
            LoadResolution();
        }

        public void LoadData()
        {
            _screenHeight = PlayerPrefs.GetInt("ScreenHeight", 1080);
            _screenWidth = PlayerPrefs.GetInt("ScreenWidth", 1920);
            _isFullScreen = PlayerPrefs.GetInt("FullScreen", 0) == 1;
            _frameRate = PlayerPrefs.GetInt("FrameRate", 60);
            LoadResolution();
            FullScreenToggle(fullScreenDropdown.value);
        }

        public void SaveData()
        {
            PlayerPrefs.SetInt("ScreenHeight", _screenHeight);
            PlayerPrefs.SetInt("ScreenWidth", _screenWidth);
            PlayerPrefs.SetInt("FullScreen", _isFullScreen ? 1 : 0);
            PlayerPrefs.SetInt("FrameRate", _frameRate);
        }

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

        private void InitializeFullScreenDropdown()
        {
            fullScreenDropdown.ClearOptions();
            var fullScreenOptions = new List<string> { "Enable", "Disable" };
            fullScreenDropdown.AddOptions(fullScreenOptions);
            fullScreenDropdown.onValueChanged.AddListener(FullScreenToggle);
        }

        public void ChangeResolution()
        {
            var selectedResolution = resolutionDropdown.options[resolutionDropdown.value].text;
            var resolution = selectedResolution.Split('x');
            _screenWidth = int.Parse(resolution[0]);
            _screenHeight = int.Parse(resolution[1]);

            Screen.SetResolution(_screenWidth, _screenHeight, _isFullScreen);
            SaveData();
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

        private void FullScreenToggle(int dropdownValue)
        {
            _isFullScreen = dropdownValue == 0;
            Screen.fullScreen = _isFullScreen;
        }
    }
}