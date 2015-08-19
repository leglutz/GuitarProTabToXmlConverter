namespace TabLib
{
    /// <summary>
    /// Class Track.
    /// </summary>
    public class Track
    {
        #region Properties

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the bars.
        /// </summary>
        /// <value>
        /// The bars.
        /// </value>
        public Bar[] Bars { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Track"/> class.
        /// </summary>
        public Track()
        {
            Index = 0;
            Name = string.Empty;
            Bars = new Bar[0];
        }

        #endregion
    }
}
