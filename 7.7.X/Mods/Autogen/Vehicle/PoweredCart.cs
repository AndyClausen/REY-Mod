namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.World.Blocks;

    [Serialized]
    [Weight(15000)]  
    public class PoweredCartItem : WorldObjectItem<PoweredCartObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Large cart for hauling sizable loads."); } }
    }

    [RequiresSkill(typeof(MechanicalEngineeringSkill), 0)] 
    public class PoweredCartRecipe : Recipe
    {
        public PoweredCartRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PoweredCartItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CombustionEngineItem>(1),
                new CraftingElement<LumberItem>(typeof(MechanicsAssemblyEfficiencySkill), 20, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<ClothItem>(typeof(MechanicsAssemblyEfficiencySkill), 20, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RivetItem>(typeof(MechanicsAssemblyEfficiencySkill), 5, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<RubberWheelItem>(typeof(MechanicsAssemblyEfficiencySkill), 3, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = new ConstantValue(25);

            this.Initialize(Localizer.DoStr("Powered Cart"), typeof(PoweredCartRecipe));
            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(AirPollutionComponent))]       
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class PoweredCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static PoweredCartObject()
        {
            WorldObject.AddOccupancy<PoweredCartObject>(new List<BlockOccupancy>(0));
        }

        private static Dictionary<Type, float> roadEfficiency = new Dictionary<Type, float>()
        {
            { typeof(GrassBlock) , 0.7f}, { typeof(SandBlock) , 0.3f},
            { typeof(DesertSandBlock) , 0.3f}, { typeof(WetlandsBlock) , 0.5f},
            { typeof(SnowBlock) , 0.5f}, { typeof(DirtBlock) , 0.8f},
            { typeof(ForestSoilBlock) , 0.6f},
            { typeof(DirtRoadBlock), 1.0f }, { typeof(DirtRoadWorldObjectBlock), 1.0f },
            { typeof(StoneRoadBlock), 1.3f }, { typeof(StoneRoadWorldObjectBlock), 1.3f },
            { typeof(AsphaltRoadBlock), 1.6f }, { typeof(AsphaltRoadWorldObjectBlock), 1.6f }
        };
        public override LocString DisplayName { get { return Localizer.DoStr("Powered Cart"); } }
        public Type RepresentedItemType { get { return typeof(PoweredCartItem); } }

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(PetroleumItem),
typeof(GasolineItem),
        };

        private PoweredCartObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(20, 3000000);            
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(40);    
            this.GetComponent<AirPollutionComponent>().Initialize(0.2f);            
            this.GetComponent<VehicleComponent>().Initialize(15, 1, roadEfficiency, 1);
        }
    }
}