﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SextantTG.ActiveRecord
{
    /// <summary>
    /// 图片实体类
    /// </summary>
    public class Picture : BaseAR
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        public string PictureId { get; set; }

        /// <summary>
        /// 景点ID
        /// </summary>
        public string SightsId { get; set; }

        /// <summary>
        /// 旅行ID（可为空）
        /// </summary>
        public string TourId { get; set; }

        /// <summary>
        /// 子旅行ID（可为空）
        /// </summary>
        public string SubTourId { get; set; }

        /// <summary>
        /// 图片存储的相对路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 图片描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 上传人
        /// </summary>
        public string UploaderId { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime? CreatingTime { get; set; }


        public override string ToString()
        {
            return string.Format("ID:{0}, 描述:{1}, 上传用户ID:{2}", PictureId, Description, UploaderId);
        }
    }
}
