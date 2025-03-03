using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject taco;
    private Transform bolas;
    private List<Rigidbody> lista;
    private bool trocar = true;
    public int jogador = 0;
    public int[] pontuacao;

    void Start()
    {
        pontuacao = new int[2];
        lista = new List<Rigidbody>();
        taco = GameObject.Find("Taco");
        bolas = GameObject.Find("Bolas").transform;
        foreach (Transform filho in bolas)
        {
            Rigidbody rb = filho.GetComponent<Rigidbody>();
            if (rb != null)
            {
                lista.Add(rb);
            }
        }
    }

    void Update()
    {
        if (pararam() && Input.GetKeyUp(KeyCode.W))
        {
            taco.SetActive(true);
            if (trocar)
            {
                jogador = jogador ^ 1;
            }
            trocar = true;
        }
        if (lista.Count == 1) {
            if (pontuacao[0] < pontuacao[1]) {
                Debug.Log("Jogador Número 1 Ganhou!");
            } else {
                Debug.Log("Jogador Número 2 Ganhou!");
            }
            Time.timeScale = 0f;
        }
    }

    bool pararam()
    {
        bool teste = true;
        List<Rigidbody> acertos = new List<Rigidbody>();
        foreach (Rigidbody rb in lista)
        {
            if (rb.isKinematic) 
            {
                acertos.Add(rb);
            }
            if (rb.velocity.magnitude > 1f)
            {
                teste = false;
            }
        }
        if (acertos.Count > 0)
        {
            trocar = false;
            foreach (Rigidbody rb in acertos)
            {
                if (rb.gameObject.name == "0")
                {
                    trocar = true;
                    rb.isKinematic = false;
                }
                else
                {
                    lista.Remove(rb);
                    pontuacao[jogador] += 1;
                }
            }
        }
        return teste;
    }
}
