using jumpAndLearn.calculation;
using jumpAndLearn.terrain;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance
    { get { return instance; } }

    [SerializeField]
    private int level;

    [SerializeField]
    private TextMeshProUGUI display;

    private void Start()
    {
        instance = this;
    }

    public void DisplayText(string text)
    {
        display.text = text;
    }

    public void EndLevel()
    {
        Platform.Platforms.Clear();
        if (SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Level" + (level + 1)+".unity") != -1)
            SceneManager.LoadSceneAsync("Level" + (level + 1));
        else
            SceneManager.LoadSceneAsync("Hub");
    }

    public void WinLevel()
    {
        StopCoroutine("timeUntilGameOver");
        DisplayText("Bravo ! Vous avez terminé ce niveau !");
    }

    public void LoseLevel()
    {
        Platform.CurrentPlatform.DestroyNextPlatforms();
        StartCoroutine("Lose");
        DisplayText("Raté ! La réponse était : "+CalculationManager.Instance.Calculation.Answer);
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void RestartTimer()
    {
        StopCoroutine("timeUntilGameOver");
        StartCoroutine("timeUntilGameOver");
    }

    //Timer if player don't jump
    IEnumerator timeUntilGameOver()
    {
        yield return new WaitForSeconds(10);
        LoseLevel();
    }

}
