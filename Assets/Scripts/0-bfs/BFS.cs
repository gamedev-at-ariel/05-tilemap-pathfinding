using System;
using System.Collections;
using System.Collections.Generic;

/**
 * A generic implementation of the BFS algorithm.
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
public abstract class BFS<NodeType> {

    // Override this function to define the neighbors of the given node:
    public abstract IEnumerable<NodeType> Neighbors(NodeType node);

    public void FindPath(NodeType startNode, NodeType endNode, List<NodeType> path, int maxiterations=1000) {
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> closedSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        openQueue.Enqueue(startNode);
        int i; for (i = 0; i < maxiterations; ++i) { // After maxiterations, stop and return an empty path
            if (openQueue.Count == 0) {
                break;
            } else {
                NodeType searchFocus = openQueue.Dequeue();
                if (searchFocus.Equals(endNode)) {
                    path.Add(endNode);
                    while (previous.ContainsKey(searchFocus)) {
                        searchFocus = previous[searchFocus];
                        path.Add(searchFocus);
                    }
                    path.Reverse();
                    break;
                }
                foreach (var neighbor in Neighbors(searchFocus)) {
                    if (closedSet.Contains(neighbor)) {
                        continue;
                    }
                    openQueue.Enqueue(neighbor);
                    previous[neighbor] = searchFocus;
                }
                closedSet.Add(searchFocus);
            }
        }
    }

    public List<NodeType> GetPath(NodeType startNode, NodeType endNode, int maxiterations=1000) {
        List<NodeType> path = new List<NodeType>();
        FindPath(startNode, endNode, path, maxiterations);
        return path;
    }
}