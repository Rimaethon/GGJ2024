using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.Managers
{
    public class AudioManager : PersistentSingleton<AudioManager>
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        private int _currentMusicIndex;
        private int _currentSFXIndex;


        private AudioClip[] _musicClips;
        private AudioClip[] _sfxClips;
        private bool musicOn;
        private bool sfxOn;

        /*protected override void Awake()
        {
            base.Awake();
            _musicClips = audioLibrary.MusicClips;
            _sfxClips = audioLibrary.SFXClips;
        }

        private void Start()
        {
            PlayMusic(MusicClips.MenuMusic);
        }

        public void PlayMusic(MusicClips clipEnum)
        {
            if (musicOn) return;
            if (musicSource.isPlaying) musicSource.Stop();
            _currentMusicIndex = (int)clipEnum;
            musicSource.clip = _musicClips[_currentMusicIndex];
            musicSource.Play();
        }

        public void PlaySFX(SFXClips clipEnum)
        {
            if (sfxOn) return;
            _currentSFXIndex = (int)clipEnum;
            sfxSource.PlayOneShot(_sfxClips[_currentSFXIndex]);
        }*/
    }
}