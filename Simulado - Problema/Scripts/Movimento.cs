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

/*-----------------------------------------------------------------*/
// Variaveis para controle das animacoes (nao alterar!)
    bool pulando = false;
    bool noChao = true;
 /*-----------------------------------------------------------------*/
   
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
         flipSprite();
         Animacao();
    }

    void MovimentoForca() // Funcao de caminhada aplicando forca ao objeto
	{
		sentidoHorizontal = Input.GetAxisRaw("Horizontal"); //Pega um valor (1, -1 ou 0) quando o jogador aperta uma das teclas de andar para frente ou para tras	
		rb.AddForce(new Vector2(forca * sentidoHorizontal, 0), ForceMode2D.Impulse);

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
            rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
            pulos--; // Diminui a quantidade de pulos disponiveis toda vez que ele pula
            noChao = false;
        }
    }

    void OnTriggerEnter2D(Collider2D colisao) // Funcao para detectar colisao com o chao
    {
        if (colisao.gameObject.CompareTag("Ground")) {
            pulos = 2; // Restaura a quantidade de pulos quando entra em contato com o chao
            noChao = true;
        }   
    }










/*-----------------------------------------------------------------*/
//NAO ALTERAR OS SEGUINTES METODOS!
    void Animacao() // Funcao que controla as animacoes (NAO ALTERAR!!!!)
    { 
        animator.SetFloat("Velocidade", Mathf.Abs(rb.velocity.x));
        animator.SetInteger("puloDuplo", pulos);
        animator.SetBool("estaPulando", pulando);
        animator.SetBool("noChao", noChao);

         if (rb.velocity.y > 0.01 && noChao == false) {
            pulando = true;
         } else {
            pulando = false;
         }

         if (noChao == true) {
            pulando = false;
         }
    }

    void flipSprite() {
        if (sentidoHorizontal == 1)
		{
			renderizador.flipX = false;
		}

		if (sentidoHorizontal == -1)
		{
			renderizador.flipX = true;
		}
    }
}
