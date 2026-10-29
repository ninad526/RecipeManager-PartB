using System.Collections.Generic;
using RecipeManagement.Core;

namespace RecipeManagement.Tests;

/// <summary>
/// Example tests from the assignment specification. Add your own tests as you work.
/// </summary>
public sealed class RecipeManagerTests
{
    [Fact]
    public void Constructor_BuildsRecipeDictionary()
    {
        var manager = CreateManager();
        Assert.Equal(2, manager.RecipeCount);
        Assert.Equal("Recipe A", manager.FindRecipe(10)?.Title);
    }

    [Fact]
    public void InstructionsAreCompletedInFileOrder()
    {
        var manager = CreateManager();
        Assert.True(manager.StartCooking(10));
        Assert.Equal("First step", manager.PeekNextInstruction());
        Assert.Equal("First step", manager.CompleteNextInstruction());
        Assert.Equal("Second step", manager.PeekNextInstruction());
    }

    [Fact]
    public void RemovedRecipesAreRestoredLastInFirstOut()
    {
        var manager = CreateManager();
        manager.AddRecipeToCookingPlan(10);
        manager.AddRecipeToCookingPlan(20);
        manager.RemoveRecipeFromCookingPlan(10);
        manager.RemoveRecipeFromCookingPlan(20);
        Assert.Equal(20, manager.PeekLastRemovedRecipe());
        Assert.True(manager.RestoreLastRemovedRecipe());
        Assert.Equal(new[] { 20 }, manager.GetCookingPlan());
    }
[Fact]
public void AddRecipe_RejectsDuplicateId()
{
    var manager = CreateManager();

    var duplicate = new Recipe
    {
        Id = 10,
        Title = "Another Recipe"
    };

    Assert.False(manager.AddRecipe(duplicate));
    Assert.Equal(2, manager.RecipeCount);
    Assert.Null(manager.FindRecipe(999));
}
   [Fact]
public void CookingPlan_RejectsDuplicatesAndEmptyHistory()
{
    var manager = CreateManager();

    Assert.True(manager.AddRecipeToCookingPlan(10));
    Assert.False(manager.AddRecipeToCookingPlan(10));
    Assert.Equal(1, manager.CookingPlanCount);
    Assert.False(manager.RestoreLastRemovedRecipe());
}
[Fact]
public void ShoppingList_CanBeCleared()
{
    var manager = CreateManager();

    Assert.Equal(1, manager.AddIngredientsToShoppingList(10));
    Assert.Equal(1, manager.ShoppingItemCount);

    manager.ClearShoppingList();

    Assert.Equal(0, manager.ShoppingItemCount);
    Assert.Empty(manager.GetShoppingList());
}

[Fact]
public void EmptyInstructionQueue_ReturnsNull()
{
    var manager = CreateManager();

    Assert.Null(manager.PeekNextInstruction());
    Assert.Null(manager.CompleteNextInstruction());
}
[Fact]
public void RecipeWorkflow_WorksTogether()
{
    var manager = CreateManager();

    Assert.Equal(1, manager.AddIngredientsToShoppingList(10));
    Assert.True(manager.AddRecipeToCookingPlan(10));
    Assert.True(manager.StartCooking(10));
    Assert.Equal("First step", manager.PeekNextInstruction());
}
[Fact]
public void Constructor_RejectsDuplicateIds()
{
    var recipes = new[]
    {
        new Recipe { Id = 1, Title = "Recipe One" },
        new Recipe { Id = 1, Title = "Recipe Two" }
    };

    Assert.Throws<ArgumentException>(
        () => new RecipeManager(recipes));
}
[Fact]
public void RecipeInCookingPlan_CannotBeRemoved()
{
    var manager = CreateManager();

    Assert.True(manager.AddRecipeToCookingPlan(10));
    Assert.False(manager.RemoveRecipe(10));
    Assert.NotNull(manager.FindRecipe(10));
}
[Fact]
public void RecipeCatalogue_AddFindRemove()
{
    var manager = CreateManager();
    var recipe = new Recipe { Id = 30, Title = "Recipe C" };

    Assert.True(manager.AddRecipe(recipe));
    Assert.Equal("Recipe C", manager.FindRecipe(30)?.Title);
    Assert.True(manager.RemoveRecipe(30));
    Assert.Null(manager.FindRecipe(30));
}
[Fact]
public void Constructor_RejectsZeroRecipeId()
{
    var recipes = new[]
    {
        new Recipe { Id = 0, Title = "Invalid Recipe" }
    };

    Assert.Throws<ArgumentException>(
        () => new RecipeManager(recipes));
}
[Fact]
public void Constructor_RejectsBlankTitle()
{
    var recipes = new[]
    {
        new Recipe { Id = 1, Title = " " }
    };

    Assert.Throws<ArgumentException>(
        () => new RecipeManager(recipes));
}
[Fact]
public void Constructor_RejectsNullRecipes()
{
    Assert.Throws<ArgumentNullException>(
        () => new RecipeManager(null!));
}
[Fact]
public void SearchByTitle_IsCaseInsensitiveAndPartial()
{
    var manager = CreateManager();

    var results = manager.SearchByTitle("cIpE");

    Assert.Equal(2, results.Count);
    Assert.Contains(results, recipe => recipe.Title == "Recipe A");
    Assert.Contains(results, recipe => recipe.Title == "Recipe B");
}
[Fact]
public void SearchByIngredient_IsCaseInsensitiveAndPartial()
{
    var manager = CreateManager();

    var results = manager.SearchByIngredient("APP");

    Assert.Single(results);
    Assert.Equal("Recipe A", results[0].Title);
}
[Fact]
public void GetHighestProteinRecipes_OrdersResultsAndSkipsMissingNutrition()
{
    var manager = new RecipeManager(new[]
    {
        new Recipe
        {
            Id = 1,
            Title = "Low Protein",
            Nutrition = new NutritionInfo { ProteinG = 10 }
        },
        new Recipe
        {
            Id = 2,
            Title = "High Protein",
            Nutrition = new NutritionInfo { ProteinG = 30 }
        },
        new Recipe
        {
            Id = 3,
            Title = "Unknown Protein",
            Nutrition = null
        }
    });

    var results = manager.GetHighestProteinRecipes(2);

    Assert.Equal(2, results.Count);
    Assert.Equal("High Protein", results[0].Title);
    Assert.Equal("Low Protein", results[1].Title);
}
[Fact]
public void SavedRecipes_AddDuplicateRemoveAndCheck()
{
    var manager = CreateManager();

    Assert.True(manager.AddSavedRecipe(10));
    Assert.True(manager.IsRecipeSaved(10));

    Assert.False(manager.AddSavedRecipe(10));
    Assert.False(manager.AddSavedRecipe(999));

    Assert.True(manager.RemoveSavedRecipe(10));
    Assert.False(manager.IsRecipeSaved(10));
    Assert.False(manager.RemoveSavedRecipe(10));

    Assert.Empty(manager.GetSavedRecipes());
    
}  [Fact]
public void PartBWorkflow_SearchSaveAndRemoveRecipe()
{
    var manager = CreateManager();

    var results = manager.SearchByTitle("Recipe A");

    Assert.Single(results);

    int recipeId = results[0].Id;

    Assert.True(manager.AddSavedRecipe(recipeId));
    Assert.True(manager.IsRecipeSaved(recipeId));

    Assert.True(manager.RemoveSavedRecipe(recipeId));
    Assert.False(manager.IsRecipeSaved(recipeId));
}
  private static RecipeManager CreateManager()
    {
        return new RecipeManager(new[]
        {
            new Recipe
            {
                Id = 10,
                Title = "Recipe A",
                Ingredients = new() { "1 apple" },
                Instructions = new() { "First step", "Second step" }
            },
            new Recipe
            {
                Id = 20,
                Title = "Recipe B"
            }
        });
    }
}
