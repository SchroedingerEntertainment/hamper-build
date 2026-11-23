// Licensed to Schroedinger Entertainment (SOE) under the terms of the AGPLv3
// Licensed to you by SOE under the terms of the AGPLv3 or another OSI-approved license 

using System.Runtime.CompilerServices;
using Soe.Storage;
using Soe.Threading;

namespace Soe.Build
{
    /// <summary>
    /// Provides a target for source identification in <see cref="ProcessingPhase.Discovery"/>
    /// </summary>
    /// <param name="identifier">The source to treat as a resource</param>
    [method: MethodImpl(MethodImplOptions.AggressiveInlining)]
    #if EXPORT_HAMPER_BUILD_MAIN
    public
    #else
    internal
    #endif
    struct TargetComponent(ResourceIdentifier identifier)
    {
        /// <summary>
        /// Gets the unified resource identifier for this target
        /// </summary>
        public ResourceIdentifier Identifier
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return identifier; }
        }

        private UInt32 resolved;
        /// <summary>
        /// Gets an indicator if this target was successfully processed by a service
        /// </summary>
        public bool Resolved
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return (resolved != 0); }
        }

        /// <summary>
        /// Marks this target as successfully processed by a service
        /// </summary>
        /// <remarks>This operation is thread-safe and can be performed from within an <see cref="ImmutablePolicy"/></remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Resolve()
        {
            Interlocked.Exchange(ref resolved, 1);
        }
    }
}