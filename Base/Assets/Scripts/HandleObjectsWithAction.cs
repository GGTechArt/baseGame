using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ActivationType
{
    Collision = 0,
    Key,
    Start,
    CollisionAndKey,
    Button,
    WaveFinished,
    EnemyKilled,
    BuildPlaced,
    BuildUpdated,
    ItemSelected,
    DemolishStateChanged,
    BuildSelled
}

public enum ValidationEnum { PlayerPrefs }
public enum NumericValidationEnum { Equal, Greater, Less, LessEqual, GreaterEqual }
public class HandleObjectsWithAction : MonoBehaviour
{
    GameManager manager;

    [SerializeField] ActivationType activationType;

    [SerializeField] UnityEvent eventosIniciales;
    [SerializeField] UnityEvent eventosFinales;
    [SerializeField] UnityEvent eventosNoCumplidasValidaciones;
    [SerializeField] UnityEvent eventosEntrarColision;
    [SerializeField] UnityEvent eventosSalirColision;

    [SerializeField] KeyCode key;

    [SerializeField] List<string> tags = new List<string>();

    [SerializeField] float delay;
    float newTime = 0;

    bool activated = false;
    bool targetInside = false;

    [SerializeField] List<Validation> validations = new List<Validation>();
    public List<Validation> Validations { get => validations; set => validations = value; }

    // Start is called before the first frame update
    void Start()
    {
        eventosIniciales?.Invoke();

        manager = ServiceLocator.GetService<GameManager>();

        switch (activationType)
        {
            case ActivationType.Start:
                ActivateDelay();
                break;
            case ActivationType.Button:
                GetComponent<Button>().onClick.AddListener(() => ActivateDelay());
                break;
            case ActivationType.WaveFinished:
                manager.Waves.WaveFinished += WaveFinished;
                break;
            case ActivationType.EnemyKilled:
                CharacterConfig.OnCharacterKilled += EnemyKilled;
                break;
            case ActivationType.BuildPlaced:
                manager.Build.BuildPlaced += BuildPlaced;
                break;
            case ActivationType.BuildUpdated:
                manager.Build.BuildUpdated += BuildUpdated;
                break;
            case ActivationType.ItemSelected:
                manager.Build.ItemSelected += ItemSelected;
                break;
            case ActivationType.BuildSelled:
                manager.Build.BuildDemolished += BuildSelled;
                break;
            case ActivationType.DemolishStateChanged:
                manager.Build.DemolitionStateChanged += DemolishStateChanged;
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (activationType == ActivationType.Key)
                ActivateDelay();

            else if (activationType == ActivationType.CollisionAndKey)
                if (targetInside)
                    ActivateDelay();
        }


        if (activated)
        {
            newTime += Time.unscaledDeltaTime;

            if (newTime >= delay)
                ExecuteEvents();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activationType == ActivationType.Collision)
            ActivateDelay();
        else if (activationType == ActivationType.CollisionAndKey)
            if (tags.Contains(other.gameObject.tag))
                targetInside = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (activationType == ActivationType.Collision)
            ActivateDelay();
        else if (activationType == ActivationType.CollisionAndKey)
            if (tags.Contains(other.gameObject.tag))
                targetInside = false;
    }
    public void BuildPlaced(IBuildable buildable)
    {
        ActivateDelay();
    }
    public void BuildUpdated(IBuildable buildable)
    {
        ActivateDelay();
    }

    public void ItemSelected(BuildableItemSO newItem)
    {
        ActivateDelay();
    }
    public void EnemyKilled(CharacterConfig character)
    {
        ActivateDelay();
    }
    public void WaveFinished()
    {
        ActivateDelay();
    }

    public void DemolishStateChanged(bool activated)
    {
        ActivateDelay();
    }

    public void BuildSelled(IBuildable buildable)
    {
        ActivateDelay();
    }

    public void ActivateDelay()
    {
        if (!activated && VerifyValidations())
        {
            activated = true;
        }
    }

    public void ExecuteEvents()
    {
        eventosFinales?.Invoke();
        activated = false;
        gameObject.SetActive(false);
    }

    public bool VerifyValidations()
    {
        bool complete = true;

        foreach (var item in validations)
        {
            switch (item.ValidationType)
            {
                case ValidationEnum.PlayerPrefs:
                    if (!item.BoolValue)
                    {
                        if (!PlayerPrefs.HasKey(item.StringValue))
                            complete = false;
                    }
                    else
                     if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
                        complete = false;
                    break;
            }
        }

        if (!complete)
            eventosNoCumplidasValidaciones?.Invoke();

        return complete;
    }
}

[Serializable]
public class Validation
{
    [SerializeField] ValidationEnum validationType;
    [SerializeField] bool boolValue;
    [SerializeField] string stringValue;
    [SerializeField] int intValue;
    [SerializeField] NumericValidationEnum numericValidationType;

    public ValidationEnum ValidationType { get => validationType; set => validationType = value; }
    public bool BoolValue { get => boolValue; set => boolValue = value; }
    public string StringValue { get => stringValue; set => stringValue = value; }
    public int IntValue { get => intValue; set => intValue = value; }
    public NumericValidationEnum NumericValidationType { get => numericValidationType; set => numericValidationType = value; }
}