using System.Collections.Generic;
using UnityEngine;

namespace DesertStrike.Level.Navigation
{
    public interface IPathfindingAlgorithm
    {
        List<Vector3> FindPath(ILevelNavigation graph, INode startNode, INode targetNode);
    }
}