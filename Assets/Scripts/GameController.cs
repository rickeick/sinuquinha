using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject taco;
    private Transform bolas;
    private List<Rigidbody> lista;
    private bool trocar = true;
    public int jogador = 1;
    public int[] pontuacao;

    void Start()
    {
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
    }

    bool pararam()
    {
        bool teste = true;
        List<Rigidbody> remover = new List<Rigidbody>();
        foreach (Rigidbody rb in lista)
        {
            if (rb.isKinematic) 
            {
                remover.Add(rb);
            }
            if (rb.velocity.magnitude > 1f)
            {
                teste = false;
            }
        }
        if (remover.Count > 0)
        {
            trocar = false;
            pontuacao[jogador] += remover.Count;
            foreach (Rigidbody rb in remover)
            {
                lista.Remove(rb);
            }
        }
        return teste;
    }
}
