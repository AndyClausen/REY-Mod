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
    [Tier(4)]
    [IsForm("Floor", typeof(FlatSteelItem))]
    public partial class FlatSteelFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Wall", typeof(FlatSteelItem))]
    public partial class FlatSteelWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Cube", typeof(FlatSteelItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 4)]
    public partial class FlatSteelCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Roof", typeof(FlatSteelItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 2)]
    public partial class FlatSteelRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Column", typeof(FlatSteelItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 4)]
    public partial class FlatSteelColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Window", typeof(FlatSteelItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 3)]
    public partial class FlatSteelWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }



    [RotatedVariants(typeof(FlatSteelStairsBlock), typeof(FlatSteelStairs90Block), typeof(FlatSteelStairs180Block), typeof(FlatSteelStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Stairs", typeof(FlatSteelItem))]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 1)]
    public partial class FlatSteelStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 1)]
    public partial class FlatSteelStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 1)]
    public partial class FlatSteelStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [RequiresSkill(typeof(Tier4ConstructionSkill), 1)]
    public partial class FlatSteelStairs270Block : Block
    { }

}
