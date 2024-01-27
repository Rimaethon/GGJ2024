using System;
using Rimaethon.Scripts.Utility;
using UnityEngine;
using UnityEngine.Audio;
using Application = UnityEngine.Device.Application;
using Screen = UnityEngine.Device.Screen;

namespace Rimaethon.Runtime.Managers
{
    public class SettingsManager : PersistentSingleton<GameManager>
    {
        [SerializeField] private AudioMixer audioMixer;

        // public void LoadData(GameSettingsData data)
        // {
        //     Screen.fullScreen = data.FullScreen;
        //     Screen.SetResolution(data.ScreenWidth, data.ScreenHeight, data.FullScreen);
        //     Application.targetFrameRate = data.FrameRate;
        // }
        //
        // public void SaveData(GameSettingsData data)
        // {
        //     throw new NotImplementedException();
        // }
    }
}