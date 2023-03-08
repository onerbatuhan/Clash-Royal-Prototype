using System;
using System.Collections.Generic;
using DesignPattern;
using Player;
using Team.ScriptTable;
using Teams.Manager;
using TMPro;
using UnityEngine;

namespace Game
{
    public class GameFinishEvent : Singleton<GameFinishEvent>
    {
       
        public List<GameObject> firstTeam;
        public List<GameObject> secondTeam;
        private int _winnerTeamID;
        public GameObject gameStateCanvasObject;
        public GameObject gameWinCanvasObject;
        public GameObject gameLoseCanvasObject;
        public TextMeshProUGUI winnerTeamIDText;
        private int _referenceTeamID;

        private void Start()
        {
            _referenceTeamID = TeamController.Instance.allPlayer[0].GetComponent<Teams.Manager.Team>().teamData.teamID;
        }

        public void GameStateFilter()
        {
           
            GameObject referencePlayer = TeamController.Instance.allPlayer[0];
            foreach (var player in TeamController.Instance.allPlayer)
            {
              
                    if (referencePlayer.GetComponent<Teams.Manager.Team>().teamData.teamID == player.GetComponent<Teams.Manager.Team>().teamData.teamID)
                    {
                        if (!firstTeam.Contains(player))
                        {
                            firstTeam.Add(player);
                        }
                        
                    }
                    else
                    {
                        if (!secondTeam.Contains(player))
                        {
                            secondTeam.Add(player);
                        }
                    }

                    

            }
            GameWinnerTeamControl();
        }


       

        public void GameWinnerTeamControl()
        {
            if (firstTeam.Count > 0 && (secondTeam.Count == 0 || firstTeam.Count > secondTeam.Count))
            {
                _winnerTeamID = firstTeam[0].GetComponent<Teams.Manager.Team>().teamData.teamID;
            }
            else if (secondTeam.Count > 0)
            {
                _winnerTeamID = secondTeam[0].GetComponent<Teams.Manager.Team>().teamData.teamID;
            }
            else
            {
                // Her iki takımda da oyuncu yoksa, oyun berabere biter
                // _winnerTeamID = -1;
            }
            GameWinEvent();
        }

        public void GameWinEvent()
        {
            gameStateCanvasObject.SetActive(true);
            // winnerTeamIDText.text = _winnerTeamID.ToString();

            if (PlayerController.Instance.playerTeamData.teamID == _winnerTeamID)
            {
                gameWinCanvasObject.SetActive(true);
            }
            else
            {
                gameLoseCanvasObject.SetActive(true);
            }

            Time.timeScale = 0;
        }
    }
}
