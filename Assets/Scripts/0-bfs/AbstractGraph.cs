using System.Collections.Generic;

/**
 * An abstract graph.
 * It does not use any memory.
 * It only has a single abstract Neighbors function, that returns the neighbors of a given node.
 * @author Erel Segal-Halevi
 * @since 2020-02
 */
public abstract class AbstractGraph<NodeType> {
    public abstract IEnumerable<NodeType> Neighbors(NodeType node);
}
