using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class AImanager : MonoBehaviour
{
    /*private static AImanager _instance;
    public static AImanager Instance
    {
        get
        cha
            return _instance;
    }
    private set
        {
            _instance = value;
        }
    }*/

    public Transform enemyCount;
    public Transform Target;
    public float RadiusAroundTarget = 0.5f;
    public List<enemyAI> Units = new List<enemyAI>();
    public int enemiesSpawned = 0;

    private void Awake()
    {
        enemyAI[] childTransforms = GetComponentsInChildren<enemyAI>(true);

        foreach (enemyAI childTransform in childTransforms)
        {
            if (childTransform != transform)
            {
                Units.Add(childTransform);
            }
        }
    }
    /*	private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.1f);
            MakeAgentsCircleTarget();
        }*/

    public void MakeAgentsCircleTarget()
    {
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].gameObject.activeSelf != false)
            {
                Units[i].GoToPoint(new Vector3(
                Target.position.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / Units.Count),
                Target.position.y,
                Target.position.z + RadiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i / Units.Count)));
            }
        }
    }
}