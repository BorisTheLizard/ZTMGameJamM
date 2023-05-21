using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class spawner : MonoBehaviour
{
	[SerializeField] Transform[] spawnPoin;
    public float duration = 180.0f;
    public Text timerText;
    private float elapsedTime = 0.0f;

    //spawner logic
    public string currentWave = "1st";
    public float timeToSpawn = 3f;
    public bool spawnerAtDuty = false;
    AImanager enemyList;

    private void Awake()
    {
        enemyList = FindObjectOfType<AImanager>();
        int childCount = transform.childCount;
        spawnPoin = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            spawnPoin[i] = transform.GetChild(i).transform;
            spawnPoin[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        float remainingTime = Mathf.Clamp(duration - elapsedTime, 0.0f, duration);
        int minutes = Mathf.FloorToInt(remainingTime / 60.0f);
        int seconds = Mathf.FloorToInt(remainingTime % 60.0f);
        string timerString = string.Format("{0:0}:{1:00}", minutes, seconds);
        timerText.text = timerString;

		if (currentWave == "1st")
		{
			if (!spawnerAtDuty)
			{
                StartCoroutine(getEnemyOnTime());
			}
        }


        if (remainingTime <= 0.0f)
        {
            //End LvL, Emp blasts Impulse and kill robots;
        }
    }
    private void spawnEnemy()
	{
        if (spawnPoin.Length > 0 && enemyList.Units.Count > 0)
        {
            int enemyCount = enemyList.Units.Count;
            int enemyIndex = enemyList.enemiesSpawned % enemyCount;
            GameObject enemyObject = enemyList.Units[enemyIndex].gameObject;

            // Find a random spawn point index
            int spawnIndex = Random.Range(0, spawnPoin.Length);
            Transform spawnPoint = spawnPoin[spawnIndex];

            while (enemyObject.activeSelf)
            {
                // Move to the next enemy object
                enemyList.enemiesSpawned++;
                enemyIndex = enemyList.enemiesSpawned % enemyCount;
                enemyObject = enemyList.Units[enemyIndex].gameObject;

                // If we have looped through all enemy objects, exit the loop
                if (enemyList.enemiesSpawned >= enemyCount)
                    break;
            }

            if (!enemyObject.activeSelf)
            {
                // Set the enemy object's position to the random spawn point
                enemyObject.transform.position = spawnPoint.position;
                enemyObject.SetActive(true);

                // Increment the number of enemies spawned
                enemyList.enemiesSpawned++;
            }
        }
    }
    IEnumerator getEnemyOnTime()
	{
        spawnerAtDuty = true;
        yield return new WaitForSeconds(timeToSpawn);
        enemyList.MakeAgentsCircleTarget();
        spawnEnemy();
        spawnerAtDuty = false;
    }
}
