using System;
using System.Collections.Generic;
using GridSystemSpace;


public enum CenterConstrainsEnum
{
    NoObjectAtCenter,
    NoObjectAtEdge,
    ObjectAtCenter,
    ObjectAtEdge,
}

public static class CenterConstrains
{
    private static Dictionary<CenterConstrainsEnum, Func<CellNumber, int, GridCell.Place, bool>> constFuncs = new Dictionary<CenterConstrainsEnum, Func<CellNumber, int, GridCell.Place, bool>>()
    {
        { CenterConstrainsEnum.NoObjectAtCenter, NoObjectAtCenter },
        { CenterConstrainsEnum.NoObjectAtEdge, NoObjectAtEdge },
        { CenterConstrainsEnum.ObjectAtCenter, ObjectAtCenter },
        { CenterConstrainsEnum.ObjectAtEdge, ObjectAtEdge }
    };

    public static bool IsCenterValid(CenterConstrainsEnum constrains, CellNumber number, int floor, GridCell.Place modifier) => constFuncs[constrains].Invoke(number, floor, modifier);

    private static bool NoObjectAtCenter(CellNumber number, int floor, GridCell.Place modifier)
    {
        if (modifier == GridCell.Place.Center) return GridSystem.IsPlaceEmpty(number, floor, GridCell.Place.Center);
        CellNumber adj = GridSystem.GetAdjacentCellNumber(number, modifier);
        return GridSystem.IsCellNumberValid(adj) && GridSystem.IsPlaceEmpty(adj, floor, GridCell.Place.Center);
    }

    private static bool NoObjectAtEdge(CellNumber number, int floor, GridCell.Place modifier) => GridSystem.IsPlaceEmpty(number, floor, modifier);

    private static bool ObjectAtCenter(CellNumber number, int floor, GridCell.Place modifier)
    {
        if (modifier == GridCell.Place.Center) return GridSystem.IsPlaceOccupied(number, floor, GridCell.Place.Center);
        CellNumber adj = GridSystem.GetAdjacentCellNumber(number, modifier);
        return GridSystem.IsCellNumberValid(adj) && GridSystem.IsPlaceOccupied(adj, floor, GridCell.Place.Center);
    }

    private static bool ObjectAtEdge(CellNumber number, int floor, GridCell.Place modifier) => GridSystem.IsPlaceOccupied(number, floor, modifier);
}