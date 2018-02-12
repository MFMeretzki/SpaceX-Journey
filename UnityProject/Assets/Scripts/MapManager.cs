using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour {

    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private PlanetFactory planetFactory;

    private const float CHUNK_SIZE = 40.0f;
    private const float MIN_DIST = 15.0f;
    private const float DIST_TO_BORDER = 7.5f;
    private const float MIN_INIT_DIST = 7.0f;

    private const int NUM_PLANETS = 6;
    private const int MAX_ITERATIONS = 20;

    private const int MAX_CHUNKS = 75;
    private const int CHUNKS_CLEANED_AT_A_TIME = 15;

    private float chunkSizeInverse = 1.0f / CHUNK_SIZE;
    float halfChunkSize = CHUNK_SIZE / 2 - DIST_TO_BORDER;
    float minDist2 = MIN_DIST * MIN_DIST;

    /* Chunk names convention cp(x)m(y) (examples: cp3p1, cm1p4, cp0m6, cm3m1...)
     * name string starts with 'c' (chunk)
     * followed by 'p'(plus) or 'm'(minus) and the index of the given coordinate (x, y)
     */
    private Dictionary<string, GameObject> chunks;

    public void Awake ()
    {
        chunks = new Dictionary<string, GameObject>();
    }

    public void Start ()
    {
        Vector3 pos = Vector3.zero;
        CreateChunk(pos, true);

        pos = new Vector3(0.0f, 1,  0.0f);
        CreateChunk(pos, false);
        pos = new Vector3(1, 1, 0.0f);
        CreateChunk(pos, false);
        pos = new Vector3(1, 0.0f, 0.0f);
        CreateChunk(pos, false);
        pos = new Vector3(1, -1, 0.0f);
        CreateChunk(pos, false);

        pos = new Vector3(0.0f, -1, 0.0f);
        CreateChunk(pos, false);
        pos = new Vector3(-1, -1, 0.0f);
        CreateChunk(pos, false);
        pos = new Vector3(-1, 0.0f, 0.0f);
        CreateChunk(pos, false);
        pos = new Vector3(-1, 1, 0.0f);
        CreateChunk(pos, false);
    }
    public void Update ()
    {
        List<Vector3> neighbours = GetNeighbourPositionsDiscretized(gameController.ShipPosition);
        List<Vector3>.Enumerator en = neighbours.GetEnumerator();
        while (en.MoveNext())
        {
            CreateChunk(en.Current, false);
        }
    }

    private void CreateChunk (Vector3 position, bool isInit)
    {
        string name = ChunkName(position.x, position.y);
        
        if (!chunks.ContainsKey(name))
        {
            GameObject chunk = new GameObject(name);
            chunk.transform.position = position * CHUNK_SIZE;
            List<Vector3> planetPositions = GeneratePositions();
            if (isInit)
            {
                planetPositions = CheckInitValid(planetPositions);
            }
            GeneratePlanets(chunk, planetPositions);
            chunks.Add(name, chunk);

            if(chunks.Count > MAX_CHUNKS)
            {
                CleanChuncks();
            }
        }
    }

    private void GeneratePlanets (GameObject chunk, List<Vector3> positions)
    {

        GameObject planet;
        List<Vector3>.Enumerator posEn = positions.GetEnumerator();
        while (posEn.MoveNext())
        {
            planet = this.planetFactory.BuildPlanet(posEn.Current + chunk.transform.position);
            planet.transform.SetParent(chunk.transform, true);
        }

    }

    private List<Vector3> GeneratePositions ()
    {
        List<Vector3> positions = new List<Vector3>();

        Vector3 pos;
        int planetCount = 0;
        int i = 0;
        while (planetCount <= NUM_PLANETS && i <= MAX_ITERATIONS)
        {
            pos = new Vector3(
                Random.Range(-halfChunkSize, halfChunkSize),
                Random.Range(-halfChunkSize, halfChunkSize),
                0.0f
                );

            List<Vector3>.Enumerator en = positions.GetEnumerator();
            bool ok = true;
            Vector3 dist;
            while (en.MoveNext() && ok == true)
            {
                dist = en.Current - pos;
                if (dist.sqrMagnitude < minDist2)
                {
                    ok = false;
                }
            }
            if (ok)
            {
                positions.Add(pos);
                planetCount++;
            }
            i++;
        }

        return positions;
    }

    private List<Vector3> CheckInitValid(List<Vector3> positions)
    {
        List<Vector3> list = new List<Vector3>();
        List<Vector3>.Enumerator en = positions.GetEnumerator();
        float dist2 = MIN_INIT_DIST * MIN_INIT_DIST;
        while (en.MoveNext())
        {
            if(en.Current.sqrMagnitude >= dist2)
            {
                list.Add(en.Current);
            }
        }

        return list;
    }

    private string ChunkName (float xf, float yf)
    {
        int x = (int)(xf);
        int y = (int)(yf);
        string name = string.Format("c{0}{1}{2}{3}",
            (x >= 0) ? 'p' : 'm',
            Mathf.Abs(x),
            (y >= 0) ? 'p' : 'm',
            Mathf.Abs(y)
            );

        return name;
    }

    private Vector3 DiscretizePosition (Vector3 position)
    {
        return new Vector3(
            Mathf.Floor((position.x + halfChunkSize) * chunkSizeInverse),
            Mathf.Floor((position.y + halfChunkSize) * chunkSizeInverse),
            0.0f
            );
    }

    private List<Vector3> GetNeighbourPositionsDiscretized (Vector3 position)
    {
        List<Vector3> neighbours = new List<Vector3>();
        Vector3 pos = DiscretizePosition(position);

        neighbours.Add(pos + new Vector3(0.0f, 1.0f, 0.0f));
        neighbours.Add(pos + new Vector3(1.0f, 1.0f, 0.0f));
        neighbours.Add(pos + new Vector3(1.0f, 0.0f, 0.0f));
        neighbours.Add(pos + new Vector3(1.0f, -1.0f, 0.0f));

        neighbours.Add(pos + new Vector3(0.0f, -1.0f, 0.0f));
        neighbours.Add(pos + new Vector3(-1.0f, -1.0f, 0.0f));
        neighbours.Add(pos + new Vector3(-1.0f, 0.0f, 0.0f));
        neighbours.Add(pos + new Vector3(-1.0f, 1.0f, 0.0f));

        return neighbours;
    }

    private void CleanChuncks ()
    {
        Vector3 ship = gameController.ShipPosition;
        List<KeyValuePair<string, float>> chunksList = new List<KeyValuePair<string, float>>();
        Dictionary<string, GameObject>.Enumerator en = chunks.GetEnumerator();

        while (en.MoveNext())
        {
            chunksList.Add(
                new KeyValuePair<string, float>(
                    en.Current.Key,
                    (en.Current.Value.transform.position - ship).sqrMagnitude
                    ));
        }

        chunksList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));

        string key;
        GameObject value;
        for (int i=chunksList.Count - 1; i >  chunksList.Count - CHUNKS_CLEANED_AT_A_TIME; --i)
        {
            key = chunksList[i].Key;
            if(this.chunks.TryGetValue(key, out value))
            {
                Destroy(value);
                this.chunks.Remove(key);
            }
        }

        /*
         * Buggy algorithm 
         *
         *
        Vector3 ship = gameController.GetShipPosition();
        List<KeyValuePair<string, float>> chunksList = new List<KeyValuePair<string, float>>();
        Dictionary<string, GameObject>.Enumerator en = chunks.GetEnumerator();

        KeyValuePair<string, float> pair;
        while (en.MoveNext())
        {            
            pair = new KeyValuePair<string, float>(
                en.Current.Key,
                (en.Current.Value.transform.position - ship).sqrMagnitude
                );
            if (chunksList.Count < CHUNKS_CLEANED_AT_A_TIME)
            {
                chunksList.Add(pair);
            }
            else
            {
                if(pair.Value > chunksList[chunksList.Count - 1].Value)
                {
                    int i = chunksList.Count - 2;
                    while (i >= 0)
                    {
                        if (pair.Value < chunksList[i].Value)
                        {
                            chunksList.Insert(i + 1, pair);
                            i = -2;
                        }
                        i--;
                    }
                    if (i == -1)
                    {
                        chunksList.Insert(0, pair);
                    }
                    chunksList.RemoveAt(chunksList.Count - 1);
                }
            }
        }

        GameObject value;
        foreach(KeyValuePair<string, float> p in chunksList)
        {
            if (this.chunks.TryGetValue(p.Key, out value))
            {
                Destroy(value);
                this.chunks.Remove(p.Key);
            }
        }
        */
    }
}
