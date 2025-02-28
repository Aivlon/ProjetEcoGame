using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField]
    private float pickupRange = 2.6f; // Dist max pr ramasser un obj

    public Inventory Inventory; // Réf à l'inventaire

    [SerializeField]
    private LayerMask layerMask; // Masque de détection

    [SerializeField]
    private GameObject toucheE; // Indicateur "E"

    private RaycastHit currentHit; // Obj détecté
    private bool isFacingItem = false; // Face à un obj ?

    void Update()
    {
        // Vérif si obj devant
        CheckForItem();

        // Si obj détecté et "E" pressé
        if (isFacingItem && Input.GetKeyDown(KeyCode.E))
        {
            PickupCurrentItem();
        }
    }

    private void CheckForItem()
    {
        // Raycast pr détecter un obj
        if (Physics.Raycast(transform.position, transform.forward, out currentHit, pickupRange, layerMask))
        {
            // Si l'obj détecté a le tag "Item"
            if (currentHit.transform.CompareTag("Item"))
            {
                Debug.Log("Item détecté.");
                toucheE.SetActive(true); // Active "E"
                isFacingItem = true;
                return;
            }
        }

        // Si aucun obj détecté
        toucheE.SetActive(false);
        isFacingItem = false;
    }

    private void PickupCurrentItem()
    {
        // Vérif que l'obj est valide
        if (currentHit.transform != null)
        {
            Item itemComponent = currentHit.transform.GetComponent<Item>();

            if (itemComponent != null)
            {
                Debug.Log($"Ramassé : {itemComponent.item.name}");

                // Ajoute à l'inventaire
                Inventory.AddItem(itemComponent.item);

                // Détruit l'obj ds la scène
                Destroy(currentHit.transform.gameObject);

                // Désactive "E"
                toucheE.SetActive(false);
                isFacingItem = false;
            }
            else
            {
                Debug.LogWarning("Pas de composant Item trouvé.");
            }
        }
        else
        {
            Debug.LogWarning("Pas d'obj valide à ramasser.");
        }
    }
}
