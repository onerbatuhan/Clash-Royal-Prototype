using System;
using System.IO;
using Cloud;
using Game;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CanvasEvents.UI.AddressablaManager
{
    public class AddressableEvents : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
    {
        public TextMeshProUGUI fileSizeText;
        public AddressableData addressableData;
        
        private void OnEnable()
        {
            
            Addressables.CleanBundleCache();
            Caching.ClearCache();
            AsyncOperationHandle<long> downloadSizeHandle = Addressables.GetDownloadSizeAsync(addressableData.referenceObject);
            downloadSizeHandle.Completed += downloadSizeOperation =>
            {
                if (downloadSizeOperation.Status == AsyncOperationStatus.Succeeded)
                {
                    long downloadSizeBytes = downloadSizeOperation.Result;
                    float downloadSizeKB = (float)downloadSizeBytes / 1024f;
                    fileSizeText.text = downloadSizeKB +" KB";
               
                    
                    
                }
            };
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            AddressablesManager.Instance.AddressableObjectLoad(addressableData);
            transform.parent.gameObject.SetActive(false);
            GameStartEvent.Instance.startCanvas.SetActive(true);
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
        }

        public void CancelButtonClicked()
        {
            transform.parent.gameObject.SetActive(false);
            GameStartEvent.Instance.startCanvas.SetActive(true);
        }
    }

   
}
