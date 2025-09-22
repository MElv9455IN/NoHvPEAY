// 代码生成时间: 2025-09-22 10:54:15
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

// Define the InventoryItem entity
public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

// Define the InventoryContext to interact with the database
public class InventoryContext : DbContext
{
    public DbSet<InventoryItem> InventoryItems { get; set; }
}

public class InventoryManagementSystem
{
    private InventoryContext context;

    public InventoryManagementSystem()
    {
        // Initialize the database context
        context = new InventoryContext();
    }

    // Add a new inventory item
    public void AddInventoryItem(InventoryItem item)
    {
        try
        {
            context.InventoryItems.Add(item);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the database operation
            Console.WriteLine("Error adding inventory item: " + ex.Message);
        }
    }

    // Update an existing inventory item
    public void UpdateInventoryItem(InventoryItem item)
    {
        try
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the database operation
            Console.WriteLine("Error updating inventory item: " + ex.Message);
        }
    }

    // Delete an inventory item
    public void DeleteInventoryItem(int id)
    {
        try
        {
            InventoryItem item = context.InventoryItems.Find(id);
            context.InventoryItems.Remove(item);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the database operation
            Console.WriteLine("Error deleting inventory item: " + ex.Message);
        }
    }

    // List all inventory items
    public List<InventoryItem> ListInventoryItems()
    {
        try
        {
            return context.InventoryItems.ToList();
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the database operation
            Console.WriteLine("Error listing inventory items: " + ex.Message);
            return null;
        }
    }
}
