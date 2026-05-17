using RecyclingRecognizer.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecyclingRecognizer.Tests
{
    public class StubFileLoader : IFileLoader
    {
        private readonly byte[] _fakeData;
        private readonly bool _throwFileNotFound;

        public StubFileLoader(byte[] fakeData = null, bool throwFileNotFound = false)
        {
            _fakeData = fakeData ?? new byte[] { 0x01, 0x02 };
            _throwFileNotFound = throwFileNotFound;
        }

        public byte[] LoadImage(string path)
        {
            if (_throwFileNotFound)
                throw new System.IO.FileNotFoundException("Файл не найден");
            return _fakeData;
        }
    }
}
