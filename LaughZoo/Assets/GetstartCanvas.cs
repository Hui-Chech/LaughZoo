using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetstartCanvas : MonoBehaviour
{
    [SerializeField] private Button StatButton;

    public Button GetGameStartButton()
    {
        return StatButton;
    }
}
