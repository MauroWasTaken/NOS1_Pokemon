using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TooltipScript : MonoBehaviour
{
    public static TooltipScript _instance;
    [SerializeField]
    TextMeshProUGUI tooltipLabel;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }
    void Update()
    {
        this.gameObject.transform.position = Input.mousePosition + new Vector3(-200,20,0);
    }
    public void ShowTooltip(string message)
    {
        gameObject.SetActive(true);
        tooltipLabel.text = message;
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
        tooltipLabel.text = string.Empty;
    }
}
