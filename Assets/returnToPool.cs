using UnityEngine;

public class returnToPool : MonoBehaviour
{
	GameObjectPool pool;
	public float TimeToWait;
	private void Awake()
	{
		pool = FindObjectOfType<GameObjectPool>();
	}
	private void OnEnable()
	{
		Invoke("Return", TimeToWait);
	}
	private void Return()
	{
		pool.ReturnObject(this.gameObject);
	}
}

