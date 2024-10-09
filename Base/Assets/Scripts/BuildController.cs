using UnityEngine;

public class BuildController : MonoBehaviour
{
    [SerializeField] BuildableItemSO itemSelected;
    GameManager manager;

    private void Start()
    {
        manager = ServiceLocator.GetService<GameManager>();
    }

    public void SelectItem(BuildableItemSO newItem)
    {
        itemSelected = newItem;
    }

    public BuildableItemSO GetSelectedItem()
    {
        return itemSelected;
    }

    public void TryBuild(Node node)
    {
        if (manager.Score.ValidateScore(itemSelected.Cost))
        {
            Build(node);
        }

        else
        {
            Debug.Log("No hay suficiente dinero para construir.");
        }
    }

    public void TryUpdate(IBuildable buildable)
    {
        if (manager.Score.ValidateScore(buildable.data.Cost))
        {
            if (buildable.TryUpdate())
            {
                UpdateBuild(buildable);
            }

            else
            {
                Debug.Log("No hay mas actualizaciones disponibles.");
            }
        }

        else
        {
            Debug.Log("No hay suficiente dinero para construir.");
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
}
