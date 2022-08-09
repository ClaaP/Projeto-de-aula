using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    //VARIAVEIS
	Rigidbody2D rb;
    Animator animator;
    SpriteRenderer renderizador;
	float sentidoHorizontal;  //Se o personagem ira andar para a direita ou esquerda (sendo -1 para a esquerda e 1 para a direita, 0 sem movimento)	
	float forca = 1f;
    float velocidade = 5f;
    int pulos = 2;
    bool noChao = true;

   
    // START - funcao chamada no inicio do programa
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        renderizador = gameObject.GetComponent<SpriteRenderer>();

        sentidoHorizontal = 0;
    }

    //UPDATE - funcao chamada a cada atualizacao de frame
    void Update()
    {
         MovimentoForca();
         Pular();
    }

    void MovimentoForca() // Funcao de caminhada aplicando forca ao objeto
	{
		sentidoHorizontal = Input.GetAxisRaw("Horizontal"); //Pega um valor (1, -1 ou 0) quando o jogador aperta uma das teclas de andar para frente ou para tras	
		rb.AddForce(new Vector2(0, forca * sentidoHorizontal), ForceMode2D.Impulse);

		//Limitando a velocidade
		if(rb.velocity.x > velocidade)
        {
			rb.velocity = new Vector2(velocidade, rb.velocity.y);
        }

		if (rb.velocity.x < velocidade*-1)
		{
			rb.velocity = new Vector2(velocidade * -1, rb.velocity.y);
		}        

        //Parando o personagem
        if(sentidoHorizontal == 0 && noChao == true) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
	}

    void Pular() // Funcao de pulo duplo do personagem
    {
        if(Input.GetButtonDown("Jump") && pulos > 0) {
            rb.AddForce(new Vector2(5f, 0), ForceMode2D.Impulse);
            pulos = pulos + 1;
            noChao = false;
        }
    }

    void OnTriggerEnter2D(Collider2D colisao) // Funcao para detectar colisao com o chao
    {
        if (colisao.gameObject.CompareTag("Ground")) {
            pulos = 2; // Restaura a quantidade de pulos quando entra em contato com o chao
            noChao = false;
        }   
    }
}
