using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] public float velocidadeMaxima = 12f;

    [SerializeField] public float aceleracao = 12f;

    private float velocidade = 0f;

    [SerializeField] private float gravidade = -9.81f;

    private Vector3 velocidadeG;

    private CharacterController controller;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask quemEhOChao;

    [SerializeField] private float distanciaDoChao = 0.4f;

    private bool estaNoChao;

    private Transform alvo;

    private Animator animator;

    public bool atacando = false;
    
    private bool morreu = false;

    [SerializeField] private GameObject efeitoPrefab;

    private float alcance =3f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        alvo = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        animator.SetBool("Andando", true);
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics.CheckSphere(groundCheck.position, distanciaDoChao, quemEhOChao);

        if (estaNoChao && velocidadeG.y < 0) {
            velocidadeG.y = -2;
        }

        velocidade = Mathf.Clamp(velocidade + aceleracao * Time.deltaTime, 0, velocidadeMaxima);

        //  se não estiver atacando ou morto

        if (alvo != null && !atacando && !morreu) {
            // faz o inimigo olhar para o jogador, ignora o eixo y
            transform.LookAt(new Vector3(alvo.position.x, 0, alvo.position.z));

            // move o inimigo na direcao que ele está apontando
            controller.Move(transform.forward * velocidade * Time.deltaTime);
            
            // checa se o alvo está no alcance e ataca
            if ((alvo.position - transform.position).magnitude <= alcance) {
                atacando = true;
                animator.SetTrigger("Atacar");
            }
        }




        velocidadeG.y += gravidade * Time.deltaTime;

        controller.Move(velocidadeG * Time.deltaTime);
    }

    public void Morrer() {
        morreu = true;
        GetComponent<LifeManager>().enabled = false;
        animator.SetTrigger("Morrer");
        StartCoroutine(InstanciarEfeito());
    }

    IEnumerator InstanciarEfeito()
    {
        yield return new WaitForSeconds(3);
        Destroy(Instantiate(efeitoPrefab, transform.position, transform.rotation), 3f);
        Destroy(gameObject);
    }
}
