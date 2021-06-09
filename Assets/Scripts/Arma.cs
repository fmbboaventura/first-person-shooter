using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] private int dano = 10;

    [SerializeField] private float alcance = 100;

    [SerializeField] private float taxaDeDisparo = 10;

    private float tempoProximoDisparo = 0;

    [SerializeField] private Camera cam;

    [SerializeField] private ParticleSystem flash;

    [SerializeField] private GameObject impactoPrefab;

    [SerializeField] private GameObject impactoSanguePrefab;

    [SerializeField] private AudioSource somDisparo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > tempoProximoDisparo) {
            tempoProximoDisparo = Time.time + 1/taxaDeDisparo;

            flash.Play();
            somDisparo.Play();
            
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, alcance))
            {
                LifeManager lifeManager = hit.transform.GetComponent<LifeManager>();

                if (lifeManager) lifeManager.CausarDano(dano);
                
                GameObject impacto = (hit.transform.tag == "Inimigo") ? 
                    impacto = Instantiate(impactoSanguePrefab, hit.point, Quaternion.LookRotation(hit.normal)) : 
                    impacto = Instantiate(impactoPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                
                impacto.transform.SetParent(hit.transform);
                Destroy(impacto, 1f);
            }
        }
    }
}
