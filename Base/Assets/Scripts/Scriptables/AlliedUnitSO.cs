using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AlliesTypeEnum
{
    WhiteBloodCell
}

[CreateAssetMenu(fileName = "Allie Data", menuName = "New Allie Data")]
public class AlliedUnitSO : CharacterSO
{
    [Header("Allie Data")]
    [SerializeField] AlliesTypeEnum _type;
}
