using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eft_dma_radar.Source.Misc
{
    /// <summary>
    /// Represents a 64-Bit Unsigned Pointer Address.
    /// </summary>
    public readonly struct MemPointer
    {
        public static implicit operator MemPointer(ulong x) => x;
        public static implicit operator ulong(MemPointer x) => x.Va;
        /// <summary>
        /// Virtual Address of this Pointer.
        /// </summary>
        public readonly ulong Va;

        /// <summary>
        /// Validates the Pointer.
        /// </summary>
        /// <exception cref="NullPtrException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void Validate()
        {
            if (Va == 0x0)
                throw new NullPtrException();
        }

        /// <summary>
        /// Convert to string format.
        /// </summary>
        /// <returns>Pointer Address represented in Upper-Case Hex.</returns>
        public readonly override string ToString() => Va.ToString("X");
    }
}
