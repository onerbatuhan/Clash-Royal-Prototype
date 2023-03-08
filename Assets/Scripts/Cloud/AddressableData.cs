using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Cloud
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AdressableData")]
    public class AddressableData : ScriptableObject
    {
        public AssetReference referenceObject;
    }
}
