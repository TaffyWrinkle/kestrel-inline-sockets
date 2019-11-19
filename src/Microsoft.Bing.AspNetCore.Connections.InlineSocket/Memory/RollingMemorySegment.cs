// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Buffers;

namespace Microsoft.Bing.AspNetCore.Connections.InlineSocket.Memory
{
    public class RollingMemorySegment : ReadOnlySequenceSegment<byte>, IDisposable
    {
        private readonly IMemoryOwner<byte> _rental;

        public RollingMemorySegment(IMemoryOwner<byte> rental, long runningIndex)
        {
            _rental = rental;
            Memory = _rental.Memory;
            RunningIndex = runningIndex;
        }

        public new RollingMemorySegment Next
        {
            get => (RollingMemorySegment)base.Next;
            set => base.Next = value;
        }

        public void Shrink(int length)
        {
            Memory = Memory.Slice(0, length);
        }

        public void Dispose()
        {
            _rental.Dispose();
        }
    }
}
