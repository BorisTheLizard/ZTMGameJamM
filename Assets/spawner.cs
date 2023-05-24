using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class spawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoin;
    public float duration = 180.0f;
    public Text timerText;
    private float elapsedTime = 0.0f;

    //spawner logic
    public string currentWave = "1st";
    public float timeToSpawn = 2f;
    public float timeToSpawn1 = 1.5f;
    public float timeToSpawn2 = 0.7f;
    public bool spawnerAtDuty = false;
    AImanager enemyList;

    //Emp Charged
    public bool chargedEMP = false;
    [SerializeField] GameObject EMPchargedButtonTrigger;
    [SerializeField] GameObject inactiveButtonMat;
    [SerializeField] Material ActiveButtonMat;
    [SerializeField] GameObject lineRenderer;
    [SerializeField] GameObject chargedParts;
    AudioSource audioSource;
    [SerializeField] GameObject EMPchargedText;

    public int activeEnemies;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
                StartCoroutine(getEnemyOnTime(timeToSpawn));
            }
            if (remainingTime <= 120)
            {
                currentWave = "2nd";
                StopAllCoroutines();
                spawnerAtDuty = false;
            }
        }
        else if (currentWave == "2nd")
        {
            if (!spawnerAtDuty)
            {
                StartCoroutine(getEnemyOnTime(timeToSpawn1));
            }
            if (remainingTime <= 60)
            {
                currentWave = "3rd";
                StopAllCoroutines();
                spawnerAtDuty = false;
            }
        }
        else if (currentWave == "3rd")
        {
            if (!spawnerAtDuty)
            {
                StartCoroutine(getEnemyOnTime(timeToSpawn2));
            }
        }


        if (remainingTime <= 0.0f)
        {
			if (!chargedEMP)
			{
                chargedEMP = true;
                inactiveButtonMat.GetComponent<MeshRenderer>().material = ActiveButtonMat;
                EMPchargedButtonTrigger.SetActive(true);
                EMPchargedText.SetActive(true);
                audioSource.Play();
                lineRenderer.SetActive(false);
                chargedParts.SetActive(true);
            }
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
            activeEnemies = 0;
            for (int i = 0; i < enemyList.Units.Count; i++)
            {
                if (enemyList.Units[i].gameObject.activeSelf == true)
                {
                    activeEnemies++;
                }
            }
        }
    }
    IEnumerator getEnemyOnTime(float spawnTime)
    {
        spawnerAtDuty = true;
        yield return new WaitForSeconds(spawnTime);
        enemyList.MakeAgentsCircleTarget();
        spawnEnemy();
        spawnerAtDuty = false;
    }
}
