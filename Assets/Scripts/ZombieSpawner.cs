using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombie;

    [SerializeField] private GameObject efeito;

    private float tProximoInimigo = 0;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= tProximoInimigo) {
            InstanciarInimigo();
            tProximoInimigo += 5 + Random.Range(1, 3);
        }
    }

    void InstanciarInimigo() {
        Destroy(Instantiate(efeito, transform.position, transform.rotation), 2f);
        StartCoroutine(InstanciarZombie());
    }

    IEnumerator InstanciarZombie() {
        yield return new WaitForSeconds(.25f);
        Instantiate(zombie, transform.position, transform.rotation);
    }
}
