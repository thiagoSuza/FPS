using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private int zombiesKiled;
    [SerializeField]
    private Text zombiesKilledText;

    private float currentTime;
    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject zombie;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private float timeToSpawn;

    private float currentTimeToRespawn;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        zombiesKiled = 0;
        zombiesKilledText.text = zombiesKiled.ToString();
        currentTimeToRespawn = timeToSpawn;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        SpawnZombie();
    }

    public void AddZombieAcount()
    {
        zombiesKiled +=10;
        zombiesKilledText.text = zombiesKiled.ToString();
    }


    public void Timer()
    {


        currentTime += Time.deltaTime;


        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);


        timerText.text = formattedTime;



        float floatValue = 3.14159f;
        string formattedFloat = floatValue.ToString("0.00");
    }

    public void SpawnZombie()
    {
        
        currentTimeToRespawn -= Time.deltaTime;
        if(currentTimeToRespawn < 0 )
        {
            int aux = Random.Range(0, spawnPoints.Length);
            Instantiate(zombie, spawnPoints[aux].position, spawnPoints[aux].rotation);
            currentTimeToRespawn = timeToSpawn;
        }

    }
}
