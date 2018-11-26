namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]    
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]
	[RequireComponent(typeof(SolidGroundComponent))] 
    [RequireRoomVolume(4)]
    public partial class PlanterPotSquareObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Square Pot"); } } 

        public virtual Type RepresentedItemType { get { return typeof(PlanterPotSquareItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Housing");                                 
            this.GetComponent<HousingComponent>().Set(PlanterPotSquareItem.HousingVal);                                



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(1500)]
    public partial class PlanterPotSquareItem : WorldObjectItem<PlanterPotSquareObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Square Pot"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Sometimes you just want to bring a little bit of nature into your house."); } }

        static PlanterPotSquareItem()
        {
            
        }
        
        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.9f    
        };}}
    }


    [RequiresSkill(typeof(StoneworkingSkill), 3)]
    public partial class PlanterPotSquareRecipe : Recipe
    {
        public PlanterPotSquareRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PlanterPotSquareItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(StoneworkingEfficiencySkill), 10, StoneworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PulpFillerItem>(typeof(StoneworkingEfficiencySkill), 5, StoneworkingEfficiencySkill.MultiplicativeStrategy)
            };
            SkillModifiedValue value = new SkillModifiedValue(5, StoneworkingSpeedSkill.MultiplicativeStrategy, typeof(StoneworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(PlanterPotSquareRecipe), Item.Get<PlanterPotSquareItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<PlanterPotSquareItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Planter Pot Square", typeof(PlanterPotSquareRecipe));
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}