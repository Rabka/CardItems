using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class Tile : MonoBehaviour, IEquatable<Tile>
{
    public Color HighlightColor = Color.cyan;

    private List<Tile> Neighbors = new List<Tile>();
    private Color defaultColor = Color.white;

    void Awake()
    {
        defaultColor = renderer.material.color;
    }

    void OnMouseEnter()
    {
        Highlight();
    }

    void OnMouseExit()
    {
        DeHighlight();
    }

    void OnMouseDown()
    {
        StartCoroutine(Wave());
    }

    /// <summary>
    ///     Creates a wave from this tile expanding though it's neighbors until it reaches the end.
    /// </summary>
    IEnumerator Wave()
    {
        List<Tile> waved = new List<Tile>();
        List<Tile> selected = new List<Tile> { this };

        do
        {
            selected.ForEach(t => t.DeHighlight());
            waved.AddRange(selected.Where(t => !waved.Contains(t)));
            selected = selected.SelectMany(t => t.Neighbors.Where(n => !waved.Contains(n) && n.gameObject.activeInHierarchy)).ToList();
            selected.ForEach(t => t.Highlight());
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


    /// <summary>
    ///     Highlights this tile.
    /// </summary>
    void Highlight()
    {
        renderer.material.color = HighlightColor;
    }

    /// <summary>
    ///     Returns this tile to its default color.
    /// </summary>
    void DeHighlight()
    {
        renderer.material.color = defaultColor;
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
