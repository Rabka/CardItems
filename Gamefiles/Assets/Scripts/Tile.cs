using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Tile : MonoBehaviour, IEquatable<Tile>
{
    /// <summary>
    ///     The neighbors of this tile
    /// </summary>
    private List<Tile> Neighbors = new List<Tile>();

    void OnMouseEnter()
    {
        HighLight();
        Neighbors.ForEach(n => n.HighLight());
    }

    void OnMouseExit()
    {
        DeHighLight();
        Neighbors.ForEach(n => n.DeHighLight());
    }

    void OnMouseDown()
    {
        StartCoroutine(Wave());
    }

    IEnumerator Wave()
    {
        List<Tile> waved = new List<Tile>();
        List<Tile> selected = new List<Tile>{this};

        do
        {
            selected.ForEach(t => t.DeHighLight());
            selected = selected.SelectMany(t => t.Neighbors.Where(n => !waved.Contains(n) && n.gameObject.activeInHierarchy)).ToList();
            selected.ForEach(t => t.HighLight());
            waved.AddRange(selected);
            yield return new WaitForSeconds(.1f);
        } while (selected.Count != 0);
    }

    /// <summary>
    ///     Adds a connection between the tiles.
    /// </summary>
    /// <param name="neighbor">The neighbor.</param>
    public void AddNeighbor(Tile neighbor)
    {
        Neighbors.Add(neighbor);
        neighbor.Neighbors.Add(this);
    }

    void HighLight()
    {
        renderer.material.color = Color.cyan;
    }
    void DeHighLight()
    {
        renderer.material.color = Color.white;
    }

    /// <summary>
    ///     Indicates whether the current tile is equal to another tile.
    /// </summary>
    /// <param name="other">A tile to compare with this tile.</param>
    /// <returns>
    ///     True if the current tile is equal to the <paramref name="other" /> parameter; otherwise, false.
    /// </returns>
    public bool Equals(Tile other)
    {
        if(ReferenceEquals(other, null))
            return false;
        if(ReferenceEquals(this, other))
            return true;
        
        return false;
    }
}
