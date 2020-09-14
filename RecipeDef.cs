using System.Collections.Generic;

namespace RimDef
{
    class RecipeDef : Def
    {
        public List<string[]> ingredients = new List<string[]>();

        public void addIngredients(string[] row)
        {
            this.ingredients.Add(row);
        }
    }
}