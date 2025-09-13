using TMPro;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs por carril")]
    [SerializeField] private GameObject[] topPrefabs;    
    [SerializeField] private GameObject[] bottomPrefabs; 

    [Header("Puntos de spawn")]
    [SerializeField] private Transform spawnArriba;   
    [SerializeField] private Transform spawnAbajo;  

    [Header("Velocidad de spawn")]
    [SerializeField] private float intervaloMin = 1f;
    [SerializeField] private float intervaloMax = 3f;
    [Range(0f, 1f)][SerializeField] private float probTop = 0.5f; //Prob es probabilidad, osea la chance de que aparezca el objeto arriba 

    [Header("Velocidad")]
    [SerializeField] private float velocidadBase = -5f;
    [SerializeField] private float dificultadDivisor = 5f;

    [Header("Tiempo!!!")]
    [SerializeField] private TextMeshProUGUI timerText;

    float timer;

    void Start()
    {
        if (intervaloMin < 0.01f) intervaloMin = 0.01f;
        if (intervaloMax < intervaloMin) intervaloMax = intervaloMin;

        StartCoroutine(RutinaDeSpawn());
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timerText) timerText.text = "Tiempo: " + Mathf.Round(timer * 100f) / 100f;
    }

    IEnumerator RutinaDeSpawn()
    {
        while (true)
        {
            bool spawnTop = Random.value < probTop;
            GameObject[] grupo = spawnTop ? topPrefabs : bottomPrefabs;

            if (grupo == null || grupo.Length == 0) { yield return new WaitForSeconds(intervaloMax); continue; }
            GameObject prefab = grupo[Random.Range(0, grupo.Length)];

            Transform origen = PerteneceAlGrupo(prefab, bottomPrefabs) ? spawnAbajo : spawnArriba;

            if (!origen) { yield return new WaitForSeconds(intervaloMax); continue; }

            GameObject go = Instantiate(prefab, origen.position, Quaternion.identity);
            if (go.TryGetComponent<Rigidbody2D>(out var rb))
            {
                float velocidadX = velocidadBase - (timer / dificultadDivisor);
                rb.linearVelocity = new Vector2(velocidadX, rb.linearVelocity.y);
            }

            yield return new WaitForSeconds(Random.Range(intervaloMin, intervaloMax));
        }
    }

    bool PerteneceAlGrupo(GameObject x, GameObject[] arr)
    {
        if (!x || arr == null) return false;
        for (int i = 0; i < arr.Length; i++)
            if (arr[i] == x) return true;
        return false;
    }
}

