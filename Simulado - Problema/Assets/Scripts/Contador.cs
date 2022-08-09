using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contador : MonoBehaviour
{
    public int pontos;
    // Start is called before the first frame update
    void Start()
    {
        pontos = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void aumentaPonto() {
        pontos = pontos + 1;
    }
}
