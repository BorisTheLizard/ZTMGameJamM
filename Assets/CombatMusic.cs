using UnityEngine;

public class CombatMusic : MonoBehaviour
{
    [SerializeField] AudioSource[] layers;
    [SerializeField] int addLayer2;
    [SerializeField] int addLayer3;
    [Range(0, 1)]
    [SerializeField] float fadeRate;

    private spawner spawner;
    bool[] layerStatus;

    void Start()
    {
        spawner = FindObjectOfType<spawner>().GetComponent<spawner>();
        foreach (AudioSource layer in layers)
        {
            layer.volume = 0;
        }
        layers[0].volume = 1;

        int layersSize = layers.Length;
        layerStatus = new bool[layersSize];
        layerStatus[0] = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.activeEnemies > addLayer3)
        {

            //layers[2].mute = false;
            //layers[1].mute = false;
            if (layers[2].volume < 1)
            { layers[2].volume += Time.deltaTime * fadeRate; }
            if (layers[1].volume < 1)
                layers[1].volume += Time.deltaTime * fadeRate;

        }
        else if (spawner.activeEnemies > addLayer2)
        {
            if (layers[2].volume > 0)
            { layers[2].volume -= Time.deltaTime * fadeRate; }
            if (layers[1].volume < 1)
                layers[1].volume += Time.deltaTime * fadeRate;
        }
        else
        {
            if (layers[2].volume > 0)
            { layers[2].volume -= Time.deltaTime * fadeRate; }
            if (layers[1].volume > 0)
            { layers[1].volume -= Time.deltaTime * fadeRate; }
        }
    }
}
