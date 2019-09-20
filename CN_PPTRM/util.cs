/*
 * Utility Functions
 * 
 * Description:
 *     Defines a few functions that'll probably be used quite a few times...
 */

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppt_replay_gui {
    class UTIL {
        public byte[] gz_compress(byte[] bytes) {
            using (var ms = new MemoryStream())
            using (var gzs = new GZipStream(ms, CompressionLevel.Optimal)) {
                gzs.Write(bytes, 0, bytes.Length);
                gzs.Close();
                return ms.ToArray();
            }
        }

        public byte[] gz_decompress(byte[] bytes) {
            try {
                using (var ms = new MemoryStream(bytes))
                using (var res = new MemoryStream())
                using (var gzs = new GZipStream(ms, CompressionMode.Decompress)) {
                    gzs.CopyTo(res);
                    return res.ToArray();
                }
            }
            catch (System.IO.InvalidDataException) {
                //That was not a properly encoded GZip file. So just assume it's RAW.
                return bytes;
            }
        }
    }
}
