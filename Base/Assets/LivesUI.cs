using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    public TextMeshProUGUI livesTxt;

    void Update()
    {
        livesTxt.text = "Lives: " + PlayerStats.Lives.ToString();
    }
}
