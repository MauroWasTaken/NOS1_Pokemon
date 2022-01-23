using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject defaultFocus;
    private void OnEnable()
    {
          EventSystem.current.SetSelectedGameObject(defaultFocus);
    }
}
