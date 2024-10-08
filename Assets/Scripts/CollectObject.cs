using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectObject : MonoBehaviour
{
    public int collectCount;
    public Text collectText;

    private void OnTriggerEnter2D(Collider2D collition)
    {
        if (collition.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        collectText.text = collectCount.ToString();
    }

}
