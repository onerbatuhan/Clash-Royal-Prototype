using System;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace ServerManager.CanvasEvents
{
    public class ClientInfo : MonoBehaviour
    {
        public TextMeshProUGUI clientNameText;
        private void Start()
        {
            clientNameText.text = PhotonNetwork.NickName;

        }
    }
}
