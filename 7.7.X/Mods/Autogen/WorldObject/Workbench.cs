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
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class WorkbenchObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override string FriendlyName { get { return "Workbench"; } } 

        public virtual Type RepresentedItemType { get { return typeof(WorkbenchItem); } } 


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
    public partial class WorkbenchItem : WorldObjectItem<WorkbenchObject>
    {
        public override string FriendlyName { get { return "Workbench"; } } 
        public override string Description  { get { return  "A bench for the basics and making even more benches."; } }

        static WorkbenchItem()
        {
            
        }

    }


    public partial class WorkbenchRecipe : Recipe
    {
        public WorkbenchRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WorkbenchItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(30),
                new CraftingElement<StoneItem>(20),   
            };
            this.CraftMinutes = new ConstantValue(5);
            this.Initialize("Workbench", typeof(WorkbenchRecipe));
            CraftingComponent.AddRecipe(typeof(CampsiteObject), this);
        }
    }
}