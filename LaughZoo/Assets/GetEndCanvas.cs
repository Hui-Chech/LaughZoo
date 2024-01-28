using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEndCanvas : MonoBehaviour
{
    [SerializeField] private Transform finshGroupRoot;

    public Transform GetFinshGroupRoot()
    {
        return finshGroupRoot;
    }

    public void ShowEndBackGround()
    {
        this.gameObject.SetActive(true);
    }
}
