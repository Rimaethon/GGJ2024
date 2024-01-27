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

        /*public void LoadData(GameSettingsData data)
        {
            masterVolumeSlider.value = data.MasterVolume;
            Debug.Log($"Master Volume: {data.MasterVolume}");
            Debug.Log(masterVolumeSlider.value);
            musicVolumeSlider.value = data.MusicVolume;
            sfxVolumeSlider.value = data.SFXVolume;
            LoadVolumes();
        }

        public void SaveData(GameSettingsData data)
        {
            data.MasterVolume = masterVolumeSlider.value;
            data.MusicVolume = musicVolumeSlider.value;
            data.SFXVolume = sfxVolumeSlider.value;
        }*/


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
        }


        private void LoadVolumes()
        {
            SetMasterVolume(masterVolumeSlider.value);
            SetMusicVolume(musicVolumeSlider.value);
            SetSfxVolume(sfxVolumeSlider.value);
        }
    }
}