using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] public Tilemap map;
    [SerializeField] private List<TileData> tileDataList;
    private Dictionary<TileBase, TileData> tileDataDict;

    private void Awake()
    {
        tileDataDict = new Dictionary<TileBase, TileData>();
        foreach (var tileData in tileDataList)
        {
            foreach (var tile in tileData.tiles)
            {
                tileDataDict.Add(tile, tileData);
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPos = map.WorldToCell(mousePos);
            TileBase clickedTile = map.GetTile(gridPos);
            float damage = tileDataDict[clickedTile].damage;
            print("At pos " + gridPos + " there is a " + clickedTile + " with a damage of " + damage);
        }
    }

    public float GetTileDamage(Vector2 worldPos)
    {
        Vector3Int gridPos = map.WorldToCell(worldPos);
        print("g:"+gridPos);
        var x = Mathf.RoundToInt(worldPos.x);
        var y = Mathf.RoundToInt(worldPos.y);
        var tilePos = new Vector3Int(x, y, 0);
        print("t:"+tilePos);
        TileBase tile = map.GetTile(tilePos);
        if (tile == null)
        {
            print("tile not in map manager");
            return 0f;
        }

        float damage = tileDataDict[tile].damage;
        return damage;
    }
}
