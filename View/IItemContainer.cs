using System;

/// <summary>
/// This interface defines an item container in a view, the container must have a getter to the item it contained,
/// a controller can use this interface to get access to an item displayed in a view, without knowing the detailed
/// implementation of how the container displays the item.
/// </summary>
public interface IItemContainer 
{
    Item Item { get; }
}
