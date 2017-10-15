using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using av = ApplicationVariable;

public class Graph : MonoBehaviour
{
    public GameObject GroundPrefab;
    public GameObject CeilingPrefab;
    public GameObject SpawnPrefab;
    public GameObject BackgroundPrefab;
    public GameObject StartPrefab;
    public GameObject EndPrefab;
    public GameObject collectable;
    public GameObject EnemyPrefab;
    public static IntVec2 size;
    public int x, y;

    private List<GameObject> collectables = new List<GameObject>();
    private List<GameObject> enemyList = new List<GameObject>();
    private GameObject[,] cell;
    private bool[,] isSolid ;

    public void Generate()
    {
        size = new IntVec2(x,y);
        cell = new GameObject[size.x, size.y];
        Step(size);
        SpawnCol(size, EnemyPrefab, enemyList, av.BoolSet.collect);
        SpawnCol(size, collectable, collectables, av.BoolSet.collect);
    }

    private void SpawnCol(IntVec2 cord, GameObject gamePrefab, List<GameObject> gameList, bool boolSet)
    {
        for (int i = 0; i < size.x/2; i++)
        {
            for (int j = 0; j < size.y; j++)
            { SpawnRand(cord, gamePrefab, gameList, boolSet); }
        }
    }

    private void SpawnRand(IntVec2 cord, GameObject gamePrefab, List<GameObject> gameList, bool boolSet)
    {
        int R1 = Random.Range(0, size.x);
        int R2 = Random.Range(0, size.y);
        cord = new IntVec2(R1, R2);
        if (isSolid[R1, R2])
        { CreateDuo(cord, gamePrefab, gameList, boolSet); }
    }

    private void Step(IntVec2 cord)
    {
        isSolid = new bool[x,y];
        for (int i = 0; i < size.x; i++)
        {
          for (int j = 0; j < size.y; j++)
          {
              cord = new IntVec2(i, j);
              SpawnLevel(cord, i, j);
              SpawnPlatform(cord, i, j);
          }
        }
    }

    private void SpawnLevel(IntVec2 cord, int i, int j)
    {
        if (j == 0)
        { CreateLevel(cord, GroundPrefab, av.Names.ground); }

        if (j == size.y - 1)
        { CreateLevel(cord, CeilingPrefab, av.Names.ceiling); }

        if (j <= size.y && i == 0)
        { CreateLevel(cord, StartPrefab, av.Names.start, av.Names.startVal); }

        if (i == size.x - 1)
        { CreateLevel(cord, EndPrefab, av.Names.end, av.Names.endVal); }

        if (j % 13 == 0)
        { CreateLevel(cord, BackgroundPrefab, av.Names.backGround, 0, av.Names.bgVal); }
    }

    private void SpawnPlatform(IntVec2 cord, int i, int j)
    {
        if (i >= 2 && j >= 1 )
        {

            if (i % 2 == 0 && j == 4)
            {
                //CreateSpawn(cord);
                CreateDuo(cord, SpawnPrefab, null, av.BoolSet.platform);
            }
            else if (j == 8 && i % 2 != 0)
            {
                CreateSpawn(cord);
            }
            else if (i > 2 && i % 2 == 0 && j == 12 && i % 4 == 0)
            {
                CreateSpawn(cord);
            }
            else if (j == 16 && i % 2 != 0 && i > 4)
            {
                CreateSpawn(cord);
            }
            else if (j == 20 && i % 2 == 0 && i > 6)
            {
                CreateSpawn(cord);
            }
            else if (j == 24 && i % 2 != 0 && i % 4 != 0 && i > 8)
            {
                CreateSpawn(cord);
            }
            else if (j == 28 && i % 2 == 0 && i % 4 == 0 && i > 10)
            {
                CreateSpawn(cord);
            }
            else if (j == 32 && i % 2 != 0 && i > 12)
            {
                CreateSpawn(cord);
            }
            else if (j == 36 && i % 2 == 0 && i > 14)
            {
                CreateSpawn(cord);
            }
            else if (j == 40 && i % 2 != 0 && i > 16)
            {
                CreateSpawn(cord);
            }
            else if (j == 44 && i % 2 == 0 && i > 18)
            {
                CreateSpawn(cord);
            }
            else if (j == size.y - 1 && i == size.x - 1)
            {

            }
        }
    }

    private void CreateDuo(IntVec2 cord, GameObject duoPrefab, List<GameObject> gameList, bool Solid)
    {
        GameObject createDuo = Instantiate(duoPrefab) as GameObject;
        cell[cord.x, cord.y] = createDuo;
        createDuo.transform.localPosition = new Vector2(cord.x * 11.5f, cord.y + 2f);
        createDuo.transform.parent = transform;
        isSolid[cord.x, cord.y] = Solid;
        gameList.Add(duoPrefab);
    }

    private void CreateSpawn(IntVec2 cord)
    {
        GameObject spawn = Instantiate(SpawnPrefab) as GameObject;
        cell[cord.x, cord.y] = spawn;
        spawn.name = "Spawn: " + cord.x+ ", " + cord.y;
        spawn.transform.localPosition = new Vector2(cord.x*11.5f, cord.y );
        spawn.transform.parent = transform;
        isSolid[cord.x,cord.y]= true;
    }

    private void CreateLevel(IntVec2 cord, GameObject gamePrefab, string name, float space = 0, float back= 0)
    {
        GameObject gameInst = Instantiate(gamePrefab) as GameObject;
        cell[cord.x, cord.y] = gameInst;
        gameInst.name = name + cord.x + cord.y;
        gameInst.transform.localPosition = new Vector2(cord.x*11.5f + space, cord.y + back);
        gameInst.transform.parent = transform; 
    }

}
