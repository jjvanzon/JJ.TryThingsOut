using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoCross.Utilities.Network
{
    /// <summary>
    /// Factory for the creation of a network access strategy.
    /// </summary>
    internal static class NetworkFactory
    {
        /// <summary>
        /// Creates an INetwork instance.
        /// </summary>
        /// <returns></returns>
        internal static INetwork Create()
        {
            INetwork network = new NetworkAsynch();
            return network;
        }

        // If we ever want to make an implementation of INetwork that we want in core,
        /// <summary>
        /// Creates an INetwork instance of the specified network type.
        /// </summary>
        /// <param name="networkType">Type of the file.</param>
        /// <returns></returns>
        internal static INetwork Create(NetworkType networkType)
        {
            INetwork network = new NetworkAsynch();

            switch (networkType)
            {
                case NetworkType.NetworkAsynch:
                    network = new NetworkAsynch();
                    break;
                case NetworkType.NetworkSynch:
                    network = new NetworkSynch();
                    break;
                default:
                    // returns the default - BasicNetwork implementation                 
                    break;
            }

            return network;
        }
    }

    /// <summary>
    /// Indicates the thread type to use
    /// </summary>
    public enum NetworkType
    {
        /// <summary>
        /// Network with Asynchronous support for Get/Post (default).
        /// </summary>
        NetworkAsynch,

        /// <summary>
        /// Network with Synchronous support for Get/Post.
        /// </summary>
        NetworkSynch
    }
}

