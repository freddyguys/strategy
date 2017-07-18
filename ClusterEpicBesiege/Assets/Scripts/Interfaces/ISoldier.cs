public interface ISoldier
{
    float Health { get; set; }
    float AttackRange { get; set; }
    float AttackPower { get; set; }
    float DetectionRadius { get; set; }
    float MovementSpeed { get; set; }
    float RotationSpeed { get; set; }
    float Cooldown { get; set; }
    SoldierType Type { get; }
    TeamTag Tag { get; set; }
}
