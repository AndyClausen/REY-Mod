namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
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
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                              
    public partial class CarpentryTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Carpentry Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CarpentryTableItem); } } 


        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize("Crafting");                                 



        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(5000)]
    public partial class CarpentryTableItem : WorldObjectItem<CarpentryTableObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Carpentry Table"); } } 
        public override string Description  { get { return  "A table for basic wooden crafts for home improvement and progress."; } }

        static CarpentryTableItem()
        {
            
        }

    }


    [RequiresSkill(typeof(WoodworkingSkill), 0)]
    public partial class CarpentryTableRecipe : Recipe
    {
        public CarpentryTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CarpentryTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<StoneItem>(typeof(WoodworkingEfficiencySkill), 20, WoodworkingEfficiencySkill.MultiplicativeStrategy),   
            };
            SkillModifiedValue value = new SkillModifiedValue(1, WoodworkingSpeedSkill.MultiplicativeStrategy, typeof(WoodworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(CarpentryTableRecipe), Item.Get<CarpentryTableItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<CarpentryTableItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Carpentry Table", typeof(CarpentryTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}