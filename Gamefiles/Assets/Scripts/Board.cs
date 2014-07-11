using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour, IEnumerable<Tile>
{
    Tile[] _tiles;

    /// <summary>
    ///     The prefab of the Tile game component
    /// </summary>
    /// <remarks>
    ///     As a standard the tile prefab must be a 1x1 hexagon, must have a Tile component, and preferably have a collider attached.
    /// </remarks>
    public GameObject TilePrefab;

    void Awake()
    {
        EmptyBoard(12, 6);
    }

    public void EmptyBoard(int width, int height)
    {
        _tiles = new Tile[width * height];
        for (int i = 0; i < width * height; i++)
        {
            GameObject go = Instantiate(TilePrefab) as GameObject;
            go.name = "Tile" + i;

            //The local position within the board
            var localPos = new Vector3(i % width, i / width);

            _tiles[i] = go.GetComponent<Tile>();

            if (localPos.x > 0)
            {
                _tiles[i].AddNeighbor(_tiles[i - 1]);
            }
            if (localPos.y > 0)
            {
                _tiles[i].AddNeighbor(_tiles[i - width]);
                if (localPos.y % 2 == 0 && localPos.x > 0)
                    _tiles[i].AddNeighbor(_tiles[i - width - 1]);
                else if (localPos.y % 2 != 0 && localPos.x != width - 1)
                    _tiles[i].AddNeighbor(_tiles[i - width + 1]);
            }

            if (localPos.y % 2 != 0)
            {
                localPos.x += 0.5f;
            }
            localPos.y *= -1;// -0.8f;

            go.transform.position = transform.position + localPos;
            go.transform.parent = this.transform;
        }
    }

    #region IEnumerable<Tile>
    public IEnumerator<Tile> GetEnumerator()
    {
        int i = _tiles.Length;
        while (i != 0)
        {
            yield return _tiles[i];
            i--;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator() as IEnumerator;
    }
    #endregion
}
