// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace System.IO
{
    /// <summary>
    /// Represents a temporary directory.  Creating an instance creates a directory at the specified path,
    /// and disposing the instance deletes the directory.
    /// </summary>
    public sealed class TempDirectory : IDisposable
    {
        /// <summary>Gets the created directory's path.</summary>
        public string Path { get; private set; }

        public TempDirectory(string path)
        {
            Path = path;
            Directory.CreateDirectory(path);
        }

        ~TempDirectory() { DeleteDirectory(); }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            DeleteDirectory();
        }

        private void DeleteDirectory()
        {
            try { Directory.Delete(Path, recursive: true); }
            catch { /* Ignore exceptions on disposal paths */ }
        }
    }
}
