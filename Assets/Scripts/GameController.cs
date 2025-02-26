using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameObject taco;
    private Transform bolas;
    private List<Rigidbody> lista;

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
        }
    }

    bool pararam()
    {
        foreach (Rigidbody rb in lista)
        {
            if (rb.velocity.magnitude > 1f)
            {
                return false;
            }
        }
        return true;
    }
}
