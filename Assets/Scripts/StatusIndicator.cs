using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform healthBarRect;
    [SerializeField]
    private Text HealthText;
    [SerializeField]
    private string charName;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (healthBarRect == null)
        {
            Debug.LogError("Status Indicator: No Health Bar Object Reference");
            Debug.LogError("Status Indicator: No Health Text Object Reference");
        }
        
    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = _cur * 1f / _max / 1f;//_cur/ _max ;
        Image image = healthBarRect.GetComponent<Image>();
        healthBarRect.localScale = new Vector3(_value, healthBarRect.localScale.y, healthBarRect.localScale.z);
        if (_value > 0.5)
        {
            image.color = new Color(2 * (1 - _value), 1, 0);
        }
        else
        {
            image.color = new Color(1, 2 * _value, 0);
        }
        HealthText.text =  charName + " " + _cur + "/" + _max;
    }


}
