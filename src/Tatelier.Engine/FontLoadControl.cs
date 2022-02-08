using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tatelier.Engine.Interface;

namespace Tatelier.Engine
{
    public class FontLoadControl: BaseLoadControl
    {
        MemoryStream dbDebugMemory = null;

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
                    if (dbDebugMemory == null)
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
                var collection = db.GetCollection<FontLoadDebugDBColumn>();
            }
        }

        /// <summary>
        /// 画像を読込
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>ハンドル</returns>
        public int Create(string fontName
            , int size = 16
            , int thick = -1
            , int fontType = -1
            , int charSet = -1
            , int edgeSize = -1
            , int italic = 0
            , int handle = -1
            )
        {
            string str = $"{fontName}?{size}?{thick}?{fontType}?{charSet}?{edgeSize}?{italic}?{handle}";

            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(str));

            ulong hash01 = BitConverter.ToUInt64(bytes, 0);
            ulong hash02 = BitConverter.ToUInt64(bytes, 16);

            using (var db = new LiteDB.LiteDatabase(dbMemory))
            {
                var collection = db.GetCollection<LoadContentCommonColumn>();

                var one = collection.FindOne(x => x.Hash01 == hash01 && x.Hash02 == hash02);

                int fontHandle = -1;

                if (one == null)
                {
                    fontHandle = engineFunctionModule.CreateFontToHandle(fontName, size, thick, fontType);

                    if (fontHandle != -1)
                    {
                        collection.Insert(new LoadContentCommonColumn()
                        {
                            Handle = fontHandle,
                            Hash01 = hash01,
                            Hash02 = hash02,
                            UsedCount = 1,
                        });

                        db.Commit();
                    }
                }
                else
                {
                    fontHandle = one.Handle;
                    one.UsedCount++;
                    collection.Update(one);
                    db.Commit();
                }
                return fontHandle;
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
                var collection = db.GetCollection<LoadContentCommonColumn>();

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
        /// 画像読込デバッグ用カラム
        /// </summary>
        internal class FontLoadDebugDBColumn
        {
            public int id { get; set; }

            public int Handle { get; set; }

            public string FilePath { get; set; }
        }

    }
}
