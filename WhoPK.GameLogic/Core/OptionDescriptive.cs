using LiteDB;

namespace WhoPK.GameLogic.Core
{
    public class OptionDescriptive : Option
    {
        [BsonField("d")]
        public string Description { get; set; }
    }
}
