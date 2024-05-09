using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCrawler : MonoBehaviour
{

    [SerializeField] private float scrollSpeed = 75f;

    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * scrollSpeed * Time.deltaTime);

        if (transform.position.y >= 4832.717)
        {
            Debug.Log("yey kelar");
        }
    }

    
}
