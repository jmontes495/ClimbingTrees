using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalsController : MonoBehaviour
{
    [SerializeField]
    private PetalsAnimation[] slots;

    private void Start()
    {
        slots = GetComponentsInChildren<PetalsAnimation>();
        FlowerContact.FlowerCollided += ShowPetals;
    }

    private void ShowPetals()
    {
        foreach (PetalsAnimation slot in slots)
        {
            if (!slot.IsRunning)
            {
                slot.BeginPetals();
                break;
            }
        }
    }
}
