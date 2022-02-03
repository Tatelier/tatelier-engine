using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatelier.Engine
{
    public interface IInput
    {
        /// <summary>
        /// 押下カウントを取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>-1: 離したタイミング, 0: 未押下, 1: 押下したタイミング, 1以上: 押下中</returns>
        int GetCount(int key);

        /// <summary>
        /// 押下中かどうか
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>true: 押下中, false: 未押下</returns>
        bool GetKey(int key);

        /// <summary>
        /// 押下したタイミングかどうか
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>true: 押下した, false: 押下したタイミングではない</returns>
        bool GetKeyDown(int key);

        /// <summary>
        /// 離れたタイミングかどうか
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns>true: 離れた, false: 離れたタイミングではない</returns>
        bool GetKeyUp(int key);
    }
}
