namespace ShortestPathFinder.Common.Graph
{
    /// <summary>
    /// Represents a node in a graph
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Returns the display name of the node
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Making sure whoever use this will calculate it's hashcode
        /// </summary>
        /// <returns></returns>
        public abstract override int GetHashCode();

        /// <summary>
        /// Returns the display name 
        /// </summary>
        /// <returns>The display name</returns>
        public override string ToString()
        {
            return DisplayName;
        }
    }
}