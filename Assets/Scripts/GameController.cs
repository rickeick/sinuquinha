using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject taco;
    private Transform bolas;
    private List<Rigidbody> lista;
    private bool trocar = true;
    public int jogador;
    public int[] pontuacao;

    void Start()
    {
        jogador = 1;
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
        if (pararam() && Input.GetKeyUp(KeyCode.W)){
            taco.SetActive(true);
            Debug.Log("trocar: " + trocar);
            if (trocar)
            {
                Debug.Log("jogador: " + jogador);
                jogador = jogador ^ 1;
                Debug.Log("jogador da vez: " + jogador);
            }
            trocar = true;
        }
        if (lista.Count == 1) {
            if (pontuacao[0] < pontuacao[1]) {
                Debug.Log("Jogador Número 2 Ganhou!");
                SceneManager.LoadScene("Player2Win");
            } else {
                Debug.Log("Jogador Número 1 Ganhou!");
                SceneManager.LoadScene("Player1Win");
            }
            //Time.timeScale = 0f;
        }
    }

    bool pararam(){
        bool teste = true;
        List<Rigidbody> acertos = new List<Rigidbody>();
        foreach (Rigidbody rb in lista){
            if (rb.isKinematic) {
                acertos.Add(rb);
            }
            if (rb.velocity.magnitude > 1f){
                teste = false;
            }
        }
        if (acertos.Count > 0){
            trocar = false;
            foreach (Rigidbody rb in acertos){
                if (rb.gameObject.name == "0"){
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
