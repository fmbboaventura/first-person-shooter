using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidade = 12f;

    [SerializeField] private float gravidade = -9.81f;

    private Vector3 velocidadeG;

    private CharacterController controller;

    [SerializeField] private Transform groundCheck;

    [SerializeField] private LayerMask quemEhOChao;

    [SerializeField] private float distanciaDoChao = 0.4f;

    [SerializeField] private float alturaDoPulo = 5f;

    private bool estaNoChao;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics.CheckSphere(groundCheck.position, distanciaDoChao, quemEhOChao);

        if (estaNoChao && velocidadeG.y < 0) {
            velocidadeG.y = -2;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movimento = transform.forward * z + transform.right * x;

        controller.Move(movimento * velocidade * Time.deltaTime);

        // v^2 = 2gH => v = Sqrt(-2*g*h)
        if (estaNoChao && Input.GetButtonDown("Jump")) {
            velocidadeG.y = Mathf.Sqrt(-2 * gravidade * alturaDoPulo);
        }

        velocidadeG.y += gravidade * Time.deltaTime;

        //  v = gt^2
        controller.Move(velocidadeG * Time.deltaTime);
    }


    void OnControllerColliderHit(ControllerColliderHit hit) {
        float forca = 2;
        
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic) return;

        Vector3 direcaoDaForca = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);

        body.velocity = direcaoDaForca * forca;
    }
}
