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
    public Recipe? FindRecipe(int recipeId)
{
    recipes.TryGetValue(recipeId, out Recipe? recipe);
    return recipe;
}
   public bool RemoveRecipe(int recipeId)
{
    if (cookingPlan.Contains(recipeId))
        return false;

    return recipes.Remove(recipeId);
}

    public int AddIngredientsToShoppingList(int recipeId)
{
    Recipe? recipe = FindRecipe(recipeId);

    if (recipe is null)
        return 0;

    foreach (string ingredient in recipe.Ingredients)
    {
        shoppingList.Add(ingredient);
    }

    return recipe.Ingredients.Count;
}

    public IReadOnlyList<string> GetShoppingList()
{
    return shoppingList.AsReadOnly();
}

public void ClearShoppingList()
{
    shoppingList.Clear();
}


    public bool AddRecipeToCookingPlan(int recipeId)
{
    if (!recipes.ContainsKey(recipeId) ||
        cookingPlan.Contains(recipeId))
    {
        return false;
    }

    cookingPlan.AddLast(recipeId);
    return true;
}
public bool RemoveRecipeFromCookingPlan(int recipeId)
{
    var node = cookingPlan.Find(recipeId);

    if (node is null)
        return false;

    cookingPlan.Remove(node);
    removedRecipes.Push(recipeId);
    return true;
}

    public bool RestoreLastRemovedRecipe()
{
    if (removedRecipes.Count == 0)
        return false;

    int recipeId = removedRecipes.Pop();

    if (!recipes.ContainsKey(recipeId) ||
        cookingPlan.Contains(recipeId))
        return false;

    cookingPlan.AddLast(recipeId);
    return true;
}

   public int? PeekLastRemovedRecipe()
{
    if (removedRecipes.Count == 0)
        return null;

    return removedRecipes.Peek();
}

   public IReadOnlyList<int> GetCookingPlan()
{
    return new List<int>(cookingPlan).AsReadOnly();
}
   public bool StartCooking(int recipeId)
{
    Recipe? recipe = FindRecipe(recipeId);

    if (recipe is null || recipe.Instructions.Count == 0)
        return false;

    instructions.Clear();

    foreach (string instruction in recipe.Instructions)
    {
        instructions.Enqueue(instruction);
    }

    return true;
}
public string? PeekNextInstruction()
{
    if (instructions.Count == 0)
        return null;

    return instructions.Peek();
}
    public string? CompleteNextInstruction()
{
    if (instructions.Count == 0)
        return null;

    return instructions.Dequeue();
}

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
