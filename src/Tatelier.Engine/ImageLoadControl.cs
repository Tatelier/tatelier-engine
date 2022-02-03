using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tatelier.Engine.Interface;

namespace Tatelier.Engine
{
    /// <summary>
    /// 画像読込管理
    /// </summary>
    public class ImageLoadControl
	{
        MemoryStream dbMemory = new MemoryStream();

        MemoryStream dbDebugMemory = null;

        SHA256 sha256 = SHA256.Create();

        IEngineFunctionModule engineFunctionModule;

        bool isDebug = false;
        public bool IsDebug
        {
            get => isDebug;
            set
            {
                isDebug = value;
                if (value)
                {
                    if(dbDebugMemory == null)
                    {
                        dbDebugMemory = new MemoryStream();
                    }
                }
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Start(IEngineFunctionModule engineFunctionModule)
        {
            this.engineFunctionModule = engineFunctionModule;
        }

        void LoadDebug(string filePath, int handle)
        {
            using (var db = new LiteDB.LiteDatabase(dbDebugMemory))
            {
                var collection = db.GetCollection<ImageLoadDebugDBColumn>();
            }
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
                var collection = db.GetCollection<ImageLoadDBColumn>();

                var one = collection.FindOne(x => x.Hash01 == hash01 && x.Hash02 == hash02);

                int handle = -1;

                if(one == null)
                {
                    handle = engineFunctionModule.LoadGraph(filePath);

                    if (handle != -1)
                    {
                        collection.Insert(new ImageLoadDBColumn()
                        {
                            Handle = handle,
                            Hash01 = hash01,
                            Hash02 = hash02,
                            UsedCount = 1,
                        });

                        db.Commit();
                    }
                }
                else
                {
                    handle = one.Handle;
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
                var collection = db.GetCollection<ImageLoadDBColumn>();

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
        internal class ImageLoadDBColumn
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
        internal class ImageLoadDebugDBColumn
        {
            public int id { get; set; }

            public int Handle { get; set; }

            public string FilePath { get; set; }
        }

    }
}
