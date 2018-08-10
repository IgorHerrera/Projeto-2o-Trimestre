using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {


	public float yMinLive = -13.9f;

    public Transform spawnPoint;

    public GameObject playerPrefab;

	playerctrl player;

    public float TimeToRespawn = 2f;
	
	public static GM instance = null;

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
	}

    public void RespawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
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
