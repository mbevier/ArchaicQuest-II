namespace WhoPK.HierarchalTaskNetworkPlanner.Conditions
{
    public interface ICondition
    {
        string Name { get; }
        bool IsValid(IContext ctx);
    }
}
