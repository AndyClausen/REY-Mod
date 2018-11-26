namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Serialization;
    using Eco.World;
    using Eco.World.Blocks;



    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Floor", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Wall", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Cube", typeof(CorrugatedSteelItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 4)]
    public partial class CorrugatedSteelCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Roof", typeof(CorrugatedSteelItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 2)]
    public partial class CorrugatedSteelRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Column", typeof(CorrugatedSteelItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 4)]
    public partial class CorrugatedSteelColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Window", typeof(CorrugatedSteelItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 3)]
    public partial class CorrugatedSteelWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }



    [RotatedVariants(typeof(CorrugatedSteelStairsBlock), typeof(CorrugatedSteelStairs90Block), typeof(CorrugatedSteelStairs180Block), typeof(CorrugatedSteelStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Stairs", typeof(CorrugatedSteelItem))]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 1)]
    public partial class CorrugatedSteelStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 1)]
    public partial class CorrugatedSteelStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 1)]
    public partial class CorrugatedSteelStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [RequiresSkill(typeof(Tier3ConstructionSkill), 1)]
    public partial class CorrugatedSteelStairs270Block : Block
    { }

}
