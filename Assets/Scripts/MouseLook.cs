using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensibilidadeDoMouse = 100f;

    [SerializeField] private Transform player;

    // Rotacao da camera em torno do seu eixo X
    private float rotacaoX = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadeDoMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadeDoMouse * Time.deltaTime;

        rotacaoX -= mouseY;
        rotacaoX = Mathf.Clamp(rotacaoX, -90, 90);
        transform.localRotation = Quaternion.Euler(rotacaoX, 0, 0);

        player.Rotate(Vector3.up * mouseX);
    }
}
