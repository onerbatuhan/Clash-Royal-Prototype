using System.Collections;
using System.Collections.Generic;
using AI.Manager;
using AI.ScriptTable;
using Game;
using Team.ScriptTable;
using Teams.Manager;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Events
{
    public class AISpawnEvent : MonoBehaviour
    {
        
        public List<GameObject> itemPrefabs;
        public List<Transform> spawnPoints;
        public TeamData spawnPlayerTeamData;
        public float spawnRangeTime;

        private void Start()
        {
            
            StartCoroutine("SpanwRandomItemDuration");
            
        }

        private IEnumerator SpanwRandomItemDuration()
        {
            yield return new WaitForSeconds(spawnRangeTime);
            SpawnRandomItem();
        }

        private void SpawnRandomItem()
        {
            float totalWeight = 0f;
            foreach (var item in itemPrefabs)
            {
                totalWeight += item.GetComponent<AIController>().aiData.spawnChance;
            }

            float randomWeight = Random.Range(0f, totalWeight);
            int selectedItemIndex = 0;

            for (int i = 0; i < itemPrefabs.Count; i++)
            {
                randomWeight -= itemPrefabs[i].GetComponent<AIController>().aiData.spawnChance;

                if (randomWeight <= 0)
                {
                    selectedItemIndex = i;
                    break;
                }
            }

            int selectedSpawnIndex = Random.Range(0, spawnPoints.Count);

            GameObject newPlayer = Instantiate(itemPrefabs[selectedItemIndex], spawnPoints[selectedSpawnIndex].position, Quaternion.identity);
            newPlayer.GetComponent<Teams.Manager.Team>().teamData = spawnPlayerTeamData;
            TeamController.Instance.allPlayer.Add(newPlayer);
            newPlayer.GetComponent<NavMeshAgent>().enabled = true;
            newPlayer.GetComponent<AIController>().enabled = true;
            StartCoroutine("SpanwRandomItemDuration");

        }
    }
    }

    

