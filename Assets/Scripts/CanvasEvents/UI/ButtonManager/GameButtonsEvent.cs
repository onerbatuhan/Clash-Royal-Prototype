using Cloud;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CanvasEvents.UI.ButtonManager
{
    public class GameButtonsEvent : MonoBehaviour
    {
        private bool _isPaused = false;
        public void PauseGame()
        {
            if (!_isPaused)
            {
                Time.timeScale = 0;
                _isPaused = true;
            }
            else
            {
                Time.timeScale = 1;
                _isPaused = false;
            }
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }


        
    }
}
