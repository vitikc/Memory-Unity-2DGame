using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public Sprite logo;
    public List<Sprite> sprites = new List<Sprite>();
    public GameObject filler;

    private Tile first;
    private Tile second;
    private Tile[,] field;

    private bool cooldown = false;

	// Use this for initialization
	void Start () {
        field = new Tile[4, 4];
        sprites.AddRange(sprites);
        Shuffle(sprites);
        int i = 0;
        for (int x = 0; x < 4; x++)
        {
            for(int y = 0; y < 4; y++)
            {
                GameObject go = Instantiate(filler, new Vector2(x, -y), Quaternion.identity, transform);
                go.name = "Tile_" + x + "_" + y;
                go.AddComponent<BoxCollider2D>();
                field[x, y] = go.GetComponent<Tile>();
                field[x, y].SetLogo(logo);
                field[x, y].SetSprite(sprites[i]);
                field[x, y].Number = int.Parse(sprites[i].name);
                i++;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        GameInput();
        if (!cooldown)
        {
            if(first!=null&&second!=null)
            StartCoroutine(WaitForCheck());
        }
	}
    void GameInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                int x = (int)hit.transform.position.x;
                int y = -(int)hit.transform.position.y;
                if (first == null)
                {
                    first = field[x, y];
                    first.Open();
                    return;
                }
                if (second == null&&first.GetInstanceID()!=field[x,y].GetInstanceID())
                {
                    second = field[x, y];
                    second.Open();
                    return;
                }
            }
        }
    }
    void CheckOpened()
    {
        
        if (first.Number == second.Number)
        {
            first = null;
            second = null;
            return;
        }
        first.Close();
        second.Close();
        first = null;
        second = null;
    }
    IEnumerator WaitForCheck()
    {
        cooldown = true;
        yield return new WaitForSeconds(1f);
        CheckOpened();
        cooldown = false;
    }
    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
