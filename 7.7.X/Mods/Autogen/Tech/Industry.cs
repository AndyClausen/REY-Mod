namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;

    [Serialized]
    [RequiresSkill(typeof(EngineerSkill), 0)]    
    public partial class IndustrySkill : Skill
    {
        public override string FriendlyName { get { return "Industry"; } }
        public override string Description { get { return Localizer.DoStr(""); } }

        public static int[] SkillPointCost = { 1, 1, 1, 1, 1 };
        public override int RequiredPoint { get { return this.Level < this.MaxLevel ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < this.MaxLevel ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 1; } }
    }

    [Serialized]
    public partial class IndustrySkillBook : SkillBook<IndustrySkill, IndustrySkillScroll>
    {
        public override string FriendlyName { get { return "Industry Skill Book"; } }
    }

    [Serialized]
    public partial class IndustrySkillScroll : SkillScroll<IndustrySkill, IndustrySkillBook>
    {
        public override string FriendlyName { get { return "Industry Skill Scroll"; } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)] 
    public partial class IndustrySkillBookRecipe : Recipe
    {
        public IndustrySkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<IndustrySkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(ResearchEfficiencySkill), 100, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BookItem>(typeof(ResearchEfficiencySkill), 16, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy),
                new CraftingElement<BrickItem>(typeof(ResearchEfficiencySkill), 50, ResearchEfficiencySkill.MultiplicativeStrategy), 
            };
            this.CraftMinutes = new ConstantValue(30);

            this.Initialize("Industry Skill Book", typeof(IndustrySkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
