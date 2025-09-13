
using System;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;
    float timer;
    [SerializeField] float intervalo;
    [SerializeField] GameObject[] Spawns;
    bool up;
    float originalIntervalo;
    [SerializeField] TextMeshProUGUI timerText;

    void Start()
    {
        originalIntervalo = intervalo;
        Invoke("Spawn", intervalo);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "Tiempo: " + Math.Round(timer, 2);
    }

    void Spawn()
    {
        int i = UnityEngine.Random.Range(0, Spawns.Length);

        int random = UnityEngine.Random.Range(0, Enemies.Length);

        GameObject temporalEnemy = Instantiate(Enemies[random], Spawns[i].transform.position, Quaternion.identity);
        temporalEnemy.GetComponent<Rigidbody2D>().linearVelocityX = -5 - (timer / 5);
        float r = UnityEngine.Random.Range(1, 3);
        intervalo = r;
        Invoke("Spawn", intervalo);
        up = !up;


    }
}
