public sealed class UnitModel : AbstractUnitModel
{
    public UnitModel(UnitStats stats) 
        : base(stats)
    {   }

    public void SetWalkSpeed() => CurrentSpeed = stats.WalkingSpeed;

    public void SetRunSpeed() => CurrentSpeed = stats.CalculateRunSpeed();

    public void SetZeroSpeed() => CurrentSpeed = 0;
}