using UnityEngine;


public class LancerGraine : MonoBehaviour
{
    public GameObject grainePrefab; // Assigne le prefab ici
    public Transform pointDeLancement; // Position de départ de la graine
    public float forceLancement = 10f;
     void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // "G" pour lancer la graine
        {
            Lancer();
        }
    }

    void Lancer()
    {
        if (grainePrefab != null && pointDeLancement != null)
        {
            // Instancier la graine
            GameObject graine = Instantiate(grainePrefab, pointDeLancement.position, Quaternion.identity);
            Rigidbody rb = graine.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                // Ajouter une force pour lancer la graine
                rb.AddForce(pointDeLancement.forward * forceLancement, ForceMode.Impulse);
            }
        }
    }
}

