using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacoController : MonoBehaviour
{
    private Rigidbody bola;
    private Vector3 posicaoInicial;
    private float velocidade = 15f;
    private float distancia = 25f;
    public float intensidade = 0f;
    public AudioSource audioSource; // som de taco na bola branca

    void Start()
    {
        posicaoInicial = transform.localPosition;
        GameObject objeto = GameObject.Find("0");
        if (objeto != null)
        {
            bola = objeto.GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (transform.localPosition.z < posicaoInicial.z + distancia)
            {
                transform.Translate(Vector3.back * velocidade * Time.deltaTime);
                intensidade = Mathf.Abs(posicaoInicial.z - transform.localPosition.z);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            gameObject.SetActive(false);
            while (transform.localPosition.z > posicaoInicial.z - distancia) {
                transform.Translate(Vector3.forward * velocidade * 10f * Time.deltaTime);
            }
            bola.AddForce(transform.forward.normalized * intensidade, ForceMode.Impulse);
            audioSource.PlayOneShot(audioSource.clip, 1f); // reproduz som
            transform.localPosition = posicaoInicial;
            intensidade = 0f;
        }
    }
}
