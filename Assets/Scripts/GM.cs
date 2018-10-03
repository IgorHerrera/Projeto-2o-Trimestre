using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GM : MonoBehaviour {


	public float yMinLive = -13.9f;

    public Transform spawnPoint;

    public GameObject playerPrefab;

	playerctrl player;

    public float TimeToRespawn = 2f;
	
	public static GM instance = null;

    public float MaxTime = 120f;

    bool timerOn = true;
    float timeLeft;

    public UI ui;

    GameData data = new GameData();

    void Awake( )
    {
        if (instance == null)
        {
            instance = this;
        }
    }
	// Use this for initialization
	void Start () {
		if (player == null)
        {
            RespawnPlayer();
        }
        timeLeft = MaxTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null)
            {
                player = obj.GetComponent<playerctrl>();
            }
        }
        updateTimer();
        DisplayHudData();
	}

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMainMenu(){
        LoadScene("MainMenu");
    }

    public void CloseApp() {
        Application.Quit();
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    void updateTimer() {
        if (timerOn) {
            timeLeft = timeLeft - Time.deltaTime;
            if (timeLeft <= 0f) {
                timeLeft = 0;
                ExpirePlayer();
            }
        }
    }

    void DisplayHudData()
    {
        ui.hud.txtCoinCount.text = "x 0" + data.coinCount;
        ui.hud.txtTimer.text = "Timer: " + timeLeft.ToString("F0");
    }

    public void IncrementCoinCount()
    {
        data.coinCount++;
    }

    public void RespawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    public void ExpirePlayer() {
        if (player != null) {
            Destroy(player.gameObject);
        }
        GameOver();
    }

    void GameOver() {
        ui.gameOver.txtCoinCount.text = "Presentes: " + data.coinCount;
        ui.gameOver.gameOverPanel.SetActive(true);
    }

    public void KillPlayer ()
    {
        if (player != null)
        {
            Destroy(player.gameObject);
            Invoke("RespawnPlayer", TimeToRespawn);
        }
    }
}
