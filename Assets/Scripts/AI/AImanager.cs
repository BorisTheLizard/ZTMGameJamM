using System.Collections.Generic;
using UnityEngine;


public class AImanager : MonoBehaviour
{
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

    public void MakeAgentsCircleTarget()
    {
        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].gameObject.activeSelf != false)
            {
				if (Units[i].GetComponent<enemyAI>().currentState == "IDLE")
				{
                    Units[i].GoToPoint(new Vector3(
Target.position.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / Units.Count),
Target.position.y,
Target.position.z + RadiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i / Units.Count)));
                }
            }
        }
    }
}