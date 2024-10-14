using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectObject : MonoBehaviour
{
    public int collectCount;
    public Text collectText;
    public GameObject blockSign;
    public GameObject bufferText;


    void Update()
    {
        collectText.text = "Gems: " + collectCount.ToString();

        if (collectCount == 4)
        {
            Destroy(blockSign);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collectCount < 4)
        {
            bufferText.SetActive(true);
            StartCoroutine(TextDelay());
        }
    }

    private IEnumerator TextDelay()
    {
        yield return new WaitForSeconds(3);
        bufferText.SetActive(false);
    }



}
