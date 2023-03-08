using System.Collections.Generic;
using UnityEngine;

namespace Object
{
    public class ObjectPool : MonoBehaviour
    {
        
        public Queue<GameObject> _poolEffectObjects;
        public GameObject objectToPool;
        public int poolSize; 
        private List<GameObject> pooledObjects; 
        private GameObject _clonePool;
        void Start()
        {
            _poolEffectObjects = new Queue<GameObject>();
            _clonePool = GameObject.Find("<--ClonePool-->");
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(objectToPool, _clonePool.transform, true);
                obj.SetActive(false);
                _poolEffectObjects.Enqueue(obj);
                
            }
        }
        
        
        public GameObject GetPooledEffectObject()
        {
            if (_poolEffectObjects != null)
            {
                GameObject obj = _poolEffectObjects.Dequeue();
                 ParticleSystem currentObject =  obj.GetComponent<ParticleSystem>();
                 currentObject.Stop();
                 currentObject.time = 0;
                 currentObject.Play();
                 obj.SetActive(true);
                _poolEffectObjects.Enqueue(obj);
                return obj;
                
            }
            else
            {
                
                GameObject obj = Instantiate(objectToPool,objectToPool.transform.position,objectToPool.transform.rotation);
                obj.SetActive(true);
                if (_poolEffectObjects != null) _poolEffectObjects.Enqueue(obj);
                return obj;
            }
           
            
        }
    }
}

