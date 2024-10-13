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
    public ParticleSystem buildParticle;
    public ParticleSystem demolitionParticle;

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
            if (!manager.Build.GetDemolitionState())
            {
                if (manager.Build.TryBuild(this))
                {
                    ServiceLocator.GetService<AudioManager>().PlayMainSfx("Build");
                    buildParticle.Play();
                }
            }
            else
            {
                manager.Build.ChangeDemolitionMode(false);
            }
        }

        else
        {
            if (manager.Build.GetDemolitionState())
            {
                demolitionParticle.Play();
                manager.Build.Demolish(buildable);
                buildable = null;
            }

            else
            {
                if (manager.Build.TryUpdate(buildable))
                {
                    ServiceLocator.GetService<AudioManager>().PlayMainSfx("Build");
                    buildParticle.Play();
                }
            }
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
