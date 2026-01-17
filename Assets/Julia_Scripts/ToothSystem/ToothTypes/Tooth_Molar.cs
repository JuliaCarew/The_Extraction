using UnityEngine;

public class Tooth_Molar : BaseTooth
{
    protected override void Start()
    {
        toothType = ToothType.Molar;
        base.Start();
    }

    public override ToothType GetToothType()
    {
        return ToothType.Molar;
    }
}
