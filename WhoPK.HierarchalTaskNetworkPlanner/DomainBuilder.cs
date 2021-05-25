using WhoPK.HierarchalTaskNetworkPlanner.Factory;

namespace WhoPK.HierarchalTaskNetworkPlanner
{
    /// <summary>
    ///     A simple domain builder for easy use when one just need the core functionality
    ///     of the BaseDomainBuilder. This class is sealed, to extend the
    ///     functionality of the domain builder, extend BaseDomainBuilder instead.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class DomainBuilder<T> : BaseDomainBuilder<DomainBuilder<T>, T>
        where T : IContext
    {
        // ========================================================= CONSTRUCTION

        public DomainBuilder(string domainName) : base(domainName, new DefaultFactory())
        {
        }

        public DomainBuilder(string domainName, IFactory factory) : base(domainName, factory)
        {
        }
    }
}