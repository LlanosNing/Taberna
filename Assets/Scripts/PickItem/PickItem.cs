using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : MonoBehaviour
{
    public bool isObjHeld;

    public Transform holdPosition; // Posici�n donde se sostendr�n los objetos
    public float interactionRange = 5f; // Ampliamos el rango de interacci�n
    public LayerMask interactableLayer; // Capa para los objetos interactuables
    public LayerMask dropPointLayer; // Capa para los puntos de entrega
    public GameObject previewObjectPrefab; // Prefab de previsualizaci�n del objeto
    public Transform raycastOrigin; // Punto de origen del raycast (por ejemplo, la mano del personaje)

    private GameObject heldObject; // Objeto actualmente sostenido por el jugador
    private GameObject previewObject; // Instancia del objeto de previsualizaci�n
    private Transform currentDropPoint; // Punto de entrega m�s cercano
    private Rigidbody heldObjectRb; // Rigidbody del objeto sostenido

    void Update()
    {
        // Detectar interacci�n con objetos al presionar el bot�n
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                TryDropObject();
            }
        }

        // Actualizar la previsualizaci�n
        UpdatePreview();
    }

    void TryPickupObject()
    {
        if (raycastOrigin == null)
        {
            Debug.LogError("raycastOrigin no est� configurado.");
            return;
        }

        // Detectar objetos cercanos usando un rayo esf�rico
        Collider[] hitColliders = Physics.OverlapSphere(raycastOrigin.position, interactionRange, interactableLayer);
        if (hitColliders.Length > 0)
        {
            // Seleccionar el primer objeto v�lido
            GameObject objectToPickup = hitColliders[0].gameObject;

            if (objectToPickup != null)
            {
                PickupObject(objectToPickup);
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        heldObject = obj;
        isObjHeld = true;

        // Desactivar f�sica del objeto mientras se sostiene
        heldObjectRb = heldObject.GetComponent<Rigidbody>();
        if (heldObjectRb != null)
        {
            heldObjectRb.isKinematic = true;
        }

        // Posicionar el objeto en la posici�n de "hold"
        heldObject.transform.position = holdPosition.position;
        heldObject.transform.rotation = holdPosition.rotation;
        heldObject.transform.parent = holdPosition;

        // Crear la previsualizaci�n si no existe ya
        if (previewObjectPrefab != null && previewObject == null)
        {
            previewObject = Instantiate(previewObjectPrefab);
        }

        // Asegurarse de que la previsualizaci�n est� inicialmente desactivada
        if (previewObject != null)
        {
            previewObject.SetActive(false);
        }
    }

    void TryDropObject()
    {
        if (currentDropPoint != null)
        {
            DropObject(currentDropPoint);
        }
        else
        {
            Debug.Log("No puedes soltar el objeto aqu�. Debes estar cerca de un punto de entrega.");
        }
    }

    void DropObject(Transform dropPoint)
    {
        isObjHeld = false;

        if (heldObject != null)
        {
            DropPoint2 dropPointScript = dropPoint.GetComponent<DropPoint2>();
            if (dropPointScript != null && dropPointScript.isOccupied)
            {
                Debug.Log("Ya hay un objeto colocado en este punto.");
                if (previewObject != null)
                {
                    previewObject.SetActive(false);
                }
                return;
            }

            // Reactivar la f�sica del objeto al soltarlo
            if (heldObjectRb != null)
            {
                heldObjectRb.isKinematic = false;
            }

            // Posicionar el objeto en el centro del objeto vac�o que marca el DropPoint2
            Vector3 dropPosition = dropPoint.position;
            heldObject.transform.position = dropPosition;
            heldObject.transform.parent = dropPoint; // Hacer que el objeto sea hijo del dropPoint

            // Congelar la posici�n y rotaci�n del objeto para evitar que se mueva
            Rigidbody dropRb = heldObject.GetComponent<Rigidbody>();
            if (dropRb != null)
            {
                dropRb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            }

            // Marcar el punto como ocupado
            if (dropPointScript != null)
            {
                dropPointScript.isOccupied = true;
            }

            // Soltar el objeto
            heldObject = null;

            // Destruir la previsualizaci�n
            if (previewObject != null)
            {
                Destroy(previewObject);
                previewObject = null;
            }
        }
    }

    void UpdatePreview()
    {
        if (heldObject != null && previewObject != null)
        {
            // Usar la posici�n del raycastOrigin como referencia para buscar puntos de entrega
            Vector3 originPosition = raycastOrigin != null ? raycastOrigin.position : transform.position;

            // Buscar puntos de entrega cercanos en el rango especificado
            Collider[] hitColliders = Physics.OverlapSphere(originPosition, interactionRange, dropPointLayer);

            if (hitColliders.Length > 0)
            {
                // Encontrar el punto de entrega m�s cercano
                Collider nearestDropPoint = null;
                float nearestDistance = float.MaxValue;

                foreach (Collider dropPoint in hitColliders)
                {
                    // Verificar que el collider sea un DropPoint2 y no un hijo de este
                    // Verifica si el objeto tiene el tag "DropPoint2" (o cualquier otra forma de identificaci�n)
                    if (dropPoint.CompareTag("DropPoint"))
                    {
                        float distance = Vector3.Distance(originPosition, dropPoint.transform.position);
                        if (distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearestDropPoint = dropPoint;
                        }
                    }
                }

                // Si encontramos un DropPoint2 v�lido
                if (nearestDropPoint != null)
                {
                    currentDropPoint = nearestDropPoint.transform;

                    // Calcular la posici�n de la previsualizaci�n usando el centro del objeto vac�o
                    Vector3 previewPosition = currentDropPoint.position;

                    // Actualizar la posici�n y rotaci�n de la previsualizaci�n
                    previewObject.SetActive(true);
                    previewObject.transform.position = previewPosition;
                    previewObject.transform.rotation = currentDropPoint.rotation;

                    // Ajustar escala para coincidir con el `dropPoint`
                    previewObject.transform.localScale = currentDropPoint.localScale;
                }
            }
            else
            {
                // Desactivar la previsualizaci�n si no hay puntos de entrega cercanos
                currentDropPoint = null;
                previewObject.SetActive(false);
            }
        }
        else
        {
            // Si no hay objeto sostenido, desactivar la previsualizaci�n
            currentDropPoint = null;
            if (previewObject != null)
            {
                previewObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualizar el rango de interacci�n en el editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);

        // Visualizar todos los puntos de entrega en el editor
        Gizmos.color = Color.green;
        Collider[] dropPoints = Physics.OverlapSphere(transform.position, interactionRange, dropPointLayer);
        foreach (Collider dropPoint in dropPoints)
        {
            Gizmos.DrawWireSphere(dropPoint.transform.position, 0.5f);
        }
    }
}

public class DropPoint : MonoBehaviour
{
    public bool isOccupied = false;
}



