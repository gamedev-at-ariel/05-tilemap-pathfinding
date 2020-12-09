using System.Collections.Generic;

/**
 * An abstract graph.
 * It does not use any memory.
 * It only has a single abstract function Neighbors, that returns the neighbors of a given node.
 * @author Erel Segal-Halevi
 * @since 2020-12
 */
public interface AbstractGraph<NodeType> {
    IEnumerable<NodeType> Neighbors(NodeType node);
}
