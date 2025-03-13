using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandMesh : MonoBehaviour
{
    public float minDistance = 1f; // Distancia de detección del jugador
    public float lowerVertexSpeed = 0.1f; // Velocidad de hundimiento
    public float maxSinkDistance = 0.5f; // Máxima distancia de hundimiento

    private Vector3[] vertices;
    private Vector3[] originalVertices; // Para recordar la posición original de los vértices
    private Mesh mesh;
    private Transform playerTransform;
    UltimatePlayerController playerController;
    WindPush windController;

    public bool isPlayerOnSand;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        originalVertices = mesh.vertices; // Guardamos las posiciones originales

        playerTransform = GameObject.FindWithTag("Player").transform;
        playerController = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
        if(SceneManager.GetActiveScene().name == "Desert_Planet")
        {
            windController = GameObject.FindWithTag("Player").GetComponent<WindPush>();
        }
    }

    void Update()
    {
        Vector3[] modifiedVertices = new Vector3[vertices.Length];

        isPlayerOnSand = false;

        // Obtener la dirección "abajo" en el espacio local
        Vector3 localDown = transform.InverseTransformDirection(transform.forward);

        for (int i = 0; i < vertices.Length; i++)
        {
            if(windController != null)
            {
                if (windController.windDurationCounter > 0)
                {
                    vertices[i] = Vector3.Lerp(vertices[i], originalVertices[i], -lowerVertexSpeed / 4 * Time.deltaTime);
                }
            }

            Vector3 worldVertex = transform.TransformPoint(vertices[i]); // Convertir a coordenadas globales
            float vertexDistance = Vector3.Distance(worldVertex, playerTransform.position);

            if (vertexDistance < minDistance)
            {
                // Solo hundimos el vértice si no ha alcanzado su límite de hundimiento
                if (Vector3.Distance(originalVertices[i], vertices[i]) < maxSinkDistance)
                {
                    vertices[i] += localDown * (lowerVertexSpeed * Time.deltaTime);
                }

                if (vertices[i].y > originalVertices[i].y - maxSinkDistance / 2)
                {
                    isPlayerOnSand = true;
                }
            }

            modifiedVertices[i] = vertices[i];
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals(); // Para que la iluminación se vea bien
        mesh.RecalculateBounds();

        if (isPlayerOnSand)
        {
            playerController.speed = playerController.maxSpeed / 2;
        }
        else
        {
            playerController.speed = playerController.maxSpeed;
        }
    }
}