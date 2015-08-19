namespace TabLib
{
    /// <summary>
    /// Class Note.
    /// </summary>
    public class Note
    {
        #region Properties

        /// <summary>
        /// Gets or sets the axe string.
        /// </summary>
        /// <value>
        /// The axe string.
        /// </value>
        public int @String { get; set; }

        /// <summary>
        /// Gets or sets the fret.
        /// </summary>
        /// <value>
        /// The fret.
        /// </value>
        public int Fret { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Note"/> class.
        /// </summary>
        public Note()
        {
            @String = 0;
            Fret = 0;
        }

        #endregion
    }
}
