using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public interface ICubeMap 
{
    public void SetCubeMap(BaseCube cube , Vector3Int position);
    public void RemoveCubeMap(Vector3Int position);
    public BaseCube GetCube(Vector3Int position);
}
