using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ServerManager.ButtonEvents
{
    public class ServerButtonEvents : MonoBehaviour
    {
        public InputField clientName;
        public InputField roomName;


        public void CreateRoomAndEnter()
        {
            PhotonNetwork.NickName = clientName.text;
            PhotonNetwork.JoinOrCreateRoom(roomName.text, new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},TypedLobby.Default);
        }

        public void RandomRoomEnter()
        {
            PhotonNetwork.NickName = clientName.text;
            PhotonNetwork.JoinRandomRoom();
        }
    }
}
