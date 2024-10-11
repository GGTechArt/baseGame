using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;

    GameObject currMenuPanel;

    private void Start()
    {
        currMenuPanel = mainMenuPanel;
    }

    public void ChangePanel(GameObject newPanel)
    {
        if (newPanel != null)
        {
            if (currMenuPanel != null)
            {
                currMenuPanel.SetActive(false);
            }
            currMenuPanel = newPanel;
            currMenuPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("El panel de destino es nulo");
        }
    }

    public void ChangeScene(int indexScene){
        SceneManager.LoadScene(indexScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
