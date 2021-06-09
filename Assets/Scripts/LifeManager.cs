using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LifeManager : MonoBehaviour
{
    private int life = 0;

    [SerializeField]
    private int maxLife = 100;

    public int Life {
        get => life;
        set => life = value;
    }

    public int MaxLife {
        get => maxLife;
    }

    private bool invencivel = false;

    public bool Invencivel
    {
        get => invencivel;
    }

    private float timer;

    [SerializeField] private float tempoDeInvencibilidade = 0;

    [SerializeField]
    private UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        if (life == 0) AddLife(maxLife);
    }

    public void AddLife(int val)
    {
        life = Mathf.Clamp(life + val, 0, maxLife);

        if (life == 0) 
        {
            invencivel = false;
            onDeath.Invoke();
        }
    }

    public void CausarDano(int dano) {
        if (!invencivel && life > 0) 
        {
            AddLife(-dano);
            invencivel = true;
        }
    }

    void Update()
    {
        if (invencivel)
        {
            timer += Time.deltaTime;

            if (timer >= tempoDeInvencibilidade) {
                timer = 0;
                invencivel = false;
            }
        }
    }
}
