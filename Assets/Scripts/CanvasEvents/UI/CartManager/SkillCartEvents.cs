using System;
using AI.Manager;
using AI.ScriptTable;
using CanvasEvents.UI.StaminaManager;
using Object;
using Photon.Pun;
using Player;
using Teams.Manager;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace CanvasEvents.UI.CartManager
{
    public class SkillCartEvents : MonoBehaviourPunCallbacks, IPointerDownHandler, IPointerUpHandler
    {
   
   
        private bool _isButtonDown = false;
        public AIData aiData;
        private GameObject _prefab;
        private GameObject _clone; 
        private bool _isClosed;
        private ObjectPool _vfx;

        private void Start()
        {
            _vfx = GameObject.Find("SupportSkillEffectPooler").GetComponent<ObjectPool>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
            _isButtonDown = true;
            _isClosed = true;
            switch (aiData.playerType)
            {
                case AIType.AIPlayerType.Melee:
                    CloneAddPlayer();
                    break;
                case AIType.AIPlayerType.Aerial:
                    CloneAddPlayer();
                    break;
                case AIType.AIPlayerType.Range:
                    CloneAddPlayer();
                    break;
                case AIType.AIPlayerType.Support:
                    SupportSkillUse();
                    break;
                case AIType.AIPlayerType.Tower:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
           
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isButtonDown = false;
            if (_clone == null) return;
            
            _clone.GetComponent<NavMeshAgent>().enabled = true;
            _clone.GetComponent<AIController>().enabled = true;
            _clone = null;




        }

        void Update()
        {
            if (!_isButtonDown) return;
            if (_clone == null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
        
            if (Physics.Raycast(ray, out hit))
            {
                Transform hitTransform = hit.transform;
                if (hitTransform == null) {
                    OnPointerUp(null); // Hedef yok, OnPointerUp metodunu çağır
                    return;
                }
                Vector3 hitPointWorld = hit.point;
                _clone.transform.position = hitPointWorld;
            }
        }
   
    
        //Bu metotlar bir event Script olacaklar.
        private void CloneAddPlayer()
        {
            if (StaminaController.Instance.staminaCount < aiData.stamina) return;
            StaminaController.Instance.StaminaUse(aiData);
            _prefab = aiData.characterObject;
            //Transform zero (Simdilik böyle, sonra düzeltiriz.)
            _clone = PhotonNetwork.Instantiate(_prefab.name,Vector3.zero, Quaternion.identity);
            TeamController.Instance.allPlayer.Add(_clone);

        }

        private void SupportSkillUse()
        {
            if (StaminaController.Instance.staminaCount < aiData.stamina) return;
            StaminaController.Instance.StaminaUse(aiData);
            int playerID = PlayerController.Instance.playerTeamData.teamID;
            foreach (var aiPlayer in TeamController.Instance.allPlayer)
            {
                if (aiPlayer.GetComponent<Teams.Manager.Team>().teamData.teamID == playerID)
                {
                    if (aiPlayer.GetComponent<AIController>())
                    {
                        aiPlayer.GetComponent<AIController>().playerHealth += aiData.supportHealth;
                        
                        //Effectler MasterClient'a bağlandığında burayı güncellemek üzere kapattık.
                        
                          // GameObject obj = _vfx.GetPooledEffectObject();
                          // obj.transform.localPosition = aiPlayer.transform.localPosition;
                          // obj.transform.SetParent(aiPlayer.transform);
                          

                    }
                   
                }
               
            }
            
        }
    }
}
