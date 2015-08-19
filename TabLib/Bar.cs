namespace TabLib
{
    /// <summary>
    /// Class Bar.
    /// </summary>
    public class Bar
    {
        #region Properties

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the beats.
        /// </summary>
        /// <value>
        /// The beats.
        /// </value>
        public Beat[] Beats { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Bar"/> class.
        /// </summary>
        public Bar()
        {
            Index = 0;
            Beats = new Beat[0];
        }

        #endregion
    }
}
