using Cinemachine;
using UnityEngine;

public class meleHitCollider : MonoBehaviour
{
    [SerializeField] int damageAmount;
    [SerializeField] int HitImpactIndex;
    [SerializeField] int WallHitImpactIndex;
    //[SerializeField] AudioSource[] sFXplayers;
    [SerializeField] AudioClip[] robotHitSounds;
    [SerializeField] AudioClip[] wallHitSounds;
    GameObjectPool pool;
    CinemachineImpulseSource source;
    AudioSource audioSource;

    private void Start()
    {
        pool = FindObjectOfType<GameObjectPool>();
        source = GetComponent<CinemachineImpulseSource>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "enemy")
        {
            other.GetComponent<HealthSystem>().TakeDamage(damageAmount);
            GameObject hitImpact = pool.GetObject(HitImpactIndex);
            source.GenerateImpulse();
            if (hitImpact != null)
            {
                hitImpact.SetActive(false);
                hitImpact.transform.position = transform.position;
                hitImpact.transform.rotation = transform.rotation;
                hitImpact.SetActive(true);
            }
            //audioSource.clip = null;
            audioSource.pitch = Random.Range(0.7f, 1.5f);
            audioSource.clip = robotHitSounds[Random.Range(0, robotHitSounds.Length)];
            audioSource.Play();
            
            /*            for (int i = 0; i < sFXplayers.Length; i++)
                        {
                            if (sFXplayers[i].isPlaying == false)
                            {
                                sFXplayers[i].clip = robotHitSounds[Random.Range(0, robotHitSounds.Length)];
                                sFXplayers[i].Play();
                                break;
                            }
                        }*/

        }
        if (other.tag == "obstacles")
        {
            GameObject hitImpact = pool.GetObject(WallHitImpactIndex);
            source.GenerateImpulse();
            if (hitImpact != null)
            {
                hitImpact.SetActive(false);
                hitImpact.transform.position = transform.position;
                hitImpact.transform.rotation = transform.rotation;
                hitImpact.SetActive(true);
            }
            //audioSource.clip = null;
            audioSource.pitch = Random.Range(0.7f, 1.5f);
            audioSource.clip = wallHitSounds[Random.Range(0, wallHitSounds.Length)];
            audioSource.Play();

            /*            for (int i = 0; i < sFXplayers.Length; i++)
                        {
                            if (sFXplayers[i].isPlaying == false)
                            {
                                sFXplayers[i].clip = wallHitSounds[Random.Range(0, wallHitSounds.Length)];
                                sFXplayers[i].Play();
                                break;
                            }
                        }*/

        }
    }
}
