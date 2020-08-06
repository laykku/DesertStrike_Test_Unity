using System.Collections.Generic;
using UnityEngine;

namespace DesertStrike.Level.Navigation
{
    public class Node : INode
    {
        public List<INode> LinkedNodes { get; private set; }
        public Vector2Int Point { get; private set; }
        public Vector3 Position { get; private set; }
        
        public Node(Vector2Int point, Vector3 position)
        {
            Point = point;
            Position = position;
            LinkedNodes  = new List<INode>();
        }
        
        public void Link(INode node)
        {
            LinkedNodes.Add(node);
        }
    }
}