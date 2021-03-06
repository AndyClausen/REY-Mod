namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class LumberDoorObject : 
        DoorObject, 
        IRepresentsItem
    {
        public override string FriendlyName { get { return "Lumber Door"; } } 

        public override Type RepresentedItemType { get { return typeof(LumberDoorItem); } } 


        protected override void Initialize()
        {
            base.Initialize(); 


        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class LumberDoorItem :
        WorldObjectItem<LumberDoorObject> 
    {
        public override string FriendlyName { get { return "Lumber Door"; } } 
        public override string Description  { get { return  "A door made from finely cut lumber."; } }

        [Tooltip(100)]
        public string TierTooltip()
        {
            return "<i>Tier 2 building material</i>";
        }


        static LumberDoorItem()
        {
            
        }

    }


    [RequiresSkill(typeof(LumberWoodworkingSkill), 2)]
    public partial class LumberDoorRecipe : Recipe
    {
        public LumberDoorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<LumberDoorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LumberItem>(typeof(LumberWoodworkingEfficiencySkill), 10, LumberWoodworkingEfficiencySkill.MultiplicativeStrategy),   
                new CraftingElement<HingeItem>(typeof(WoodworkingEfficiencySkill), 2, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<NailsItem>(typeof(WoodworkingEfficiencySkill), 5, WoodworkingEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(3, LumberWoodworkingSpeedSkill.MultiplicativeStrategy, typeof(LumberWoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(LumberDoorRecipe), Item.Get<LumberDoorItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<LumberDoorItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Lumber Door", typeof(LumberDoorRecipe));
            CraftingComponent.AddRecipe(typeof(SawmillObject), this);
        }
    }
}