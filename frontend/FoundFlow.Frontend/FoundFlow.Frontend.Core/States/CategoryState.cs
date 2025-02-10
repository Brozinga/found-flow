using FoundFlow.Frontend.Core.Entities.Category;

namespace FoundFlow.Frontend.Core.States;

public class CategoryState
{
    public Category? SelectedCategory { get; private set; }

    public void SetSelectedCategory(string id, string name, string color)
    {
        var selectedCategory = new Category();

        if (!string.IsNullOrEmpty(id))
            selectedCategory.Id = Guid.Parse(id);

        selectedCategory.Name = name;
        selectedCategory.Color = color;

        SelectedCategory = selectedCategory;
    }
}