using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcscript : MonoBehaviour
{
    private Animator animator;
    private bool isBeingHelped = false; // Evita reativações acidentais

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError(" Nenhum Animator encontrado no NPC!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isBeingHelped)
        {
            isBeingHelped = true;
            Debug.Log("⏳ NPC foi tocado! Esperando 3 segundos...");
            StartCoroutine(WaitAndTriggerHelped());
        }
    }

    private IEnumerator WaitAndTriggerHelped()
    {
        yield return new WaitForSeconds(3f); // Espera 3 segundos

        if (animator != null)
        {
            animator.SetTrigger("helped");
            Debug.Log("NPC recebeu ajuda!");
        }

        isBeingHelped = false; // Permite nova ativação no futuro
    }
}
