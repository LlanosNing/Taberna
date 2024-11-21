using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Selected : MonoBehaviour
{
    LayerMask mask;
    public float distancia = 1.5f;

    public Texture2D puntero;
    public GameObject TextDetect;
    GameObject ultimoObjetoRegistrado = null;

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");

        TextDetect.SetActive(false);
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            Deselect();
            SelectedObject(hit.transform);

            if (hit.collider.tag == "Nota")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<InteractiveObject>().ActivarObjeto();
                }
            }
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward) * distancia, Color.red);
            
        }
        else
        {
            Deselect();
        }
    }

    void SelectedObject(Transform transform)
    {
        transform.GetComponent<MeshRenderer>().material.color = Color.gray;
        ultimoObjetoRegistrado = transform.gameObject;
    }

    void Deselect()
    {
        if(ultimoObjetoRegistrado)
        {
            ultimoObjetoRegistrado.GetComponent<Renderer>().material.color = Color.white;
            ultimoObjetoRegistrado = null;
        }
    }

    private void OnGUI()
    {
        Rect rect = new(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
        GUI.DrawTexture(rect, puntero);

        if(ultimoObjetoRegistrado)
        {
            TextDetect.SetActive(true);
        }
        else
        {
            TextDetect.SetActive(false);
        }
    }

}
