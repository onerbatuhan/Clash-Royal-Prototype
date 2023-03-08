using TMPro;
using UnityEngine;

namespace Game
{
    public class GameTimer : MonoBehaviour
    {
        public float totalTime = 60f; // Toplam süre
        private float _remainingTime; // Geri kalan süre
        public TextMeshProUGUI countdownText; // TextMeshPro UI elemanı
        private bool _canReturn = true;
        public bool isGameFinish;
        private void Start()
        {
            _remainingTime = totalTime;
        }

        private void Update()
        {
           if(!GameStartEvent.Instance.isGamePlay) return;
            _remainingTime -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(_remainingTime % 60);
            if (seconds >= 0)
            {
                countdownText.text = seconds.ToString();
                
            }
            else if(_canReturn)
            {
                isGameFinish = true;
                _canReturn = false;
                GameFinishEvent.Instance.GameStateFilter();
                Time.timeScale = 0f;
                
            }
            
        }
    }
}
