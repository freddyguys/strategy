public class Soldier
{
    /// <summary
    /// Creating new soldier using this constructor
    /// </summary
    /// <param name="Name">soldier name</param>
    /// <param name="Icon">Item icon path in resources</param>
    /// <param name="Description">Item description</param>
    /// <param name="TeamTag">Good or Bad guy</param>

    public Soldier(string Name, string Icon, string Description, TeamTag Tag)
    {
        name = Name;
        icon = Icon;
        description = Description;
        tag = Tag;
    }

    public string name;
    public string icon;
    public string description;
    public TeamTag tag;

}
