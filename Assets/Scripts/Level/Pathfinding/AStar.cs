using System.Collections.Generic;
using DesertStrike.Helpers;
using UnityEngine;

namespace DesertStrike.Level.Navigation
{
    public class AStar : IPathfindingAlgorithm
    {
        private double Heuristic(INode a, INode b)
        {
            return Vector2Int.Distance(a.Point, b.Point);
        }

        public List<Vector3> FindPath(ILevelNavigation graph, INode startNode, INode targetNode)
        {
            PriorityQueue<INode> openSet = new PriorityQueue<INode>();
            openSet.Enqueue(startNode, 0);
            
            Dictionary<INode, INode> cameFrom = new Dictionary<INode, INode>();
            cameFrom.Add(startNode, startNode);
            
            Dictionary<INode, double> costSoFar = new Dictionary<INode, double>();
            costSoFar.Add(startNode, 0);
            
            while (openSet.Count > 0)
            {
                INode current = openSet.Dequeue();

                if (current == targetNode)
                {
                    break;
                }
                
                foreach (var linkedNode in current.LinkedNodes)
                {
                    double newCost = costSoFar[current] + graph.Cost(current, linkedNode);
                    
                    if (!costSoFar.ContainsKey(linkedNode) || newCost < costSoFar[linkedNode])
                    {
                        costSoFar[linkedNode] = newCost;
                        double priority = newCost + Heuristic(linkedNode, targetNode);
                        openSet.Enqueue(linkedNode, priority);
                        cameFrom[linkedNode] = current;
                    }
                }
            }

            List<Vector3> path = new List<Vector3>();

            INode n = cameFrom[targetNode];
            while (n != startNode)
            {
                path.Add(n.Position);
                n = cameFrom[n];
            }

            path.Reverse();
            
            return path;
        }
    }
}