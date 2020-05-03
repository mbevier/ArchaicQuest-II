using WhoPK.GameLogic.Core;
using System.Collections.Generic;

namespace WhoPK.GameLogic.Character.Class
{
    public class Class : OptionDescriptive
    {
        public List<Class> SeedData()
        {
            var seedData = new List<Class>()
            {
                new Class()
                {
                    Name = "Fighter",
                    Description = @"Warriors are able to use any weapon and armour effectively along side their wide range of lethal and defensive combat skills."
                },

                new Class()
                {
                    Name = "Thief",
                    Description = @"Rogues are masters at the arts of remaining hidden and delivering devastating blows from the shadows before fleeing into the darkness once more."
                },
                new Class()
                {   
                    Name = "Cleric",
                    Description =  @"Cleric power comes from the gods they worship, stronger the devotion, stronger the power."
                },
                new Class()
                {
                    Name = "Mage",
                    Description = @"Mages are the most feared across the realm due to their devastating spells and power."
                }
            };

            return seedData;
        }



    }
}
