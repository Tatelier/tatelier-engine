using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    public class SoundLoadControl
    {
        MemoryStream dbMemory = new MemoryStream();

        SHA256 sha256 = SHA256.Create();

        public bool IsDebug { get; set; }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// 画像を読込
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>ハンドル</returns>
        public int Load(string filePath)
        {
            var bytes = sha256.ComputeHash(File.OpenRead(filePath));

            ulong hash01 = BitConverter.ToUInt64(bytes, 0);
            ulong hash02 = BitConverter.ToUInt64(bytes, 16);

            using (var db = new LiteDB.LiteDatabase(dbMemory))
            {
                var collection = db.GetCollection<SoundLoadDBColumn>();

                var one = collection.FindOne(x => x.Hash01 == hash01 && x.Hash02 == hash02);

                int handle = -1;

                if (one == null)
                {
                    handle = (int)(hash01 & 0x00000000FFFFFFFF);

                    collection.Insert(new SoundLoadDBColumn()
                    {
                        Handle = handle,
                        Hash01 = hash01,
                        Hash02 = hash02,
                        UsedCount = 1,
                    });

                    db.Commit();
                }
                else
                {
                    one.UsedCount++;
                    collection.Update(one);
                    db.Commit();
                }
                return handle;
            }
        }

        /// <summary>
        /// 画像を削除
        /// </summary>
        /// <param name="handle">画像ハンドル</param>
        /// <returns></returns>
        public int Delete(int handle)
        {
            using (var db = new LiteDB.LiteDatabase(dbMemory))
            {
                var collection = db.GetCollection<SoundLoadDBColumn>();

                var one = collection.FindOne(x => x.Handle == handle);

                if (one == null)
                {
                    // 無いのにコールされた
                    return 0;
                }
                else
                {
                    one.UsedCount--;
                    if (one.UsedCount == 0)
                    {
                        collection.Delete(one.id);
                        db.Commit();
                    }
                    else
                    {
                        collection.Update(one);
                        db.Commit();
                    }

                    return one.UsedCount;
                }
            }
        }


        /// <summary>
        /// 画像読込カラム
        /// </summary>
        internal class SoundLoadDBColumn
        {
            public int id { get; set; }

            public int Handle { get; set; }

            public ulong Hash01 { get; set; }

            public ulong Hash02 { get; set; }

            public int UsedCount { get; set; }
        }

        /// <summary>
        /// 画像読込デバッグ用カラム
        /// </summary>
        internal class SoundLoadDebugDBColumn
        {
            public int id { get; set; }

            public int Handle { get; set; }

            public string FilePath { get; set; }
        }
    }
}
