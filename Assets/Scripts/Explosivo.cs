using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosivo : MonoBehaviour
{
    [SerializeField] private GameObject explosaoPrefab;

    public void Explodir() {
        Destroy(Instantiate(explosaoPrefab, transform.position, transform.rotation), 2);

        Destroy(gameObject);
    }
}
