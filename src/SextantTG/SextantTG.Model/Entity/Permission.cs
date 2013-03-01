﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SextantTG.Model.Entity
{
    /// <summary>
    /// 权限实体类
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public int? PermissionType { get; set; }
    }
}
