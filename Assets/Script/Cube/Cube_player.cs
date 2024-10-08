using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_player : BaseCube
{
    public static Cube_player AddPlayer(Vector3Int position, Vector3Int destination)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Cube_player");
        if(prefab == null)
        {
            Debug.LogError("Cube_player prefab not found");
            return null;
        }
        prefab = Instantiate(prefab);
        Cube_player player = prefab.GetComponent<Cube_player>();
        player.Init(position, destination);
        return player;
    }
    // public Vector3Int position;
    Vector3Int destination;
    protected void Init(Vector3Int position , Vector3Int destination)
    {
        this.position = position;
        this.destination = destination;
        transform.position = position + Vector3Int.up;
        moveTargetList = new Stack<Vector3Int>();
        isMove = false;
        speed = 3f;
        
        childCubes = new HashSet<BaseCube>();
    }
    private Stack<Vector3Int> moveTargetList;
    public void ClearMoveTarget()
    {
        moveTargetList.Clear();
    }
    public void AddMoveTarget(Vector3Int target)
    {
        moveTargetList.Push(target);
    }
    public void Save(Action<Vector3Int , Vector3Int> savePlayer)
    {
        savePlayer(position , destination);
    }
    protected override void ChangePosition()
    {
        base.ChangePosition();
        position = parentCube.position;
        transform.position = position + Vector3Int.up;
    }
    public override void AddParentCube(BaseCube parentCube)
    {
        base.AddParentCube(parentCube);
        ChangePosition();
    }
    public void Move(Action MoveEnd)
    {
        if(moveTargetList.Count == 0)
        {
            return ;
        }
        this.MoveEnd = MoveEnd;
        transform.position = moveTargetList.Pop() + Vector3Int.up;
        midTarget = moveTargetList.Pop() + Vector3Int.up;
        isMove = true;
    }

    private bool isMove;
    private float speed ;
    private Action MoveEnd;
    Vector3 midTarget;
    private void Update()
    {
        if(isMove)
        {
            if((transform.position - midTarget).sqrMagnitude < 0.001f)
            {
                if(moveTargetList.Count == 0)
                {
                    isMove = false;
                    position = Vector3Int.RoundToInt(transform.position - Vector3.up);
                    MoveEnd();
                    return ;
                }
                else
                {
                    transform.position = moveTargetList.Pop() + Vector3Int.up;
                    midTarget = moveTargetList.Pop() + Vector3Int.up;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, midTarget, speed * Time.deltaTime);
        }
    }
}
