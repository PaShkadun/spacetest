using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<Ability> _abilities;

    public static AbilityManager instance;

    private void Start()
    {
        if (instance != null)
        {
            Destroy(instance);
        }

        instance = this;
    }

    public static void TryGenerateAbility(Transform position)
    {
        var rndValue = Random.Range(0f, 1f);
        var value = 0f;

        Debug.Log(rndValue);

        foreach (var ability in instance._abilities)
        {
            value += ability.Chance;

            if (value >= rndValue)
            {
                Debug.Log("Generated");
                var instance = Instantiate(ability, position);
                instance.transform.position = position.position;
                break;
            }
        }
    }
}
