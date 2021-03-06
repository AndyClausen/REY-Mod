namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    [Serialized]
    [Weight(10)]                                          
     [Yield(typeof(FiddleheadsItem), typeof(ForestForagerSkill), new float[] {1f, 1.4f, 1.8f, 2.2f, 2.6f, 3f})]      
    public partial class FiddleheadsItem :
        FoodItem            
    {
        public override string FriendlyName                     { get { return "Fiddleheads"; } }
        public override string FriendlyNamePlural               { get { return "Fiddleheads"; } } 
        public override string Description                      { get { return "Acollection of the furled fronds of young ferns; a unique addition to a meal."; } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 0, Protein = 1, Vitamins = 3};
        public override float Calories                          { get { return 80; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

}