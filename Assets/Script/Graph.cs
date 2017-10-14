using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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
    public int x, y;
    public GameObject[] collectT = new GameObject[10];

    private List<GameObject> collectables = new List<GameObject>();
    private List<GameObject> enemyList = new List<GameObject>();
    private GameObject[] enemyArray = new GameObject[10];
    private int enemyAmount=0;
    private GameObject[,] cell;
    private bool[,] isSolid ;
    public static IntVec2 size;
    public static int sizeTo=1;


    public void Generate()
    {
        size = new IntVec2(x,y);
        cell = new GameObject[size.x, size.y];
        Step(size);
        SpawnStep(size);
        CollectStep(size);
        EnemySet(size);
        BackgroundStep(size);
    }

    private void EnemySet(IntVec2 cord)
    {
      if( enemyList.Count< size.x)
      {
        for (int i = 0; i < size.x/2; i++)
        {
          for (int j = 0; j < size.y; j++)
           {
            int R1 = Random.Range(0, size.x);
            int R2 = Random.Range(0, size.y);
            cord = new IntVec2(R1, R2);
            if ( isSolid[R1,R2] )
            {
                        //CreateEnemy(cord);
                        CreateDuo(cord, EnemyPrefab, enemyList);
            }
          }
        }
      }
    }


    private void CollectStep(IntVec2 cord)
    {
      //if (collectables.Count < 10)
    //  {
        for (int i = 0; i < size.x /2; i++)
        {
          for (int j = 0; j < size.y; j++)
          {
            int R1 = Random.Range(0,size.x);
            int R2 = Random.Range(0, size.y);
            cord = new IntVec2(R1, R2);
            if (isSolid[R1,R2])
            {
                    //CreateCollect(cord);
                    CreateDuo(cord,collectable,collectables);
            }
          }
        }
      //}
    }


    private void BackgroundStep(IntVec2 cord)
    {
      int sizeH = size.y / 2;
      for (int i = 0; i < size.x; i++)
      {
        for (int j = 0; j < size.y; j++)
        {
          if (j % 13 ==0)
          {
            cord = new IntVec2(i, j);
            CreateBackground(cord);
          }
        }
      }
    }

    private void Step(IntVec2 cord)
    {
        isSolid = new bool[x,y];
        for (int i = 0; i < size.x; i++)
        {
          for (int j = 0; j < size.y; j++)
          {
              cord = new IntVec2(i, j);

              if (j == 0)
              { CreateFloor(cord);}

              if (j == size.y - 1)
              { CreateCeiling(cord); }

              if( j <= size.y && i == 0 )
              { CreateStart(cord); }

              if( i == size.x - 1 )
              { CreateEnd(cord); }
           }
        }
    }



    private void SpawnStep(IntVec2 cord)
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y-4; j++)
            {
                cord = new IntVec2(i,j);
                if (i >= 2 && j >= 1)
                {

                    if ( i % 2==0 && j == 4 )
                    {
                        CreateSpawn(cord);
                    }
                    else if (j == 8 && i % 2 != 0 ) {
                        CreateSpawn(cord);
                    }
                    else if( i > 2 && i % 2 == 0 && j == 12 && i % 4 ==0 ){
                        CreateSpawn(cord);
                    }
                    else if ( j == 16 && i % 2 != 0  && i > 4 ) {
                        CreateSpawn(cord);
                    }
                    else if ( j == 20 && i % 2 == 0 && i> 6 ) {
                      CreateSpawn(cord);
                    }
                    else if (j == 24 && i % 2 != 0 && i % 4 !=0  && i > 8 ) {
                      CreateSpawn(cord);
                    }
                    else if (j == 28 && i % 2 == 0 && i % 4 == 0 && i > 10 ) {
                      CreateSpawn(cord);
                    }
                    else if (j == 32 && i % 2 != 0  && i > 12 ) {
                      CreateSpawn(cord);
                    }
                    else if (j == 36 && i % 2 == 0  && i > 14 ) {
                      CreateSpawn(cord);
                    }
                    else if (j == 40 && i % 2 != 0  && i > 16 ) {
                      CreateSpawn(cord);
                    }
                    else if (j == 44 && i % 2 == 0  && i > 18 ) {
                      CreateSpawn(cord);
                    }
                    else if (j == size.y -1 && i == size.x-1 ) {

                    }
                  }
            }
        }
    }

    private void CreateEnemy(IntVec2 cord)
    {
        GameObject enemy = Instantiate(EnemyPrefab) as GameObject;
        cell[cord.x, cord.y] = enemy;
        enemy.name = "Enemy: " + cord.x + ", " + cord.y;
        enemy.transform.localPosition = new Vector2(cord.x * 11.5f, cord.y + 2f);
        enemy.transform.parent = transform;
        isSolid[cord.x, cord.y] = false;
        enemyList.Add(EnemyPrefab);
    }

    private void CreateCollect(IntVec2 cord)
    {
        GameObject collect = Instantiate(collectable) as GameObject;
        cell[cord.x, cord.y] = collect;
        collect.transform.localPosition = new Vector2(cord.x * 11.5f, cord.y+2f);
        collect.transform.parent = transform;
        isSolid[cord.x, cord.y] = false;
        collectables.Add(collectable);
    }

    private void CreateDuo(IntVec2 cord, GameObject duoPrefab, List<GameObject> gameList)
    {
        GameObject createDuo = Instantiate(duoPrefab) as GameObject;
        cell[cord.x, cord.y] = createDuo;
        createDuo.transform.localPosition = new Vector2(cord.x * 11.5f, cord.y + 2f);
        createDuo.transform.parent = transform;
        isSolid[cord.x, cord.y] = false;
        gameList.Add(duoPrefab);
    }

    private void CreateFloor(IntVec2 cord)
    {
        GameObject floor = Instantiate(GroundPrefab) as GameObject; //Create Instance of the Floor
        cell[cord.x, cord.y] = floor;
        floor.name = "Floor: " + cord.x +", "+ cord.y;     //Label for the Scene Manager
        floor.transform.localPosition = new Vector2(cord.x*11.5f,cord.y); //Use of Cord.x * value (Scaler) made spacing
        floor.transform.parent = transform; //Set to all to tree under the Graph

    }

    private void CreateStart(IntVec2 cord)
    {
        GameObject start = Instantiate(StartPrefab) as GameObject;
        cell[cord.x, cord.y] = start;
        start.name = "Start: " + cord.x + ", "+ cord.y;
        start.transform.localPosition = new Vector2(cord.x*11.5f-5f, cord.y);
        start.transform.parent = transform;
    }

    private void CreateEnd(IntVec2 cord)
    {
        GameObject end = Instantiate(StartPrefab) as GameObject;
        cell[cord.x, cord.y] = end;
        end.name = "end: " + cord.x + ", "+ cord.y;
        end.transform.localPosition = new Vector2(cord.x*11.5f+5f, cord.y);
        end.transform.parent = transform;
    }

    private void CreateCeiling(IntVec2 cord)
    {
        GameObject ceiling = Instantiate(CeilingPrefab) as GameObject;
        cell[cord.x, cord.y] = ceiling;
        ceiling.name = "Ceiling: " + cord.x + ", "+ cord.y;
        ceiling.transform.localPosition = new Vector2(cord.x * 11.5f, cord.y);
        ceiling.transform.parent = transform;
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

    private void CreateBackground(IntVec2 cord)
    {
        GameObject background = Instantiate(BackgroundPrefab) as GameObject;
        cell[cord.x, cord.y] = background;
        background.name = "Background: " + cord.x + ", " + cord.y;
        background.transform.localPosition = new Vector2(cord.x* 11.5f, cord.y+6f);
        background.transform.parent = transform;
    }

}
