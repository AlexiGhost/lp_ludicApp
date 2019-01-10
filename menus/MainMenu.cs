using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace jumpAndLearn.menus
{
    public class MainMenu : MonoBehaviour
    {

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void PlayGame()
        {
            SceneManager.LoadSceneAsync("Hub");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
