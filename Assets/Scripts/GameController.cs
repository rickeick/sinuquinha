using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text player1Text; // Reference for Player 1's text
    private GameObject taco;
    private Transform bolas;
    private List<Rigidbody> lista;
    private bool trocar = true;
    public int[] pontuacao;
    public int jogador;

    void Start()
    {
        jogador = 0;
        pontuacao = new int[2];
        lista = new List<Rigidbody>();
        
        // Encontrar objetos
        taco = GameObject.Find("Taco");
        bolas = GameObject.Find("Bolas").transform;
        player1Text = GameObject.Find("player1Txt").GetComponent<TMP_Text>();
        
        if (taco == null || bolas == null || player1Text == null)
        {
            Debug.LogError("Objeto Taco, Bolas ou texto não encontrado.");
        }
        
        // Popula a lista de rigidbodies
        foreach (Transform filho in bolas)
        {
            Rigidbody rb = filho.GetComponent<Rigidbody>();
            if (rb != null)
            {
                lista.Add(rb);
            }
            else
            {
                Debug.LogError($"Rigidbody não encontrado em {filho.name}");
            }
        }
    }

    void Update()
    {
        if (player1Text != null)
        {
            if(jogador == 0)
                player1Text.text = $"=>PLAYER1:{pontuacao[0]} PLAYER2:{pontuacao[1]}";
            else
                player1Text.text = $"PLAYER1:{pontuacao[0]} =>PLAYER2:{pontuacao[1]}";

        }
        if (pararam() && Input.GetKeyUp(KeyCode.W))
        {
            if (taco != null)
            {
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
        }
        
        if (pontuacao[0] >= 8)
        {
            Debug.Log("Jogador Número 1 Ganhou!");
            SceneManager.LoadScene("Player1Win");
        }
        if (pontuacao[1] >= 8)
        {
            Debug.Log("Jogador Número 2 Ganhou!");
            SceneManager.LoadScene("Player2Win");
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
                    rb.isKinematic = false;
                    trocar = true;
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
