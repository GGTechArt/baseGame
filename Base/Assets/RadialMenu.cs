using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    [SerializeField]
    GameObject EntryPrefab;

    [SerializeField]
    float Radius = 20f;

    [SerializeField]
    List<Texture> icons;

    List<RadialMenueEntry> Entries;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        Entries = new List<RadialMenueEntry>();
        buildManager = BuildManager.instance;
    }

    void AddEntry(string pLabel, Texture pIcon, RadialMenueEntry.RadialMenuEntryDelegate pCallback)
    {
        GameObject entry = Instantiate(EntryPrefab,transform);

        RadialMenueEntry rme = entry.GetComponent<RadialMenueEntry>();
        rme.SetLabel(pLabel);
        rme.SetIcon(pIcon);
        rme.SetCallback(pCallback);

        Entries.Add( rme );
    }

    public void Open()
    {
        for (int i = 0; i < 3; i++)
        {
            AddEntry("Button" + i.ToString(), icons[i], selectTurret);
        }
        Rearrange();
    }

    void Rearrange()
    {
        float radiansOfSeparation = (Mathf.PI * 2) / Entries.Count;

        for (int i = 0; i < Entries.Count; i++)
        {
            float x = Mathf.Sin(radiansOfSeparation * i)* Radius;
            float y = Mathf.Cos(radiansOfSeparation * i)* Radius;

            RectTransform rect = Entries[i].GetComponent<RectTransform>();

            rect.localScale = Vector3.zero;
            rect.DOScale(Vector3.one, .3f).SetEase(Ease.OutQuad).SetDelay(.05f * i);
            rect.DOAnchorPos(new Vector3(x, y, 0),.3f).SetEase(Ease.OutQuad).SetDelay(.05f*i);
        }
    }

    public void selectTurret(RadialMenueEntry pEntry)
    {

    }

    public void StandarShopTurret()
    {
        buildManager.SetTurretTobuild(buildManager.standardTurretPrefab);
    }

    public void OtherShopTurret()
    {
        buildManager.SetTurretTobuild(buildManager.anotherTurretPrefab);
    }
}
