using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Cloud
{
    
    public class AddressablesManager : DesignPattern.Singleton<AddressablesManager>
    {
        
      
      

        
        public TextMeshProUGUI fileSizeText;
        public List<Transform> objectListPosition;
        public void AddressableObjectLoad(AddressableData addressableData)
        {
            Load(addressableData);
        }

        #region default

        //      public void Load(AddressableData addressableData)
// {
//     // Clean the cache first
//     Caching.ClearCache();
//     AsyncOperationHandle handleCleanCache = Addressables.CleanBundleCache();
//     handleCleanCache.Completed += cleanCacheOperation =>
//     {
//         if (cleanCacheOperation.Status == AsyncOperationStatus.Succeeded)
//         {
//             Debug.Log("Addressables cache cleared.");
//
//             
//             // Then load the asset
//             AsyncOperationHandle<Material> handleLoadAsset = Addressables.LoadAssetAsync<Material>(addressableData.referenceObject);
//             handleLoadAsset.Completed += loadAssetOperation =>
//             {
//                 if (loadAssetOperation.Status == AsyncOperationStatus.Succeeded)
//                 {
//                     Material loadedMaterial = loadAssetOperation.Result;
//
//                     foreach (GameObject obj in objectList)
//                     {
//                         MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
//                         if (renderer != null)
//                         {
//                             renderer.material = loadedMaterial;
//                         }
//                     }
//
//                     // Get the download size of the asset
//                     AsyncOperationHandle<long> fileSize = Addressables.GetDownloadSizeAsync(addressableData.referenceObject.RuntimeKey.ToString());
//                     fileSize.Completed += fileSizeOperation =>
//                     {
//                         if (fileSizeOperation.Status == AsyncOperationStatus.Succeeded)
//                         {
//                             long fileSizeBytes = fileSizeOperation.Result;
//                             float fileSizeKB = fileSizeBytes / 1024.0f;
//                             fileSizeText.text = "File size: " + fileSizeKB.ToString("F2") + " KB";
//                         }
//                         else
//                         {
//                             fileSizeText.text = "Failed to get file size!";
//                         }
//                     };
//                 }
//                 else
//                 {
//                     fileSizeText.text = "Failed to load material!";
//                 }
//             };
//         }
//         else
//         {
//             Debug.Log("Failed to clear Addressables cache.");
//         }
//     };
// }

        #endregion


       
        


        public void Load(AddressableData addressableData)
     {
         
         AsyncOperationHandle<GameObject> handleLoadAsset = Addressables.LoadAssetAsync<GameObject>(addressableData.referenceObject);
         handleLoadAsset.Completed += loadAssetOperation =>
         {
             if (loadAssetOperation.Status == AsyncOperationStatus.Succeeded)
             {
                 GameObject loadObject = loadAssetOperation.Result;
                 foreach (var currentObject in objectListPosition)
                 {
                     Instantiate(loadObject,currentObject.transform.position,Quaternion.identity,currentObject);
                 }
             }
         };
    
        
     }
    };
}
    
