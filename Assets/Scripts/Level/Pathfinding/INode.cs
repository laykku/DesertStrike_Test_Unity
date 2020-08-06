using System.Collections.Generic;
using UnityEngine;

namespace DesertStrike.Level.Navigation
{
    public interface INode
    {
        List<INode> LinkedNodes { get; }
        void Link(INode node);
        Vector2Int Point { get; }
        Vector3 Position { get; }
    }
}