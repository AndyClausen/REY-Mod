// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Objects;
using Eco.Mods.TechTree;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using REYmod.Utils;

[Serialized]
[RequireComponent(typeof(MinimapComponent))]
[RequireComponent(typeof(PowerGridComponent))]
[RequireComponent(typeof(PowerConsumptionComponent))]
[RequireComponent(typeof(ChargingComponent))]
[RequireComponent(typeof(PowerGridNetworkComponent))]
[RequireComponent(typeof(SpecificGroundComponent))]
public class LaserObject : WorldObject
{
    public float MinimapYaw { get { return 0.0f; } }
    public bool HideOnMinimap { get { return false; } }

    protected override void Initialize()
    {
        base.Initialize();
        this.GetComponent<MinimapComponent>().Initialize("Laser");
        this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
        this.GetComponent<PowerConsumptionComponent>().Initialize(9000);
        this.GetComponent<ChargingComponent>().Initialize(30, 30);
        this.GetComponent<PowerGridNetworkComponent>().Initialize(new Dictionary<Type, int> { { typeof(LaserObject), 8 }, { typeof(ComputerLabObject), 4 } }, false);
        this.GetComponent<SpecificGroundComponent>().Initialize(typeof(ReinforcedConcreteItem));
    }

    static LaserObject()
    {
        AddOccupancyList(typeof(LaserObject), new BlockOccupancy(Vector3i.Zero, typeof(WorldObjectBlock)));
    }
}