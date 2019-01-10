using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace jumpAndLearn.menus
{
    public class GameOver : MonoBehaviour
    {

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void ReplayGame()
        {
            SceneManager.LoadScene("Level1");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Hub()
        {
            SceneManager.LoadSceneAsync("Hub");
        }

        public void QuitGame()
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    }
}
