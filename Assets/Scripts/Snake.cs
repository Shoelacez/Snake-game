using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Snake : MonoBehaviour
{
    //By default our snake is going to be moving from L to R 
    private Vector2 _direction=Vector2.right;
    private List<Transform> _segments=new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize=4;
    void Start() 
    {
        ResetState();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //When we press W we move UP
            _direction=Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            //When we press A we move L
            _direction=Vector2.left;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            //When we press S we move Down
            _direction=Vector2.down;   
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //We move to the R
            _direction=Vector2.right;
        }
    }

    private void FixedUpdate() 
    {
        for (int i = _segments.Count-1; i > 0; i--) 
        {
            _segments[i].position=_segments[i-1].position;   
        }

        this.transform.position=new Vector3(
            Mathf.Round(this.transform.position.x)+_direction.x,
            Mathf.Round(this.transform.position.y)+_direction.y,
            0.0f
        );   
    }

    void Grow()
    {
        Transform segment=Instantiate(segmentPrefab);
        //segment.transform.position isn't neceessary coz its of Transform type already
        segment.position=_segments[_segments.Count-1].position;
        _segments.Add(segment);
    }

    void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].transform.gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        //for the snake to have an initial size
        for (int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(segmentPrefab));
        }

        this.transform.position=Vector3.zero;
    }

     private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag=="food")
        {
            Grow();
        }

        if (other.tag=="Wall")
        {
            ResetState();
        }
    }

}
