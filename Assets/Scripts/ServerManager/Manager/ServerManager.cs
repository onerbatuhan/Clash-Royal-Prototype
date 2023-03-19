using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace ServerManager.Manager
{
    public class ServerManager : MonoBehaviourPunCallbacks
    {
        public TextMeshProUGUI serverMessageLogText;
        public GameObject entrancePanelCanvas;
        public GameObject clientInfoPanelCanvas;
        private void Start()
        {
            //1.Server Connect
            PhotonNetwork.ConnectUsingSettings();
        
            //2. Best(Internet level) server area connect and manuel area connect.
            // PhotonNetwork.ConnectToBestCloudServer(); //Oyuncuyu, en iyi durumda olan sunucuya bağlar.(Bölge olarak)
            // PhotonNetwork.ConnectToRegion("eu"); //Statik olarak parametreye verdiğimiz değerdeki sunucuya bağlar.(Sunucu kısaltmaları Photon sitesinde mevcut.Burada Avrupaya bağlamaya çalışır)
        


            /*
        //2.Lobby Connect
        PhotonNetwork.JoinLobby();

        //3.Room Connect
        PhotonNetwork.JoinRoom("Istanbul");
        
        //4.Random Room Connect
        PhotonNetwork.JoinRandomRoom();
        
        //5.Create Room
        PhotonNetwork.CreateRoom("Izmir", new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},TypedLobby.Default);
        
        //6.Create room and Connect room
        PhotonNetwork.JoinOrCreateRoom("Izmir", new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},TypedLobby.Default);

        //7.Exit lobby
        PhotonNetwork.LeaveLobby();
        
        //8.Exit room
        PhotonNetwork.LeaveRoom();
        */
        }

        public override void OnConnected()
        {
            ServerMessageSetLog("Oyuncu Server'a bağlandı.");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            ServerMessageSetLog("Oyuncunun bağlantısı koptu.");
            // PhotonNetwork.ReconnectAndRejoin(); //Bağlantısı kopan oyuncuyu, son bulunduğu Room'a tekrar bağlar.
            PhotonNetwork.RejoinRoom("Izmır"); //Bağlantısı kopan oyuncuyu, parametre olarak belirttiğimiz odaya bağlar.
        }
    
        public override void OnConnectedToMaster()
        {
            //OnConnected'dan Farkı şu; Ben bu metotta, lobby veya room'a geçiş yapabilecek durumdayım. Diğeri sadece bağlanma durumunu belirtir.
            ServerMessageSetLog("Oyuncu Server'a bağlandı ve lobby veya room'a girmeye hazır durumda.");
            //Lobby'e otomatik aktarıyoruz.
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinedLobby()
        {
        
            //Lobby bağlantı sorgusu(Oyuncu room'a bağlandığı zaman lobby'den çıkış yapacağı için, room'dayken bu değer false döner.
            if (PhotonNetwork.InLobby)
            {
          
            
                Debug.Log("Oyuncu Lobby'de");
                //Oyuncunun ismi, eğer photon sistemine kayıt ettirmek istiyorsan girebilirsin.
                // PhotonNetwork.NickName = "Batuhan";
            }
            else
            {
                Debug.Log("Oyuncu Lobby'de değil.");
            }
        
            ServerMessageSetLog("Oyuncu lobby'e bağlandı.");
            //Direkt oda kurar ve odaya giriş işlemi yapar.
            // PhotonNetwork.JoinOrCreateRoom("Izmir", new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},TypedLobby.Default);
        
            //Oda kurar.
            // PhotonNetwork.CreateRoom("Izmir", new RoomOptions{MaxPlayers = 2,IsOpen = true,IsVisible = true},TypedLobby.Default);
        
       
        }
    
        public override void OnJoinedRoom()
        {
            //
            // Camera.main.transform.position = new Vector3(39f, 73, 0);
            // Camera.main.transform.rotation = new Quaternion(61.9999962f, 270, 0, 0);
       
            entrancePanelCanvas.SetActive(false); //Oda kur panelini kapa.
            clientInfoPanelCanvas.SetActive(true); //client ınfo paneli aç.
            ServerMessageSetLog("Oyuncu room'a bağlandı.");
            Debug.Log(PhotonNetwork.LocalPlayer.NickName); //LocalPlayer'da oyuncunun yani client sahibinin bilgilerini alırsın.
            Debug.Log(PhotonNetwork.CountOfPlayers); //Server'da ki oyuncu sayısı
            Debug.Log(PhotonNetwork.CountOfRooms); //Server'da ki oda sayısı
            Debug.Log(PhotonNetwork.CountOfPlayersOnMaster); //Lobby'deki oyuncu sayısı
            Debug.Log(PhotonNetwork.CountOfPlayersInRooms); //Odalardaki oyuncu sayısı
            Debug.Log(PhotonNetwork.CurrentRoom); //Odamızın bilgilerini verir.
            Debug.Log(PhotonNetwork.CurrentLobby);//Lobby'mizin bilgilerini verir.
            Debug.Log(PhotonNetwork.CloudRegion); //Hangi bölgenin server'ına bağlıyız o bilgiyi verir.
      
        }

    
        //Return mesaj metotları.
    
   
        //Lobby'den çıkılır ise;
        public override void OnLeftLobby()
        {
            ServerMessageSetLog("Oyuncu lobby'den ayrıldı.");
        }

        //Room'dan çıkılır ise;
        public override void OnLeftRoom()
        {
            ServerMessageSetLog("Oyuncu Room'dan ayrıldı.");
        }
    
    
        //Odaya girilemez ise;
        public override void OnJoinRoomFailed(short returnCode,string message)
        {
            ServerMessageSetLog("Odaya girilemedi. " + message + " Hata kodu : " + returnCode);

        }
    
        //Random Join'lenen odaya giremez ise;
        public override void OnJoinRandomFailed(short returnCode,string message)
        {
            ServerMessageSetLog("Random odaya girilemedi. "+message + " Hata kodu : "+ returnCode);
        
        }
    
        //Oda kurulamaz ise;
        public override void OnCreateRoomFailed(short returnCode,string message)
        {
            ServerMessageSetLog("Oda kurulamadı. "+message + " Hata kodu : "+ returnCode);
        }


        private void ServerMessageSetLog(string messageText)
        {
            serverMessageLogText.text = messageText;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                PhotonNetwork.LeaveRoom(); //Odadan çıkarır.
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                PhotonNetwork.LeaveLobby(); //Lobby'den çıkarır.
            }

            //Server bağlantı sorgusu
            if (PhotonNetwork.IsConnected)
            {
                Debug.Log("Oyuncu Server'a bağlı");
            }
            else
            {
                Debug.Log("Oyuncu Server'a bağlı değil");
            }
        
        
            if (PhotonNetwork.IsConnectedAndReady)
            {
                Debug.Log("Oyuncu Server'a bağlı ve diğer işlemlere hazır.");
            }
            else
            {
                Debug.Log("Oyuncu Server'a bağlı değil ve diğer işlemlere hazır değil");
            }


            //Odayı kuran oyuncu ben miyim?
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("Oyunun kurucusuyum");
            }
            else
            {
                Debug.Log("Oyunun kurucus ben değilim.");
            }

            if (PhotonNetwork.InRoom)
            {
                Debug.Log("Oyuncu Room'da");
            }
            else
            {
                Debug.Log("Oyuncu Room'da değil.");
            }
     
        }


        public void DisconnectButtonClicked()
        {
            PhotonNetwork.Disconnect();
        
        }
        public void ReConnectButtonClicked()
        {
            PhotonNetwork.Reconnect();
        }
    
        public void StatisticsDeleteButtonClicked()
        {
            PhotonNetwork.NetworkStatisticsReset();
        }
    
        public void StatisticsAddButtonClicked()
        {
            PhotonNetwork.NetworkStatisticsEnabled = true;
            Debug.Log(PhotonNetwork.NetworkStatisticsToString());
        }
    
        public void PingButtonClicked()
        {
            ServerMessageSetLog(PhotonNetwork.GetPing().ToString());
        }
    
    }
}
