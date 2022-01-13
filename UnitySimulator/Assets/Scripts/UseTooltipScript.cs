using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class UseTooltipScript : MonoBehaviour
{
    public string message;
    void OnMouseEnter()
    {
        TooltipScript._instance.ShowTooltip(message);
    }
    void OnMouseExit()
    {
        TooltipScript._instance.HideTooltip();
    }
}
