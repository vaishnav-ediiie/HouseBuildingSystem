using GridSystemSpace;

public enum EdgeConstrainsEnum
{
    NoObjectAtEitherCenter,
    NoObjectAtBothCenter,
    NoObjectAtEdge,
    ObjectAtEitherCenter,
    ObjectAtBothCenter,
    ObjectAtEdge,
}


public interface IConstrainEdge
{
    public bool IsEdgeValid(CellNumber number, int floor, GridCell.Place edge);
}


public class CE_NoObjectAtEitherCenter : IConstrainEdge
{
    public bool IsEdgeValid(CellNumber number, int floor, GridCell.Place edge)
    {
        if (GridSystem.IsPlaceEmpty(number, floor, GridCell.Place.Center)) return true;

        CellNumber adj = GridSystem.GetAdjacentCellNumber(number, edge);
        return GridSystem.IsCellNumberValid(adj) && GridSystem.IsPlaceEmpty(adj, floor, GridCell.Place.Center);
    }
}

public class CE_NoObjectAtBothCenter : IConstrainEdge
{
    public bool IsEdgeValid(CellNumber number, int floor, GridCell.Place edge)
    {
        if (GridSystem.IsPlaceOccupied(number, floor, GridCell.Place.Center)) return false;

        CellNumber adj = GridSystem.GetAdjacentCellNumber(number, edge);
        return GridSystem.IsCellNumberValid(adj) && GridSystem.IsPlaceEmpty(adj, floor, GridCell.Place.Center);
    }
}

public class CE_NoObjectAtEdge : IConstrainEdge
{
    public bool IsEdgeValid(CellNumber number, int floor, GridCell.Place edge) => GridSystem.IsPlaceEmpty(number, floor, edge);
}

public class CE_ObjectAtEitherCenter : IConstrainEdge
{
    public bool IsEdgeValid(CellNumber number, int floor, GridCell.Place edge)
    {
        if (GridSystem.IsPlaceOccupied(number, floor, GridCell.Place.Center)) return true;

        CellNumber adj = GridSystem.GetAdjacentCellNumber(number, edge);
        return GridSystem.IsCellNumberValid(adj) && GridSystem.IsPlaceOccupied(adj, floor, GridCell.Place.Center);
    }
}

public class CE_ObjectAtBothCenter : IConstrainEdge
{
    public bool IsEdgeValid(CellNumber number, int floor, GridCell.Place edge)
    {
        if (GridSystem.IsPlaceEmpty(number, floor, GridCell.Place.Center)) return false;

        CellNumber adj = GridSystem.GetAdjacentCellNumber(number, edge);
        return GridSystem.IsCellNumberValid(adj) && GridSystem.IsPlaceOccupied(adj, floor, GridCell.Place.Center);
    }
}

public class CE_ObjectAtEdge : IConstrainEdge
{
    public bool IsEdgeValid(CellNumber number, int floor, GridCell.Place edge) => GridSystem.IsPlaceOccupied(number, floor, edge);
}