using UnityEngine;

public class BuildController : MonoBehaviour
{
    public delegate void DemolitionStateChangedDelegate(bool activated);
    public DemolitionStateChangedDelegate DemolitionStateChanged;

    [SerializeField] BuildableItemSO itemSelected;
    GameManager manager;

    bool demolitionMode;

    private void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();

        ChangeDemolitionMode(false);
    }

    public void SelectItem(BuildableItemSO newItem)
    {
        itemSelected = newItem;
    }

    public BuildableItemSO GetSelectedItem()
    {
        return itemSelected;
    }

    public bool TryBuild(Node node)
    {
        if (manager.Score.ValidateScore(itemSelected.Cost))
        {
            Build(node);
            return true;
        }

        else
        {
            Debug.Log("No hay suficiente dinero para construir.");
            return false;
        }
    }

    public bool TryUpdate(IBuildable buildable)
    {
        if (manager.Score.ValidateScore(buildable.data.Cost))
        {
            if (buildable.TryUpdate())
            {
                UpdateBuild(buildable);
                return true;
            }

            else
            {
                Debug.Log("No hay mas actualizaciones disponibles.");
                return false;
            }
        }

        else
        {
            Debug.Log("No hay suficiente dinero para construir.");
            return false;
        }
    }

    public void Build(Node node)
    {
        BuildableItemSO scriptable = itemSelected;
        manager.Score.RemoveScore(scriptable.Cost);
        GameObject itemGO = Instantiate(scriptable.Prefab, node.GetBuildPosition(), Quaternion.identity);
        IBuildable buildable = itemGO.GetComponent<IBuildable>();
        buildable.Configure(scriptable);
        node.SetBuild(buildable);
    }

    public void UpdateBuild(IBuildable buildable)
    {
        buildable.Update();
        manager.Score.RemoveScore(buildable.data.Cost);
    }

    public void Demolish(IBuildable buildable)
    {
        if (buildable != null)
        {
            int cost = (int)((buildable.GetCurrentUpdate() + 1) * buildable.data.Cost * 0.5f);
            Debug.Log(cost);
            manager.Score.AddScore(cost);
            buildable.Demolished();
            ChangeDemolitionMode(false);
        }
    }

    public void ChangeDemolitionMode(bool activate)
    {
        demolitionMode = activate;
        DemolitionStateChanged?.Invoke(activate);
    }

    public bool GetDemolitionState()
    {
        return demolitionMode;
    }
}
