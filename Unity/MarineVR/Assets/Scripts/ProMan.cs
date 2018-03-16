using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProMan : MonoBehaviour
{
    //Singleton
    private static ProMan proMan;

    private ProMan() { }

    public static ProMan Instance
    {
        get
        {
            if (proMan == null)
            {
                proMan = new ProMan();
            }
            return proMan;
        }
    }

    //player variables
    [SerializeField]
    private Transform player;
    private Vector2Int lastPos;

    //procedural prefab
    [SerializeField]
    private GameObject procedural;

    //size
    private float size;

    //lists
    private Dictionary<Vector2Int, Procedural> pros = new Dictionary<Vector2Int, Procedural>();
    private List<Vector2Int> actives = new List<Vector2Int>();

    // Use this for initialization
    void Start()
    {
        //set width and length
        size = procedural.GetComponent<Procedural>().size;
        //initialise last position
        lastPos = posToPairs(player.position);
        lastPos.x += 1;
    }

    // Update is called once per frame
    void Update()
    {
        //find simplfied player position
        Vector2Int place = posToPairs(player.position);

        //if player has moved chunk
        if (place != lastPos)
        {
            //set prevs to actives
            List<Vector2Int> prevs = new List<Vector2Int>(actives);

            actives.Clear();

            for (int x = -1; x < 2; x++)
            {
                for (int z = -1; z < 2; z++)
                {
                    actives.Add(new Vector2Int( x, z) + place);
                }
            }

            foreach (Vector2Int a in actives)
            {
                if (prevs.Contains(a))
                {
                    prevs.Remove(a);
                }
                else
                {
                    if (pros.ContainsKey(a))
                    {
                        pros[a].Create();
                    }
                    else
                    {
                        MakePro(a);
                    }
                }
            }

            foreach (Vector2Int p in prevs)
            {
                pros[p].Remove();
            }

            //set last pos to place
            lastPos = place;
        }
    }

    GameObject MakePro(Vector2Int pos)
    {
        if (!pros.ContainsKey(pos))
        {
            //create Game Object
            GameObject x = Instantiate(procedural, new Vector3((float)pos.x * size + size / 2.0f, 0.0f, (float)pos.y * size + size / 2.0f), Quaternion.identity);
            x.transform.parent = this.GetComponent<Transform>();
            pros.Add(pos, x.GetComponent<Procedural>());
            return x;
        }
        return null;
    }

    Vector2Int posToPairs(Vector3 pos)
    {
        Vector2Int sp = new Vector2Int((int)(pos.x / size), (int)(pos.z / size));
        if (pos.x < 0)
        {
            sp.x -= 1;
        }
        if (pos.z < 0)
        {
            sp.y -= 1;
        }
        return sp;
    }
}
