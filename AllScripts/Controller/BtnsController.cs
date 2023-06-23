using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnsController : MonoBehaviour
{
    public static BtnsController instance;

    [SerializeField]
    private GameObject losePanel, pausePanel;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dead()
    {
        Time.timeScale = 0;
        losePanel.SetActive(true);
        Reload();
    }

    public void Pauser()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void Reload()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.name);
    }

    
}
