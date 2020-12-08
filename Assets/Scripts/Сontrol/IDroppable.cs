
public interface IDroppable
{
    bool CanDrop(IDraggable draggable);
    bool TryDrop(IDraggable draggable);
}
