namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;



    [Serialized]
    [Weight(500)]
    [Fuel(2000)]
    [Currency]
    public partial class BeeswaxItem :
    Item
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Beeswax"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A natural wax that can be used in many things."); } }

    }

}