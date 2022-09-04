using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	private int score;
	public Text scoreText;
	
	public GameObject restartGameObject;
	private bool restart;
	public GameObject gameOverGameObject;
	private bool gameOver;
	
	// Start is called before the first frame update
    void Start()
    {
        UpdateSpawnValues();
		restart = false;
		restartGameObject.SetActive(false);
		gameOver = false;
		gameOverGameObject.SetActive(false);
		score = 0;
		UpdateScore();
		StartCoroutine (SpawnWaves());
    }

	void UpdateSpawnValues()
	{
		Vector2 half = Utils.GetHalfDimensionsInWorldUnits();
		spawnValues = new Vector3(half.x-0.7f,0f,half.y+37f);
		Debug.Log(half);
	}
	
	void Update()
	{
		if(restart && Input.GetKeyDown(KeyCode.R))
		{
			Restart();
		}
	}
	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
    // Update is called once per frame
    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
		while(true)
		{
			for(int i=0; i<hazardCount; i++)
				{
					GameObject hazard = hazards[Random.Range(0,hazards.Length)];
					Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x,spawnValues.x),spawnValues.y,spawnValues.z);
					Instantiate(hazard, spawnPosition, Quaternion.identity);
					yield return new WaitForSeconds(spawnWait);
				}
				yield return new WaitForSeconds(waveWait);
				
				if(gameOver)
				{
					restartGameObject.SetActive(true);
					restart = true;
					break;
				}
		}
		
    }
	
	public void AddScore(int value)
	{
		score += value;
		UpdateScore();
	}
	
	void UpdateScore()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver()
	{
		gameOverGameObject.SetActive(true);
		gameOver = true;
	}
}

