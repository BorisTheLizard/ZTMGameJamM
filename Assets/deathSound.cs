using UnityEngine;

public class deathSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip[] deathSounds;
    private spawner spwnr;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spwnr = FindObjectOfType<spawner>().GetComponent<spawner>();
    }
    private void OnEnable()
    {
        if (spwnr.enabled)
        {
            audioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
            audioSource.Play();
        }
		else
		{
            audioSource.volume = 0.3f;
            audioSource.clip = deathSounds[Random.Range(0, deathSounds.Length)];
            audioSource.Play();
        }
    }
}
