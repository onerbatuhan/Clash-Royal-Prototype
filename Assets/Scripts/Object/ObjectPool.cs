using System;
using System.Collections.Generic;
using Photon.Pun;
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
        private bool _canReturn;
        void Start()
        {
            _canReturn = true;
        }

        private void LateUpdate()
        {
            //PhotonNetwork.IsMasterClient = Odayı kuran ya da odaya ilk giren kişidir.
            //Ortak kullanımda olacak objeler ya da diğer veriler var ise, masterClient yapacak bu işleri.
            //Diğer client'ların her birinin yapmasına gerek olmayan işler için. Örneğin objetPool.
            if (!PhotonNetwork.IsMasterClient || !PhotonNetwork.IsConnectedAndReady || !_canReturn) return;
            _canReturn = false;
            _poolEffectObjects = new Queue<GameObject>();
            _clonePool = GameObject.Find("<--ClonePool-->");
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = PhotonNetwork.Instantiate(objectToPool.name, Vector3.zero, Quaternion.identity);
                Debug.Log(obj);
                obj.transform.SetParent(_clonePool.transform);
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
                
                GameObject obj = PhotonNetwork.Instantiate(objectToPool.name, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(_clonePool.transform);
                obj.SetActive(true);
                if (_poolEffectObjects != null) _poolEffectObjects.Enqueue(obj);
                return obj;
            }
           
            
        }
    }
}

