using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace jumpAndLearn.menus
{
    public class MainMenuPause : MonoBehaviour
    {

        public bool gameIsPaused = false;

        public GameObject pauseMenuUI;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Cancel"))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
                gameIsPaused = !gameIsPaused;
            }
        }

        void Resume()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }

        void Pause()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }

        public void Hub()
        {
            Resume();
            SceneManager.LoadSceneAsync("Hub");
        }

        public void MenuGame()
        {
            Resume();
            SceneManager.LoadSceneAsync("MainMenu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}