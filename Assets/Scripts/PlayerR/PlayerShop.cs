using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PlayerShop : MonoBehaviour
{
    public float detectionRadius = 5f;
    [SerializeField] private LayerMask shopLayerMask;
    [SerializeField] private TextMeshProUGUI shopText;

    private InputManager inputManager;
    
    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, shopLayerMask);
        if (hitColliders.Length != 1)
        {
            shopText.text = "";
            return;
        }

        shopText.text = "Press K to open shop";
        if (inputManager.PlayerInput.OnGround.OpenShop.triggered)
        {
            DataPersistenceManager.instance.SaveGame();
            SceneManager.LoadScene("Scenes/Remade/ShopScene");
            SceneHelper.CurrentScenePath = "Scenes/Remade/ShopScene";
        }
    }

}
