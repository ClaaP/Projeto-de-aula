using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fim : MonoBehaviour
{
    private GameObject personagem;
    private Contador contador;
    void Start () {
        personagem = GameObject.FindGameObjectWithTag("Player");
        contador = personagem.GetComponent<Contador>();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player") && contador.pontos >= 15) {
            Debug.Log("Venceu!");
        }
    }
}
