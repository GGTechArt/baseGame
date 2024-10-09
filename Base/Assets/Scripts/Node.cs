using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    IBuildable buildable;
    GameManager manager;

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    private Color startColor;

    public Transform buildPosition;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        manager = ServiceLocator.GetService<GameManager>();
    }

    public Vector3 GetBuildPosition()
    {
        return buildPosition.position;
    }

    private void OnMouseDown()
    {
        if (buildable == null)
        {
            manager.Build.TryBuild(this);
        }

        else
        {
            manager.Build.TryUpdate(buildable);
        }
    }

    //Estudiar y diseñar el input de VR para este llamado.
    void OnMouseEnter()
    {
        if (manager.Score.ValidateScore(manager.Build.GetSelectedItem().Cost))
        {
            rend.material.color = hoverColor;
        }

        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    public void SetBuild(IBuildable buildable)
    {
        this.buildable = buildable;
    }

}
