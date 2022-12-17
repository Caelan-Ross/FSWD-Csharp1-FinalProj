# EXSM3941: C# Fundamentals - Final Project

This is a C# console application that allows users to create and manage categories of to-do items. 

It has the following features:

    - Create a new category
    - Remove a category
    - Add a to-do item to a category
    - Sort the to-do items in a category
    - Remove a to-do item from a category
    - Clear all to-do items from a category

The main menu prompts the user to select an option from that list of options. The program is designed to handle user input and potential errors, such as attempting to remove a non-existent category or to-do item.

The program uses the SortedDictionary class to store the categories, which are sorted alphabetically by their titles for ease of access. It also utilizes the List class to store the to-do items within each category.