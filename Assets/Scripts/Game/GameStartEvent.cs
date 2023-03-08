using System;
using System.Collections.Generic;
using AI.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game
{
    public class GameStartEvent : DesignPattern.Singleton<GameStartEvent>
    {

        public AISpawnEvent spanEvent;
        public List<GameObject> openObjectCanvasList;
        public GameObject updatePopUpCanvas;
        public GameObject startCanvas;
        public bool isGamePlay;
        private void Start()
        {
            spanEvent.enabled = false;
            updatePopUpCanvas.SetActive(true);
            
        }


        public void StartButtonClicked()
        {
            isGamePlay = true;
            startCanvas.SetActive(false);
            spanEvent.enabled = true;
            foreach (var currentObject in openObjectCanvasList)
            {
                currentObject.gameObject.SetActive(true);
            }
        }
    }
}
