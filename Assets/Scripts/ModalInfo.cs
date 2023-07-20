using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalInfo : MonoBehaviour
{   
    public void Show()
    {
        GetComponent<Animator>().Play("modalInfoShow");
    }

    public void Hide()
    {
        GetComponent<Animator>().Play("modalInfoHide");
    }
}
