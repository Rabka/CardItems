using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour, IEnumerable<Tile>
{
    Tile[] _tiles;

    public GameObject TilePrefab;

    void Awake()
    {
        _tiles = new Tile[64];
        for (int i = 0; i < 64; i++)
        {
            GameObject go = Instantiate(TilePrefab) as GameObject; //GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = "Tile" + i;

            var relativePosition = new Vector3(i % 8, -(i / 8));

            if((i/8)%2 != 0)
            {
                relativePosition.x += 0.5f;
            }

            go.transform.position = transform.position + relativePosition;
            go.transform.parent = this.transform;

            _tiles[i] = go.AddComponent<Tile>();

            if(i%8 > 0)
            {
                _tiles[i].AddNeighbor(_tiles[i-1]);
            }
            if(i >= 8)
            {
                _tiles[i].AddNeighbor(_tiles[i-8]);
                if ((i / 8) % 2 == 0 && i % 8 > 0) 
                    _tiles[i].AddNeighbor(_tiles[i - 9]);
                else if((i / 8) % 2 != 0 && i % 8 < 7)
                    _tiles[i].AddNeighbor(_tiles[i - 7]);
            }
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
