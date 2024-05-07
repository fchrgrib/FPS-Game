using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private CharacterController characterController;

    private Camera camera;
    private InputManager inputManager;
    public TextMeshProUGUI uiInteractText;

    public float rayDistance = 10f;
    public LayerMask layerMask;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<PlayerDirection>().Camera;
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance);
        RaycastHit raycastHit;
        // uiInteractText.text = "";
        if (Physics.Raycast(ray, out raycastHit, rayDistance, layerMask))
        {
            var interactable = raycastHit.collider.GetComponent<Interactable>();
            if (interactable is null)
            {
                return;
            }
            
            uiInteractText.text = interactable.interactText;
            if (inputManager.PlayerInput.OnGround.Interact.triggered)
            {
                // interactable.Interact();
                Vector3 distance = gameObject.transform.position - interactable.transform.position;
                characterController.Move(-distance * 0.9f);
            }

        }
    }
}
