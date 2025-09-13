using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI puntajeText;
    [SerializeField] int puntaje = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        puntajeText.text = "Puntos: " + puntaje;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetPuntaje()
    {
        return Mathf.RoundToInt(puntaje);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            puntaje += 1;
            puntajeText.text = "Puntos: " + puntaje;
        }
    }
}
