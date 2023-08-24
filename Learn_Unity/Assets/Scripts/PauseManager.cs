using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject playPanel;
    // Start is called before the first frame update
    private void Start()
    {
        setPage("play");
    }

    public void setPage(string page)
    {
        switch (page)
        {
            case "pause":
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                playPanel.SetActive(false);

                break;
            case "play":
                Time.timeScale = 1.0f;
                playPanel.SetActive(true);
                pausePanel.SetActive(false);
                break;


                default: break;

        }
    }

    public void Restry()
    {
        SceneManager.LoadScene(1);
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }
}
