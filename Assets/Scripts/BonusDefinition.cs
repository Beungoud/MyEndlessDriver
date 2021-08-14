using UnityEngine;
using System.Collections;

public class BonusDefinition : MonoBehaviour {
    public enum TypeEnum
    {
        dog, missile, coin
    }
    [SerializeField]
    private TypeEnum type;

    public TypeEnum Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }
}
