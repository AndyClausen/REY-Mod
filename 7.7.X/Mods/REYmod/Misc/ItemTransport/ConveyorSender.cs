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
    using Eco.Gameplay.Wires;
    using System.Timers;
    using System.Linq;

    [Serialized]
    [RequireComponent(typeof(MustBeOwnedComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    public partial class ConveyorSenderObject :
        WorldObject, IWireContainer
    {
        public Timer timer;
        public WireOutput outputWire;
        IEnumerable<WireConnection> IWireContainer.Wires { get { return this.outputWire.SingleItemAsEnumerable(); } }
        private Inventory Storage { get { return this.GetComponent<PublicStorageComponent>().Inventory; } }
        private Inventory LinkedStorage { get { return this.GetComponent<LinkComponent>().GetSortedLinkedInventories(this.OwnerUser); } }
        private int pullcounter = 0;

        public override string FriendlyName { get { return "ConveyorSender"; } } 

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
            typeof(WoodPulpItem),
        };

        protected override void Initialize()
        {
            outputWire = new WireOutput(this, typeof(PipeBlock), new Ray(0, 0, 0, Direction.Up), "Output");                 
            this.GetComponent<PropertyAuthComponent>().Initialize();
            this.GetComponent<LinkComponent>().Initialize(2);
            this.GetComponent<AttachmentComponent>().Initialize();
            this.GetComponent<PublicStorageComponent>().Initialize(2);
            timer = new Timer(1000);
            timer.AutoReset = true;
            timer.Elapsed += CustomTick;
            timer.Start();



        }

        private void CustomTick(object sender, ElapsedEventArgs e)
        {          
            if (!this.Enabled) return;
            int mult = 1;
            int unitstosend = 8;
            pullcounter++;  
            if (pullcounter >= 30) // dont pull item into its internal inventory too often to reduce lag
            {
                this.LinkedStorage.MoveAsManyItemsAsPossible(Storage, this.OwnerUser);
                pullcounter = 0;
            }
            IEnumerable<ItemStack> nonempty = Storage.NonEmptyStacks;
            nonempty.ForEach(stack =>
            {
                int stackbefore = stack.Quantity;
                if (stack.Item.IsCarried) mult = 5; else mult = 1; // transport less carried items per second than normal ones, maybe restrict based on mass?
                if (unitstosend / mult < 1) return;
                outputWire.SendItemConsume(stack,unitstosend/mult); // thats where the items will be sent over the syste,
                unitstosend -= (stackbefore - stack.Quantity) * mult;
                if (stack.Quantity <= 0)
                {
                    Storage.AddItem(stack.Item);
                    Storage.RemoveItem(stack.Item.Type);
                }
            });
        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Weight(2000)]
    public partial class ConveyorSenderItem : WorldObjectItem<ConveyorSenderObject>
    {
        public override string FriendlyName { get { return "Conveyor Entry Point"; } } 
        public override string Description  { get { return "Entry Point of a itempipe system. (Name not final, im open to suggestions)"; } }

        static ConveyorSenderItem()
        {
            WorldObject.AddOccupancy<ConveyorSenderObject>(new List<BlockOccupancy>(){
            new BlockOccupancy(new Vector3i(0, 0, 0),typeof(PipeSlotBlock)),
            });
        }

       
    }


    [RequiresSkill(typeof(MechanicalEngineeringSkill), 4)]
    public partial class ConveyorSenderRecipe : Recipe
    {
        public ConveyorSenderRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ConveyorSenderItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MechanicsAssemblyEfficiencySkill), 10, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<GearboxItem>(typeof(MechanicsAssemblyEfficiencySkill), 4, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<PistonItem>(typeof(MechanicsAssemblyEfficiencySkill), 2, MechanicsAssemblyEfficiencySkill.MultiplicativeStrategy),
            };
            SkillModifiedValue value = new SkillModifiedValue(1, MetalworkingSpeedSkill.MultiplicativeStrategy, typeof(MetalworkingSpeedSkill), Localizer.DoStr("craft time"));
            SkillModifiedValueManager.AddBenefitForObject(typeof(ConveyorSenderRecipe), Item.Get<ConveyorSenderItem>().UILink(), value);
            SkillModifiedValueManager.AddSkillBenefit(Item.Get<ConveyorSenderItem>().UILink(), value);
            this.CraftMinutes = value;
            this.Initialize("Conveyor Entry Point", typeof(ConveyorSenderRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

}