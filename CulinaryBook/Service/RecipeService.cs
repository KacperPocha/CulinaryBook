using CulinaryBook.Models;
using CulinaryBook.Persistance;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RecipeService
{
    private readonly IAppDbContext _appDbContext;

    public RecipeService(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IEnumerable<Recipe> GetRecipes()
    {
        return _appDbContext.Recipes;
    }

    public async Task<Recipe> CreateRecipe(string title, string category, string ingredients, string directions, string taste, string time)
    {
        var recipe = new Recipe()
        {
            Title = title,
            Category = category,
            Ingredients = ingredients,
            Directions = directions,
            Taste = taste,
            Time = time,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };

        _appDbContext.Recipes.AddOrUpdate(recipe);
        await _appDbContext.SaveChangesAsync();
        return recipe;
    }

    public async Task<Recipe> UpdateRecipe(int id, string title, string category, string ingredients, string directions, string taste, string time)
    {
        var recipe = _appDbContext.Recipes.FirstOrDefault(b => b.Id == id);
        if (recipe != null)
        {
            recipe.Title = title;
            recipe.Category = category;
            recipe.Ingredients = ingredients;
            recipe.Directions = directions;
            recipe.Taste = taste;
            recipe.Time = time;
            recipe.UpdatedDate = DateTime.Now;
            _appDbContext.Recipes.AddOrUpdate(recipe);
            await _appDbContext.SaveChangesAsync();
        }
        return recipe;
    }

    public async Task<bool> DeleteRecipe(int id)
    {
        var recipeToRemove = _appDbContext.Recipes.FirstOrDefault(n => n.Id == id);
        if (recipeToRemove != null)
        {
            _appDbContext.Recipes.Remove(recipeToRemove);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
