namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Gameplay.Systems.TextLinks;

    [RequiresSkill(typeof(HuntingSkill), 3)]
    public class SkinElkRecipe : Recipe
    {
        public SkinElkRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SkinnedElkItem>(1),
               new CraftingElement<LeatherHideItem>(typeof(HuntingSkill), 2, HuntingSkill.MultiplicativeStrategy),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(1),
            };
            this.Initialize(Localizer.DoStr("Skin Elk"), typeof(SkinElkRecipe));
            this.CraftMinutes = CreateCraftTimeValue(typeof(SkinElkRecipe), this.UILink(), 1, typeof(HuntingSkill));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}