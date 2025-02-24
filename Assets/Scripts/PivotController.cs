using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float rotacao = inputHorizontal * 100f * Time.deltaTime;

        Vector3 rotacaoAtual = transform.eulerAngles;
        rotacaoAtual.y += rotacao;
        transform.eulerAngles = rotacaoAtual;
    }
}
