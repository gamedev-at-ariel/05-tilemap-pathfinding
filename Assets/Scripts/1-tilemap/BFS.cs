using System;
using System.Collections;
using System.Collections.Generic;

/**
 * A generic implementation of the BFS algorithm.
 * Allows to track the progress of the algorithm.
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
public abstract class BFS<NodeType> {

    // Override this function to define the neighbors of the given node:
    public virtual IEnumerable<NodeType> Neighbors(NodeType node) { return null; }

    // Override these functions to track the progress of the algorithm:
    public virtual IEnumerator OnEndFound() { return null; }
    public virtual IEnumerator OnEndNotFound() { return null; }
    public virtual IEnumerator OnFocus(NodeType node) { return null; }
    public virtual IEnumerator OnOpen(NodeType node) { return null; }
    public virtual IEnumerator OnClose(NodeType node) { return null; }
    public virtual IEnumerator OnBackTrack(NodeType node) { return null; }

    public IEnumerator FindPath(NodeType startNode, NodeType endNode, List<NodeType> path, int maxiterations=1000) {
        Queue<NodeType> openQueue = new Queue<NodeType>();
        HashSet<NodeType> closedSet = new HashSet<NodeType>();
        Dictionary<NodeType, NodeType> previous = new Dictionary<NodeType, NodeType>();
        openQueue.Enqueue(startNode);
        yield return OnOpen(startNode);
        int i; for (i = 0; i < maxiterations; ++i) { // Not part of the algorithm - just to prevent infinite loops in case of bugs.
            if (openQueue.Count == 0) {
                yield return OnEndNotFound();
                break;
            } else {
                NodeType searchFocus = openQueue.Dequeue();
                yield return OnFocus(searchFocus);
                if (searchFocus.Equals(endNode)) {
                    OnEndFound();
                    path.Add(endNode);
                    while (previous.ContainsKey(searchFocus)) {
                        searchFocus = previous[searchFocus];
                        path.Add(searchFocus);
                        yield return OnBackTrack(searchFocus);
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
                    yield return OnOpen(neighbor);
                }
                closedSet.Add(searchFocus);
                yield return OnClose(searchFocus);
            }
        }
        if (i >= 1000) {
            throw new OverflowException("Too many iterations");
        }
    }

    public List<NodeType> GetPath(NodeType startNode, NodeType endNode, int maxiterations=1000) {
        List<NodeType> path = new List<NodeType>();
        var bfsRun = FindPath(startNode, endNode, path, maxiterations);
        while (bfsRun.MoveNext()) ;
        return path;
    }
}