using UnityEngine;

namespace NZLib.Utilities.DirectionCore
{
    /// <summary>
    /// Offers useful methods related to Directions.
    /// </summary>
    public sealed class DirectionHelper
    {
        /* ===================================================================================== */
        // Methods

        /// <summary>
        /// Returns a random Direction.
        /// </summary>
        public static Direction GetRandom () => (Direction)UnityEngine.Random.Range(0, 4);

        /// <summary>
        /// Returns a random horizontal Direction.
        /// </summary>
        public static Direction GetRandomHorizontal () => (Direction)UnityEngine.Random.Range(2, 4);

        /// <summary>
        /// Returns a random Direction vertical.
        /// </summary>
        public static Direction GetRandomVertical () => (Direction)UnityEngine.Random.Range(0, 2);

        /// <summary>
        /// Returns opposite horizontal Direction for the given one.
        /// </summary>
        public static Direction GetOppositeHorizontal (Direction horizontal)
        {
            if (horizontal == Direction.Left)
            {
                return Direction.Right;
            }
            else if (horizontal == Direction.Right)
            {
                return Direction.Left;
            }
            Debug.LogWarning("Direction given is not horizontal. Returning 'Left' by default.");
            return Direction.Left;
        }

        /// <summary>
        /// Returns opposite vertical Direction for the given one.
        /// </summary>
        public static Direction GetOppositeVertical (Direction vertical)
        {
            if (vertical == Direction.Up)
            {
                return Direction.Down;
            }
            else if (vertical == Direction.Down)
            {
                return Direction.Up;
            }
            Debug.LogWarning("Direction given is not horizontal. Returning 'Up' by default.");
            return Direction.Up;
        }
    }
}
