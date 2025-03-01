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
    
    public MG_PlayerController1 pMRef;

    public bool isActive;

    public float timeDuration;
    public float maxtimeDuration = 1f;

    public Material oldMaterialFruit1;

    void Start()
    {
        mask = LayerMask.GetMask("Raycast Detect");

        //TextDetect.SetActive(false);

        pMRef = GameObject.FindWithTag("Player").GetComponent<MG_PlayerController1>();

        maxtimeDuration = 1f;
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distancia, mask))
        {
            Deselect();
            SelectedObject(hit.transform);

            //if (hit.collider.tag == "Nota")
            //{
            //    if (Input.GetKeyDown(KeyCode.E) && isActive == false && timeDuration <= 0)
            //    {
            //        hit.collider.transform.GetComponent<InteractiveObject>().MostrarNota();
            //        isActive = true;
            //        timeDuration = maxtimeDuration;
            //        pMRef.canMove = false;
            //    }

            //    else if(Input.GetKeyDown(KeyCode.E) && isActive == true && timeDuration <= 0)
            //    {
            //        pMRef.canMove = true;
            //        hit.collider.transform.GetComponent<InteractiveObject>().OcultarNota();
            //        timeDuration = maxtimeDuration;
            //        isActive = false;
            //    }
            //}

            if (hit.collider.tag == "Fruit")
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.transform.GetComponent<wildFruits>().ObtenerFruta();
                }
            }

            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward) * distancia, Color.red);
            
        }
        else
        {
            Deselect();
        }

        if(timeDuration > 0)
        {
            timeDuration -= Time.deltaTime;
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
            ultimoObjetoRegistrado.GetComponent<Renderer>().material = oldMaterialFruit1;
            ultimoObjetoRegistrado = null;
        }
    }

    private void OnGUI()
    {
        Rect rect = new(Screen.width / 2, Screen.height / 2, puntero.width, puntero.height);
        GUI.DrawTexture(rect, puntero);

        //if (ultimoObjetoRegistrado)
        //{
        //    TextDetect.SetActive(true);
        //}
        //else
        //{
        //    TextDetect.SetActive(false);
        //}
    }

}
