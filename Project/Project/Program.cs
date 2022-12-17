using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Project
{
    public class Program
    {
        public static SortedDictionary<string, List<string>> Categories = new SortedDictionary<string, List<string>>(); //Global Categories sorted dict, for ease of access.

        public static void Main()
        {
            //Define reused variables
            string line = "";
            string mainMenuIntro = "Please select an option from the following list: \n" +
                "1. Create a new Category. \n" +
                "2. Remove a Category. \n" +
                "3. Add a todo to a Category. \n" +
                "4. Sort todos in a Category. \n" +
                "5. Remove a todo from a Category. \n" +
                "6. Clear all todos from a Category. \n" +
                "7. Exit.";
            //Start main application loop
            do
            {
                Console.WriteLine(mainMenuIntro);
                line = Console.ReadLine();
                if(line == "1")
                {
                    CreateCategory();
                }
                else if(line == "2")
                {
                    string category = GetCategory();
                    if (category != "")
                    {
                        RemoveCategory(category);
                    }
                    else
                    {
                        Console.WriteLine("There are no categories. Please create one then try again.");
                    }
                }
                else if (line == "3")
                {
                    string category = GetCategory();
                    if (category != "")
                    {
                        AddTodo(category);
                    }
                    else
                    {
                        Console.WriteLine("There are no categories. Please create one then try again.");
                    }
                }
                else if (line == "4")
                {
                    string category = GetCategory();
                    if (category != "")
                    {
                        SortTodos(category);
                    }
                    else
                    {
                        Console.WriteLine("There are no categories. Please create one then try again.");
                    }
                }
                else if (line == "5")
                {
                    string category = GetCategory();
                    if(category != "")
                    {
                        RemoveTodo(category);
                    }
                    else
                    {
                        Console.WriteLine("There are no categories. Please create one then try again.");
                    }
                }
                else if (line == "6")
                {
                    string category = GetCategory();
                    if (category != "")
                    {
                        ClearTodos(category);
                    }
                    else
                    {
                        Console.WriteLine("There are no categories. Please create one then try again.");
                    }
                }
                else if(line != "7")
                {
                    //Catch unknown
                    Console.WriteLine("Error, unknown request.");
                }

            } while (line != "7");
        }

        /// <summary>
        /// Prompts the user for a title of category, then saves it.
        /// </summary>
        public static void CreateCategory()
        {
            bool created = false;
            do
            {
                Console.WriteLine("Please enter the category name: ");
                string line = Console.ReadLine();
                if (line != "" && line != null)
                {
                    Categories.Add(line, new List<string>());
                    Console.WriteLine("Successfully added new category " + line);
                    created = true;
                }
                else
                {
                    //Catch unknown
                    Console.WriteLine("Error, please enter a category.");
                }
            } while (!created);
            
        }

        /// <summary>
        /// Presents the current list of categories then once one is chosen, returns the name of the category.
        /// </summary>
        /// <returns> The name of the category selected. </returns>
        public static string GetCategory()
        {
            string requestedCategory = "";
            if(Categories.Count > 0)
            {
                Console.WriteLine("What category would you like to select: ");
                string todos = "";
                int count = 0;
                foreach (KeyValuePair<string, List<string>> category in Categories)
                {
                    count++;
                    todos = todos + "\n" + count.ToString() + ". " + category.Key;
                }

                bool selected = false;
                do
                {
                    Console.WriteLine(todos);
                    string line = Console.ReadLine();
                    count = 0;
                    foreach (KeyValuePair<string, List<string>> category in Categories)
                    {
                        count++;
                        if (line == count.ToString())
                        {
                            requestedCategory = category.Key;
                            selected = true;
                        }
                    }
                    if (!selected)
                    {
                        //Catch unknown
                        Console.WriteLine("Error, do not recognize the requested option. Please try again.");
                    }
                } while (!selected);
            }
            return requestedCategory;
        }

        /// <summary>
        /// Check for certaintity of removal, then does so if there are any to remove and the category has no todos.
        /// </summary>
        /// <param name="category">The category to grab the list of todos from</param>
        public static void RemoveCategory(string category)
        {
            if(Categories.Count() > 0)
            {
                if (Categories[category].Count() == 0)
                {
                    Console.WriteLine("Are you sure you want to delete " + category + ", y/n: ");
                    string line = Console.ReadLine().ToLower();
                    do
                    {
                        if (line == "y" || line == "yes")
                        {
                            Categories.Remove(category);
                            Console.WriteLine("Category successfully removed.");
                        }
                        else if (line != "n" && line != "no")
                        {
                            //Catch unknown
                            Console.WriteLine("Please select one of the two options: y/yes, or n/no.");
                        }
                    } while (line != "n" || line == "no");
                    Console.WriteLine("Returning to main menu...");
                }
                else
                {
                    Console.WriteLine("Error, cannot delete categories with todo items. Please remove them before deleting.");
                }
            }
        }

        /// <summary>
        /// Adds a todo to a the requested category's todo list
        /// </summary>
        /// <param name="category">The category to grab the list of todos from</param>
        public static void AddTodo(string category)
        {
            string line = "";
            do
            {
                Console.WriteLine("What to do would you like to add: ");
                line = Console.ReadLine();
                Categories[category].Add(line);
                Console.WriteLine("Would you like to add another to do, y/n: ");
                line = Console.ReadLine();
                if (line.ToLower() != "y" && line.ToLower() != "yes" && line.ToLower() != "n" && line.ToLower() != "no")
                {
                    //Catch unknown
                    Console.WriteLine("Please select one of the two options: y/yes, or n/no");
                }
            }while (line != "n" || line == "no");
            Console.WriteLine("Returning to main menu...");
        }

        /// <summary>
        /// Sorts the requested categories list of todo strings by the requested sorting method
        /// </summary>
        /// <param name="category">The category to grab the list of todos from</param>
        public static void SortTodos(string category)
        {
            if (Categories[category].Count() > 0)
            {
                string line = "";
                do
                {
                    Console.WriteLine("How would you like to sort todos: \n" +
                    "1. Alphabetically \n" +
                    "2. By Length. \n" +
                    "3. Return to Main Menu.");
                    line = Console.ReadLine();
                    if (line == "1")
                    {
                        Categories[category].OrderBy(s => s).ToList();
                        Console.WriteLine("Successfully sorted!~");
                        line = "3";
                    }
                    else if (line == "2")
                    {
                        Categories[category].OrderBy(s => s.Length).ToList();
                        Console.WriteLine("Successfully sorted!~");
                        line = "3";
                    }
                    else
                    {
                        //Catch unknown
                        Console.WriteLine("Error, do not recognize the requested option. Please try again.");
                    }
                } while (line != "3");
            }
            else
            {
                Console.WriteLine("There are no todos for this category. Please create some then try again.");
            }
        }

        /// <summary>
        /// Presents the current todos to the user for the requested category if any exist, then deletes that todo.
        /// </summary>
        /// <param name="category">The category to grab the list of todos from</param>
        public static void RemoveTodo(string category)
        {
            if(Categories[category].Count() > 0)
            {
                Console.WriteLine("What todo would you like to remove: ");
                string todos = "";
                int count = 0;
                foreach(string todo in Categories[category])
                {
                    count++;
                    todos += "\n" + count.ToString() + ". " + todo;
                }
                bool selected = false;
                do
                {
                    Console.WriteLine(todos);
                    string line = Console.ReadLine();
                    count = 0;
                    int indexToPop = -1;

                    foreach(string todo in Categories[category])
                    {
                        count++;
                        if (line == count.ToString())
                        {
                            indexToPop = count;
                            selected = true;
                        }
                    }
                    if (selected && indexToPop != -1)
                    {
                        Categories[category].RemoveAt(indexToPop);
                        Console.WriteLine("Successfully removed!~");
                    }
                    else
                    {
                        //Catch unknown
                        Console.WriteLine("Error, do not recognize the requested option. Please try again.");
                    }
                } while (!selected);
            }
            else
            {
                Console.WriteLine("There are no todos for this category. Please create some then try again.");
            }
        }

        /// <summary>
        /// Requests confirmation of deletion if any todos exist, then deletes the 
        /// </summary>
        /// <param name="category">The category to grab the list of todos from</param>
        public static void ClearTodos(string category)
        {
            if (Categories[category].Count() > 0)
            {
                Console.WriteLine("Are you sure you want to clear " + category + "'s todos, y/n: ");
                string line = Console.ReadLine().ToLower();
                bool deleted = false;
                do
                {
                    if (line == "y" || line == "yes")
                    {
                        Categories[category] = new List<string>();
                        deleted = true;
                        Console.WriteLine("Todos successfully removed.");
                    }
                    else
                    {
                        //Catch unknown
                        Console.WriteLine("Please select one of the two options: y/yes, or n/no");
                    }
                } while (line != "n" && line == "no" && !deleted);
                Console.WriteLine("Returning to main menu...");
            }
            else
            {
                Console.WriteLine("There are no todos for this category. Please create some then try again.");
            }
        }
    }
}
