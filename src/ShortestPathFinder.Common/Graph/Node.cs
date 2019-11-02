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
        /// Creates new instance of the Node
        /// </summary>
        public Node(string displayName)
        {
            DisplayName = displayName;
        }

        /// <summary>
        /// Making sure whoever use this will calculate it's hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (DisplayName != null ? DisplayName.GetHashCode() : 0);
        }

        private bool Equals(Node other)
        {
            return DisplayName == other.DisplayName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Node) obj);
        }

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