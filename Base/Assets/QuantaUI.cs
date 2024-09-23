using TMPro;
using UnityEngine;

public class QuantaUI : MonoBehaviour
{
    public TextMeshProUGUI quantaText;

    void Update()
    {
        quantaText.text = "Q"+PlayerStats.quanta.ToString();
    }
}
