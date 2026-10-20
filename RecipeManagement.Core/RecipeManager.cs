using System;
using System.Collections.Generic;

namespace RecipeManagement.Core;

/// <summary>
/// Implement this class using the five Part A collections as private fields:
/// Dictionary&lt;int, Recipe&gt;, List&lt;string&gt;, LinkedList&lt;int&gt;,
/// Stack&lt;int&gt; and Queue&lt;string&gt;.
/// </summary>
public sealed class RecipeManager : IRecipeManager
{
    private readonly Dictionary<int, Recipe> recipes = new();
private readonly List<string> shoppingList = new();
private readonly LinkedList<int> cookingPlan = new();
private readonly Stack<int> removedRecipes = new();
private readonly Queue<string> instructions = new();

    public RecipeManager(IEnumerable<Recipe> recipes)
    {
        if (recipes is null)
{
    throw new ArgumentNullException(nameof(recipes));
}

foreach (Recipe recipe in recipes)
{
    if (recipe.Id <= 0 ||
        string.IsNullOrWhiteSpace(recipe.Title) ||
        this.recipes.ContainsKey(recipe.Id))
    {
        throw new ArgumentException("Invalid or duplicate recipe.", nameof(recipes));
    }

    this.recipes.Add(recipe.Id, recipe);
}
       
    }

 
public int RecipeCount => recipes.Count;
public int ShoppingItemCount => shoppingList.Count;
public int CookingPlanCount => cookingPlan.Count;
public int PendingInstructionCount => instructions.Count;
public int RemovedRecipeCount => removedRecipes.Count;
    
public bool AddRecipe(Recipe recipe)
{
    if (recipe is null)
        throw new ArgumentNullException(nameof(recipe));

    if (recipe.Id <= 0 ||
        string.IsNullOrWhiteSpace(recipe.Title) ||
        recipes.ContainsKey(recipe.Id))
        return false;

    recipes.Add(recipe.Id, recipe);
    return true;
}
    public Recipe? FindRecipe(int recipeId) =>
        throw new NotImplementedException("Part A: implement FindRecipe.");

    public bool RemoveRecipe(int recipeId) =>
        throw new NotImplementedException("Part A: implement RemoveRecipe.");

    public int AddIngredientsToShoppingList(int recipeId) =>
        throw new NotImplementedException("Part A: implement AddIngredientsToShoppingList.");

    public IReadOnlyList<string> GetShoppingList() =>
        throw new NotImplementedException("Part A: implement GetShoppingList.");

    public void ClearShoppingList() =>
        throw new NotImplementedException("Part A: implement ClearShoppingList.");

    public bool AddRecipeToCookingPlan(int recipeId) =>
        throw new NotImplementedException("Part A: implement AddRecipeToCookingPlan.");

    public bool RemoveRecipeFromCookingPlan(int recipeId) =>
        throw new NotImplementedException("Part A: implement RemoveRecipeFromCookingPlan.");

    public bool RestoreLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement RestoreLastRemovedRecipe.");

    public int? PeekLastRemovedRecipe() =>
        throw new NotImplementedException("Part A: implement PeekLastRemovedRecipe.");

    public IReadOnlyList<int> GetCookingPlan() =>
        throw new NotImplementedException("Part A: implement GetCookingPlan.");

    public bool StartCooking(int recipeId) =>
        throw new NotImplementedException("Part A: implement StartCooking.");

    public string? PeekNextInstruction() =>
        throw new NotImplementedException("Part A: implement PeekNextInstruction.");

    public string? CompleteNextInstruction() =>
        throw new NotImplementedException("Part A: implement CompleteNextInstruction.");

    public IReadOnlyList<Recipe> SearchByTitle(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByTitle.");

    public IReadOnlyList<Recipe> SearchByIngredient(string searchText) =>
        throw new NotImplementedException("Part B: implement SearchByIngredient.");

    public IReadOnlyList<Recipe> GetHighestProteinRecipes(int count) =>
        throw new NotImplementedException("Part B: implement GetHighestProteinRecipes.");

    public bool AddSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement AddSavedRecipe.");

    public bool RemoveSavedRecipe(int recipeId) =>
        throw new NotImplementedException("Part B: implement RemoveSavedRecipe.");

    public bool IsRecipeSaved(int recipeId) =>
        throw new NotImplementedException("Part B: implement IsRecipeSaved.");

    public IReadOnlyList<int> GetSavedRecipes() =>
        throw new NotImplementedException("Part B: implement GetSavedRecipes.");
}
