using UnityEngine;

[System.Serializable]
public class DragonInfo : MonoBehaviour
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public float HealthPoint { get; private set; }
    [field: SerializeField] public float MoveSpeed { get; private set; }
    [field: SerializeField] public float FlappingForce { get; private set; }
    [field: SerializeField] public ElementType Type { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField] public bool IsDead { get; set; }

    public void FillInfo(Dragon data)
    {
        Name = data.Name;
        HealthPoint = data.HealthPoint;
        MoveSpeed = data.MoveSpeed;
        FlappingForce = data.FlappingForce;
        Type = data.Type;
        Color = data.Color;
        Icon = data.Icon;
    }
}