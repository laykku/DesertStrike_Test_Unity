using System.Collections.Generic;
using UnityEngine;

namespace DesertStrike.Level.Navigation
{
    public interface ILevelNavigation
    {
        double Cost(INode a, INode b);
    }

    public class LevelNavigation : ILevelNavigation
    {
        private readonly Dictionary<Vector2Int, INode> _nodes;
        private readonly IPathfindingAlgorithm _pathfindingAlgorithm;
        
        public LevelNavigation(IPathfindingAlgorithm pathfindingAlgorithm)
        {
            _nodes = new Dictionary<Vector2Int, INode>();
            _pathfindingAlgorithm = pathfindingAlgorithm;
        }
        
        private const float ObstacleOffset = 2f;
        
        public void BuildNavigationGraph(Vector3 min, Vector3 max, bool diagonal, List<IObstacle> obstacles)
        {
            Vector2Int point = Vector2Int.zero;

            for (int x = (int)min.x; x < (int)max.x; x++)
            {
                for (int z = (int)min.z; z < (int)max.z; z++)
                {
                    Vector3 pos = new Vector3(x + .5f, 0f, z + .5f);
                    bool free = true;

                    foreach (var obstacle in obstacles)
                    {
                        if (Vector3.Distance(obstacle.Transform.position, pos) < ObstacleOffset)
                        {
                            free = false;
                            break;
                        }
                    }

                    if (free)
                    {
                        _nodes.Add(point, new Node(point, pos));
                    }
                    
                    point.y++;
                }

                point.y = 0;
                point.x++;
            }

            // link nodes

            foreach (var node in _nodes)
            {
                Vector2Int left = new Vector2Int(node.Key.x - 1, node.Key.y);
                if (_nodes.ContainsKey(left)) node.Value.Link(_nodes[left]);

                Vector2Int right = new Vector2Int(node.Key.x + 1, node.Key.y);
                if (_nodes.ContainsKey(right)) node.Value.Link(_nodes[right]);

                Vector2Int front = new Vector2Int(node.Key.x, node.Key.y + 1);
                if (_nodes.ContainsKey(front)) node.Value.Link(_nodes[front]);

                Vector2Int back = new Vector2Int(node.Key.x, node.Key.y - 1);
                if (_nodes.ContainsKey(back)) node.Value.Link(_nodes[back]);

                if (diagonal)
                {
                    Vector2Int nw = new Vector2Int(node.Key.x - 1, node.Key.y + 1);
                    if (_nodes.ContainsKey(nw)) node.Value.Link(_nodes[nw]);

                    Vector2Int ne = new Vector2Int(node.Key.x + 1, node.Key.y + 1);
                    if (_nodes.ContainsKey(ne)) node.Value.Link(_nodes[ne]);

                    Vector2Int sw = new Vector2Int(node.Key.x - 1, node.Key.y - 1);
                    if (_nodes.ContainsKey(sw)) node.Value.Link(_nodes[sw]);

                    Vector2Int se = new Vector2Int(node.Key.x + 1, node.Key.y - 1);
                    if (_nodes.ContainsKey(se)) node.Value.Link(_nodes[se]);
                }
            }
        }

        public double Cost(INode a, INode b) => 1;

        private INode FindNodeByPos(Vector3 pos)
        {
            float closestDistance = Mathf.Infinity;
            INode closestNode = null;
            
            foreach (var node in _nodes)
            {
                float dist = Vector3.Distance(node.Value.Position, pos);
                if (dist < closestDistance)
                {
                    closestDistance = dist;
                    closestNode = node.Value;
                }
            }

            return closestNode;
        }

        public List<Vector3> FindPath(Vector3 start, Vector3 target)
        {
            INode startNode = FindNodeByPos(start);
            INode targetNode = FindNodeByPos(target);
            return _pathfindingAlgorithm.FindPath(this, startNode, targetNode);
        }
    }
}