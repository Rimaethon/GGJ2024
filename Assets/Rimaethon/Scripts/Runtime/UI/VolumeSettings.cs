using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Rimaethon.Runtime.UI
{
    public class VolumeSettings : MonoBehaviour
    {
        private const string MasterVolume = "Master";
        private const string MusicVolume = "Music";
        private const string SfxVolume = "SFX";
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        private void Awake()
        {
            if (audioMixer == null || masterVolumeSlider == null || sfxVolumeSlider == null ||
                musicVolumeSlider == null)
            {
                Debug.LogError("One or more components are not assigned in the Inspector.");
                enabled = false;
                return;
            }

            LoadVolumes();
        }

        private void OnEnable()
        {
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        private void OnDisable()
        {
            masterVolumeSlider.onValueChanged.RemoveListener(SetMasterVolume);
            musicVolumeSlider.onValueChanged.RemoveListener(SetMusicVolume);
            sfxVolumeSlider.onValueChanged.RemoveListener(SetSfxVolume);
        }

        private void SetMasterVolume(float volume)
        {
            SetVolume(MasterVolume, masterVolumeSlider, volume);
        }

        private void SetMusicVolume(float volume)
        {
            SetVolume(MusicVolume, musicVolumeSlider, volume);
        }

        private void SetSfxVolume(float volume)
        {
            SetVolume(SfxVolume, sfxVolumeSlider, volume);
        }

        private void SetVolume(string volumeType, Slider volumeSlider, float volume)
        {
            audioMixer.SetFloat(volumeType, Mathf.Lerp(-80, 0, volume));
            volumeSlider.value = volume;
            PlayerPrefs.SetFloat(volumeType, volume);
        }

        private void LoadVolumes()
        {
            masterVolumeSlider.value = PlayerPrefs.GetFloat(MasterVolume, 0.5f);
            musicVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolume, 1f);
            sfxVolumeSlider.value = PlayerPrefs.GetFloat(SfxVolume, 1f);
            SetMasterVolume(masterVolumeSlider.value);
            SetMusicVolume(musicVolumeSlider.value);
            SetSfxVolume(sfxVolumeSlider.value);
        }
    }
}