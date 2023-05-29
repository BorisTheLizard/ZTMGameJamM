using UnityEngine;

public class pickupBullets : MonoBehaviour
{
    public int amount = 3;
    GameObjectPool pool;
    public AudioClip pickupSound;

    private void Start()
    {
        pool = FindObjectOfType<GameObjectPool>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            other.GetComponent<AttackSystem>().bulletsInClip += amount;
            AudioSource audioSource = other.GetComponent<AudioSource>();
            audioSource.PlayOneShot(pickupSound);
            this.gameObject.SetActive(false);
            pool.ReturnObject(this.gameObject);
        }
    }
}
