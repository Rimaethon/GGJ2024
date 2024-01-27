using System;
using System.Collections.Generic;
using System.Linq;
using Rimaethon.Scripts.Core.Enums;
using Rimaethon.Scripts.Utility;
using UnityEngine;

namespace Rimaethon.Scripts.Managers
{
    public class EventManager : PersistentSingleton<EventManager>
    {
        #region Fields And Properties

        public Dictionary<GameEvents, List<Delegate>> _eventHandlers = new Dictionary<GameEvents, List<Delegate>> ();

        #endregion


        #region Unity Methods

        [SerializeField] private List<string> _eventNames = new();

        private void Start()
        {
            foreach (var events in _eventHandlers.Keys)
            {
                _eventNames.Add(events.ToString());
                foreach (var value in _eventHandlers[events].ToList())
                {
                    Type type = value.Method.DeclaringType;
                    string className = type.Name;
                    _eventNames.Add(className+ "." + value.Method.Name);
                }
               
            }
        }

        protected override void OnApplicationQuit()
        {
            _eventHandlers.Clear();
            Debug.LogWarning("Event Manager is cleared");
            base.OnApplicationQuit();
        }

        #endregion

        #region Event Handlers

        public void AddHandler(GameEvents gameEvent, Action handler)
        {
            if (!_eventHandlers.ContainsKey(gameEvent)) _eventHandlers[gameEvent] = new List<Delegate>();

            _eventHandlers[gameEvent].Add(handler);
            Debug.Log($"Added handler {handler.Method.Name} for game event {gameEvent}");
        }

        public void AddHandler<T>(GameEvents gameEvent, Action<T> handler)
        {
            if (!_eventHandlers.ContainsKey(gameEvent)) _eventHandlers[gameEvent] = new List<Delegate>();

            _eventHandlers[gameEvent].Add(handler);
            Debug.Log($"Added handler {handler.Method.Name} for game event {gameEvent}");
        }

        public void RemoveHandler(GameEvents gameEvent, Action handler)
        {
            if (_eventHandlers.TryGetValue(gameEvent, out var handlers))
            {
                handlers.Remove(handler);
                Debug.Log($"Removed handler {handler.Method.Name} for game event {gameEvent}");

                if (handlers.Count == 0)
                {
                    _eventHandlers.Remove(gameEvent);
                    Debug.Log($"No more handlers for game event {gameEvent}");
                }
            }
        }

        public void RemoveHandler<T>(GameEvents gameEvent, Action<T> handler)
        {
            if (_eventHandlers.TryGetValue(gameEvent, out var handlers))
            {
                handlers.Remove(handler);
                Debug.Log($"Removed handler {handler.Method.Name} for game event {gameEvent}");

                if (handlers.Count == 0)
                {
                    _eventHandlers.Remove(gameEvent);
                    Debug.Log($"No more handlers for game event {gameEvent}");
                }
            }
        }

        #endregion

        #region Event Broadcasting

        public void Broadcast(GameEvents gameEvents)
        {
            ProcessEvent(gameEvents);
        }

        public void Broadcast<T>(GameEvents gameEvent, T arg)
        {
            ProcessEvent(gameEvent, arg);
        }

        private void ProcessEvent(GameEvents gameEvents, params object[] args)
        {
            if (_eventHandlers.TryGetValue(gameEvents, out var eventHandler))
                foreach (var handler in eventHandler)
                {
                    handler.DynamicInvoke(args);
                    Debug.Log(
                        $"Broadcasted event {gameEvents} with arguments {string.Join(", ", args.Select(arg => arg.ToString()))} to handler {handler.Method.Name}");
                }
        }

        #endregion
    }
}