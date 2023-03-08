using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDynamic : MonoBehaviour
{
    public class CameraAspectRatio : MonoBehaviour
    {
        public float targetAspectRatio = 16f / 9f; // hedef en-boy oranı
        private float initialAspectRatio; // başlangıçta kullanılan en-boy oranı
        private Camera cam;

        void Start()
        {
            cam = GetComponent<Camera>();
            initialAspectRatio = cam.aspect;
        }

        void Update()
        {
            float currentAspectRatio = (float)Screen.width / Screen.height;
            float ratio = currentAspectRatio / targetAspectRatio;
            cam.fieldOfView *= ratio / initialAspectRatio;
            initialAspectRatio = ratio;
        }
    }
}
