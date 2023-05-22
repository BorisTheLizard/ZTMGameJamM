using UnityEngine;

public class CombatMusic : MonoBehaviour
{
    private spawner spawner;
    [SerializeField] AudioSource[] layers;

    void Start()
    {
        spawner = FindObjectOfType<spawner>().GetComponent<spawner>();
        foreach (AudioSource layer in layers)
        {
            layer.mute = true;
        }
        layers[0].mute = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.activeEnemies > 10)
        {
            layers[2].mute = false;
            layers[1].mute = false;
        }
        else if (spawner.activeEnemies > 5)
        {
            layers[2].mute = true;
            layers[1].mute = false;
        }
        else
        {
            layers[2].mute = true;
            layers[1].mute = true;
        }
    }
}
