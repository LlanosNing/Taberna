using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandMesh : MonoBehaviour
{
    public float minDistance = 1f; // Distancia de detecci�n del jugador
    public float lowerVertexSpeed = 0.1f; // Velocidad de hundimiento
    public float maxSinkDistance = 0.5f; // M�xima distancia de hundimiento

    private Vector3[] vertices;
    private Vector3[] originalVertices; // Para recordar la posici�n original de los v�rtices
    private Mesh mesh;
    private Transform playerTransform;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        originalVertices = mesh.vertices; // Guardamos las posiciones originales
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3[] modifiedVertices = new Vector3[vertices.Length];

        // Obtener la direcci�n "abajo" en el espacio local
        Vector3 localDown = transform.InverseTransformDirection(transform.forward);

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldVertex = transform.TransformPoint(vertices[i]); // Convertir a coordenadas globales
            float vertexDistance = Vector3.Distance(worldVertex, playerTransform.position);

            if (vertexDistance < minDistance)
            {
                // Solo hundimos el v�rtice si no ha alcanzado su l�mite de hundimiento
                if (Vector3.Distance(originalVertices[i], vertices[i]) < maxSinkDistance)
                {
                    vertices[i] += localDown * (lowerVertexSpeed * Time.deltaTime);
                }
            }

            modifiedVertices[i] = vertices[i];
        }

        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals(); // Para que la iluminaci�n se vea bien
        mesh.RecalculateBounds();
    }
}