# Design



## 1. Saved recipe collection
I chose `HashSet<int>` to store saved recipe IDs because each recipe has a unique ID and the order of saved recipes does not matter. The `Add()` method prevents duplicate IDs, while `Remove()` deletes a saved recipe. The `Contains()` method checks whether a recipe is already saved. These operations are efficient, with average time complexity of O(1).

## 2. Integration

Building on the existing `RecipeManager` from Part A, this update lets users search through the recipe catalogue by title or ingredient using LINQ. To keep track of saved recipes without running into duplicate entries, it uses a `HashSet<int>` for the recipe IDs. There's also a new protein report that sorts recipes by protein content while filtering out anything missing nutrition data. Everything from Part A still works as expected alongside these new features..

## 3. Basic complexity

| Operation | Structure | Expected complexity | Reason |
| --- | --- | --- | --- |
| Lookup recipe by ID | Dictionary | Average O(1) | Hash-based key lookup. |
| Traverse cooking plan | LinkedList | O(n) | Each planned recipe may need to be visited. |
| Complete next instruction | Queue | O(1) | The item at the front is removed. |
| LINQ title/ingredient search | Recipe collection | O(n) | Each recipe may need to be inspected. |
| Check whether a recipe is saved | HashSet<int> | Average O(1) | Hash-based membership lookup. |
