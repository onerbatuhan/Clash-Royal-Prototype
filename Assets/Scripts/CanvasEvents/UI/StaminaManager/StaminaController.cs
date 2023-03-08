using System;
using System.Collections.Generic;
using AI.ScriptTable;
using DesignPattern;
using UnityEngine;

namespace CanvasEvents.UI.StaminaManager
{
    public class StaminaController : Singleton<StaminaController>
    {
        public List<GameObject> staminaUIObjects;
        public int staminaCount;
        private int currentStaminaUIIndex; // şu anda işlem yapılan staminaUIObjects indeksi

        float timer = 0f; // zamanlayıcı değişkeni

        private void Start()
        {
            foreach (Transform child in transform)
            {
                staminaUIObjects.Add(child.gameObject);
            }
            staminaUIObjects.Reverse();
            staminaCount = staminaUIObjects.Count;

            currentStaminaUIIndex = staminaUIObjects.Count - 1; 
        }

        public void StaminaUse(AIData aiData)
        {
            if (staminaCount >= aiData.stamina)
            {
                for (int i = 0; i < aiData.stamina; i++)
                {
                    if (currentStaminaUIIndex >= 0 && currentStaminaUIIndex < staminaUIObjects.Count) 
                    {
                        if (staminaUIObjects[currentStaminaUIIndex].activeSelf) 
                        {
                            staminaUIObjects[currentStaminaUIIndex].SetActive(false);
                        }
                        currentStaminaUIIndex--;
                    }
                }
                staminaCount -= aiData.stamina;
            }
        }
        
        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 1.5f && staminaUIObjects.Count > staminaCount)
            {
                
                StaminaAdd(1);
                timer = 0f;
            }
        }
        
        public void StaminaAdd(int count)
        {
            staminaCount += count;
            currentStaminaUIIndex = staminaCount - 1;
            
            for (int i = 0; i < staminaUIObjects.Count; i++)
            {
                if (i <= currentStaminaUIIndex) 
                {
                    if (!staminaUIObjects[i].activeSelf)
                    {
                        staminaUIObjects[i].SetActive(true);
                    }
                }
                else
                {
                    if (staminaUIObjects[i].activeSelf)
                    {
                        staminaUIObjects[i].SetActive(false);
                    }
                }
            }
        }
    }
}