using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetEndCanvas : MonoBehaviour
{
    [SerializeField] private Transform finshGroupRoot;

    [SerializeField] private Button ResetBtn;

    public Transform GetFinshGroupRoot()
    {
        return finshGroupRoot;
    }

    public void ShowEndBackGround()
    {
        this.gameObject.SetActive(true);
    }

    public Button GetResetBtn()
    {
        return ResetBtn;
    }
}
