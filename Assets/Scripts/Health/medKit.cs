using UnityEngine;

public class medKit : MonoBehaviour
{
    [SerializeField] int healAmount = 2;
    [SerializeField] AudioClip healSound;
    GameObjectPool pool;

    private void Start()
    {
        pool = FindObjectOfType<GameObjectPool>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            if (other.GetComponent<HealthSystem>().Health < other.GetComponent<HealthSystem>().MaxHealth)
            {
                other.gameObject.GetComponent<HealthSystem>().getHeal(healAmount);
                AudioSource audioSource = other.gameObject.GetComponent<AudioSource>();
                audioSource.PlayOneShot(healSound);
                this.gameObject.SetActive(false);
                pool.ReturnObject(this.gameObject);
            }
        }
    }
}
