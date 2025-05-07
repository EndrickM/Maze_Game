using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class MazeGeneratorTilemap : MonoBehaviour
{
    public int width = 16;
    public int height = 16;

    public Tilemap floorTilemap;
    public Tilemap wallTilemap;
    public TileBase floorTile;
    public TileBase wallTile;
    public GameObject Exit;
    public GameObject playerInicial;

    private bool[,] visited;
    private List<Vector3Int> floorPositions = new List<Vector3Int>(); 

    private Vector2Int[] directions = new Vector2Int[]
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    private void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        visited = new bool[width, height];
        bool[,] maze = new bool[width * 2 + 1, height * 2 + 1];

        for (int x = 0; x < maze.GetLength(0); x++)
            for (int y = 0; y < maze.GetLength(1); y++)
                maze[x, y] = false;

        CarveMaze(1, 1, maze);
        DrawMaze(maze);
        PlacePlayerAndExit(); 
    }

    void CarveMaze(int x, int y, bool[,] maze)
    {
        visited[x / 2, y / 2] = true;
        maze[x, y] = true;

        var dirs = new List<Vector2Int>(directions);
        Shuffle(dirs);

        foreach (var dir in dirs)
        {
            int nx = x + dir.x * 2;
            int ny = y + dir.y * 2;

            if (nx > 0 && ny > 0 && nx < maze.GetLength(0) && ny < maze.GetLength(1) && !visited[nx / 2, ny / 2])
            {
                maze[x + dir.x, y + dir.y] = true;
                CarveMaze(nx, ny, maze);
            }
        }
    }

    void DrawMaze(bool[,] maze)
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
        floorPositions.Clear(); 

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                int px = x * 2;
                int py = y * 2;

                Tilemap targetTilemap = maze[x, y] ? floorTilemap : wallTilemap;
                TileBase tileToSet = maze[x, y] ? floorTile : wallTile;

                Vector3Int tilePos = new Vector3Int(px, py, 0);
                targetTilemap.SetTile(tilePos, tileToSet);
                targetTilemap.SetTile(new Vector3Int(px + 1, py, 0), tileToSet);
                targetTilemap.SetTile(new Vector3Int(px, py + 1, 0), tileToSet);
                targetTilemap.SetTile(new Vector3Int(px + 1, py + 1, 0), tileToSet);

                if (maze[x, y])
                {
                    floorPositions.Add(tilePos); 
                }
            }
        }
    }

    void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }

private GameObject currentPlayer;
private GameObject currentExit;

void PlacePlayerAndExit()
{
    if (floorPositions.Count < 2) return;

    Vector3Int start = floorPositions[0];
    Vector3Int farthest = start;
    float maxDist = 0f;

    foreach (var pos in floorPositions)
    {
        float dist = Vector3Int.Distance(start, pos);
        if (dist > maxDist)
        {
            maxDist = dist;
            farthest = pos;
        }
    }

    Vector3 startWorld = floorTilemap.CellToWorld(start) + new Vector3(0.5f, 0.5f, 0);
    Vector3 exitWorld = floorTilemap.CellToWorld(farthest) + new Vector3(0.5f, 0.5f, 0);
    currentPlayer = Instantiate(playerInicial, startWorld, Quaternion.identity);
    currentPlayer.tag = "Player";
    currentExit = Instantiate(Exit, exitWorld, Quaternion.identity);
    currentExit.tag = "Exit";

    if (currentPlayer != null)
        Destroy(currentPlayer);

    //if (currentExit != null)
    //    Destroy(currentExit);

    Debug.Log("Saída criada em: " + exitWorld);
}


}
