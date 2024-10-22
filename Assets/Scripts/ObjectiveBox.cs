using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveBox : MonoBehaviour
{

    public GameObject OjctiveBox;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OjctiveBox.SetActive(false);
        }
    }
}
